using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Models;
using Chess.Entities.Models;
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

        public async Task<Cell[,]> InitializeNewGameByInvitationId(long invitationId)
        {
            _chessboard.InitNewGame();

            var invitation =
                _invitationRepository.Query(invitation1 => invitation1.Id == invitationId).Select().FirstOrDefault();
            if (invitation == null) return null;

            var gameLog = new GameLog
            {
                CreateDate = DateTime.Now,
                ObjectState = ObjectState.Added,
                Index = 1,
                Log = _chessboard.SerializedBoard()
            };

            var game = new Game
            {
                GameIdentificator = Guid.NewGuid(),
                FirstPlayerGameTime = new DateTime(0),
                SecondPlayerGameTime = new DateTime(0),
                IsEnded = false,
                GameStartDate = gameLog.CreateDate,
                ObjectState = ObjectState.Added,
                GameLogs = new List<GameLog> { gameLog }
            };

            Insert(game);
            await _unitOfWorkAsync.SaveChangesAsync();
            invitation.GameId = game.Id;
            invitation.ObjectState = ObjectState.Modified;
            _invitationRepository.Update(invitation);

            return await Task.FromResult(_chessboard.Board);
        }
    }
}
