using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Models;
using Chess.Entities.Models;
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

        public GameService(IRepositoryAsync<Game> repository, IUnitOfWorkAsync unitOfWorkAsync, IChessboard chessboard, IRepositoryAsync<Invitation> invitationRepository)
            : base(repository)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _chessboard = chessboard;
            _invitationRepository = invitationRepository;
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
                  FirstPlayerGameTime = invitation1.Game.FirstPlayerGameTime,
                  SecondPlayerGameTime = invitation1.Game.SecondPlayerGameTime,
                  FirstPlayerName = invitation1.Invitator.User.UserName,
                  SecondPlayerName = invitation1.Acceptor.User.UserName,
                  GameLog = invitation1.Game.GameLogs.OrderByDescending(log => log.Index).FirstOrDefault().Log,
                  LogIndex = invitation1.Game.GameLogs.OrderByDescending(log => log.Index).FirstOrDefault().Index,
              }).FirstOrDefault();

            return await Task.FromResult(result);
        }
    }
}
