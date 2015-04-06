using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Mediator;
using Chess.Core.Models;
using Chess.Entities.Models;
using Chess.Enums;
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

        public GameService(IRepositoryAsync<Game> repository, IUnitOfWorkAsync unitOfWorkAsync, IChessboard chessboard, IRepositoryAsync<Invitation> invitationRepository, IMoveMediator moveMediator)
            : base(repository)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _chessboard = chessboard;
            _invitationRepository = invitationRepository;
            _moveMediator = moveMediator;
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
                  FirstPlayerName = invitation1.Invitator.User.UserName,
                  FirstPlayerId = invitation1.Invitator.User.UserId,
                  SecondPlayerName = invitation1.Acceptor.User.UserName,
                  SecondPlayerId = invitation1.Acceptor.User.UserId,
                  GameLog = invitation1.Game.GameLogs.OrderByDescending(log => log.Index).FirstOrDefault().Log,
                  LogIndex = invitation1.Game.GameLogs.OrderByDescending(log => log.Index).FirstOrDefault().Index,
              }).FirstOrDefault();

            return await Task.FromResult(result);
        }

        public async Task<GameViewModel> MakeMove(long gameId, Position from, Position to)
        {
            var game = Query(x => x.Id == gameId).Include(x => x.GameLogs)
                .Include(game1 => game1.Invitation)
                .Select().FirstOrDefault();
            if (game == null) return null;
            _chessboard.DeserializeBoard(game.GameLogs.Last().Log);
            var moveResult = _moveMediator.Send(from, to, _chessboard);
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
    }
}
