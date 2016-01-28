using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Bot;
using Chess.Core.Bot.Interfaces;
using Chess.Core.Mediator;
using Chess.Core.Models;
using Chess.Entities.Models;
using Chess.Enums;
using Chess.Helpers;
using Chess.Models;
using Chess.Services.Interfaces;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;

namespace Chess.Services
{
    public class GameService : Service<Game>, IGameService
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IChessboard _chessboard;
        private readonly IRepositoryAsync<Invitation> _invitationRepository;
        private readonly IMoveMediator _moveMediator;
        private readonly IBotMediator _botMediator;
        private readonly IRepositoryAsync<GameLog> _gameLogRepository;

        public GameService(IRepositoryAsync<Game> repository, IUnitOfWorkAsync unitOfWorkAsync, IChessboard chessboard, IRepositoryAsync<Invitation> invitationRepository, IMoveMediator moveMediator, IRepositoryAsync<GameLog> gameLogRepository, IBotMediator botMediator)
            : base(repository)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _chessboard = chessboard;
            _invitationRepository = invitationRepository;
            _moveMediator = moveMediator;
            _gameLogRepository = gameLogRepository;
            _botMediator = botMediator;
        }

        public async Task<GameViewModel> GetGameBoardByInvitationId(long invitationId)
        {
            var invitation =
                _invitationRepository.Query(invitation1 => invitation1.Id == invitationId)
                .Include(x => x.Game)
                .Include(x => x.Game.GameLogs)
                .Select().FirstOrDefault();
            if (invitation == null) return null;

            if (invitation.Game == null)
            {
                _chessboard.InitNewGame();
                var gameLog = new GameLog
                {
                    CreateDate = DateTime.Now,
                    Index = 1,
                    Log = _chessboard.SerializedBoard(),
                    ObjectState = ObjectState.Added
                };

                var game = new Game
                {
                    GameIdentificator = Guid.NewGuid(),
                    FirstPlayerGameTime = DateTime.Now,
                    SecondPlayerGameTime = DateTime.Now,
                    IsEnded = false,
                    GameStartDate = DateTime.Now,
                    GameLogs = new List<GameLog>(),
                    Invitation = invitation,
                    ObjectState = ObjectState.Added,
                    Watchers = new List<PlayerGame>()
                };
                game.GameLogs.Add(gameLog);
                invitation.Game = game;
                invitation.GameId = game.Id;

                _invitationRepository.Update(invitation);

                await _unitOfWorkAsync.SaveChangesAsync();
            }


            var result = _invitationRepository.Query(x => x.Id == invitationId).Select(invitation1 => new GameViewModel
              {
                  GameId = invitation1.Game.Id,
                  FirstPlayerGameTime = invitation1.Game.FirstPlayerGameTime,
                  SecondPlayerGameTime = invitation1.Game.SecondPlayerGameTime,
                  FirstPlayerName = invitation1.Invitator.IsBot ? invitation1.Invitator.Bot.Type.ToString() : invitation1.Invitator.User.UserName,
                  FirstPlayerId = invitation1.Invitator.IsBot ? invitation1.Invitator.Bot.Id : invitation1.Invitator.User.UserId,
                  SecondPlayerName = invitation1.Acceptor.IsBot ? invitation1.Acceptor.Bot.Type.ToString() : invitation1.Acceptor.User.UserName,
                  SecondPlayerId = invitation1.Acceptor.IsBot ? invitation1.Acceptor.Bot.Id : invitation1.Acceptor.User.UserId,
                  GameLog = invitation1.Game.GameLogs.OrderByDescending(log => log.Index).FirstOrDefault().Log,
                  LogIndex = invitation1.Game.GameLogs.OrderByDescending(log => log.Index).FirstOrDefault().Index,
                  IsEnded = invitation1.Game.IsEnded
              }).FirstOrDefault();

            return await Task.FromResult(result);
        }

        public async Task<ExtendedPosition> GetBotMove(long gameId)
        {
            var game = Query(x => x.Id == gameId).Include(x => x.GameLogs)
                .Include(game1 => game1.Invitation)
                .Include(game1 => game1.Invitation.Invitator)
                .Include(game1 => game1.Invitation.Acceptor)
                .Include(game1 => game1.Invitation.Acceptor.Bot)
                .Select().FirstOrDefault();
            if (game == null) return null;
            _chessboard.DeserializeBoard(game.GameLogs.Last().Log);
            var color = game.GameLogs.Last().Index % 2 != 0 ? Color.White : Color.Black;
            if (!game.Invitation.Acceptor.IsBot) return null;
            var result=await Task.FromResult(_botMediator.Send(_chessboard, color, game.Invitation.Acceptor.Bot.Type));
            return result;
        }

        public Task<bool> IsMoveOfBot(long gameId)
        {
            var game = Query(x => x.Invitation.Id == gameId).Include(x => x.GameLogs)
               .Include(game1 => game1.Invitation)
               .Include(game1 => game1.Invitation.Invitator)
               .Include(game1 => game1.Invitation.Acceptor)
               .Select().FirstOrDefault();

            if (game == null) return Task.FromResult(false);
            if (!game.Invitation.Acceptor.IsBot) return Task.FromResult(false);
            var color = game.GameLogs.Last().Index % 2 != 0 ? Color.White : Color.Black;
            return Task.FromResult(color != Color.White);
        }

	    public async Task<bool> IsGameEnded(long gameId)
	    {
		    var game = Query(x => x.Id == gameId).Include(x => x.GameLogs)
			    .Include(game1 => game1.Invitation)
			    .Select().FirstOrDefault();
		    if (game == null) return false;
				_chessboard.DeserializeBoard(game.GameLogs.Last().Log);
				var color = game.GameLogs.Last().Index % 2 != 0 ? Color.White : Color.Black;
				var allMoves = new List<ExtendedPosition>();
				allMoves.AddRange(_moveMediator.GetExtendedAttackMovesByColor(color, _chessboard));
				allMoves.AddRange(_moveMediator.GetExtendedPossibleMovesByColor(color, _chessboard));
				var randomMove = RandomGeneratorHelper.GetRandomValueFromList(allMoves);
		    if (randomMove == null) return true;
				var moveResult = _moveMediator.Send(randomMove.From, randomMove.To, _chessboard, color);
		    var isEnded = false;
				switch (moveResult)
				{
					case MoveStatus.Success:
						_chessboard.UndoLastMove();
						break;
					case MoveStatus.Checkmate:
						isEnded = true;
						break;
				}
		    if (!isEnded) return false;

		    game.IsEnded = true;
		    Update(game);
		    await _unitOfWorkAsync.SaveChangesAsync();
		    return  true;
	    }

	    public async Task<GameViewModel> MakeMove(long gameId, Position from, Position to)
        {
            var game = Query(x => x.Id == gameId).Include(x => x.GameLogs)
                .Include(game1 => game1.Invitation)
                .Select().FirstOrDefault();
            if (game == null) return null;
            _chessboard.DeserializeBoard(game.GameLogs.Last().Log);
            var color = game.GameLogs.Last().Index % 2 != 0 ? Color.White : Color.Black;
            var moveResult = _moveMediator.Send(from, to, _chessboard, color);
            if (moveResult == MoveStatus.Checkmate && !game.IsEnded)
            {
                game.IsEnded = true;
                Update(game);
                await _unitOfWorkAsync.SaveChangesAsync();
            }

            if (moveResult != MoveStatus.Success)
                return await Task.FromResult(new GameViewModel { MoveStatus = moveResult });
            game.GameLogs.Add(new GameLog { CreateDate = DateTime.Now, ObjectState = ObjectState.Added, Log = _chessboard.SerializedBoard(), Index = game.GameLogs.Last().Index + 1 });
            Update(game);
            await _unitOfWorkAsync.SaveChangesAsync();
            return
                await
                    Task.FromResult(new GameViewModel
                    {
                        GameLog = game.GameLogs.Last().Log,
                        LogIndex = game.GameLogs.Last().Index,
                        SecondPlayerGameTime = game.SecondPlayerGameTime,
                        FirstPlayerGameTime = game.FirstPlayerGameTime,
                        MoveStatus = moveResult
                    });
        }

        public async Task<GameLogViewModel> GetGameLogByInvitationIdAndLogId(long invitationId, long logId)
        {
            var result =
                _gameLogRepository.Query(log => log.Index == logId && log.Game.Invitation.Id == invitationId && log.Game.IsEnded)
                    .Select(log => new GameLogViewModel
                    {
                        Id = log.Id,
                        Log = log.Log,
                        FirstPlayerName = log.Game.Invitation.Invitator.IsBot ? log.Game.Invitation.Invitator.Bot.Type.ToString() : log.Game.Invitation.Invitator.User.UserName,
                        SecondPlayerName = log.Game.Invitation.Acceptor.IsBot ? log.Game.Invitation.Acceptor.Bot.Type.ToString() : log.Game.Invitation.Acceptor.User.UserName,
                    }).FirstOrDefault();

            return await Task.FromResult(result);
        }

        public async Task<long> GetQuantityOfMoveByInvitationId(long invitationId)
        {
            return await Task.FromResult(_gameLogRepository.Query(log => log.Game.Invitation.Id == invitationId && log.Game.IsEnded).Select().Count());
        }
    }
}
