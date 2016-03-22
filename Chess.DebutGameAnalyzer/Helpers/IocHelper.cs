using Chess.Core.Bot;
using Chess.Core.Bot.Interfaces;
using Chess.Core.FactoryFigures;
using Chess.Core.Mediator;
using Chess.Core.Models;
using Chess.Entities;
using Chess.Entities.Models;
using Chess.Services;
using Chess.Services.Interfaces;
using Microsoft.Practices.Unity;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace Chess.DebutGameAnalyzer.Helpers
{
    public static class IocHelper
    {
        public static UnityContainer Init()
        {
            var container = new UnityContainer();
            container
                .RegisterType<IDataContextAsync, ChessContext>(new PerThreadLifetimeManager())
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerThreadLifetimeManager())
                .RegisterType<IRepositoryAsync<User>, Repository<User>>()
                .RegisterType<IRepositoryAsync<Invitation>, Repository<Invitation>>()
                .RegisterType<IRepositoryAsync<Game>, Repository<Game>>()
                .RegisterType<IRepositoryAsync<Bot>, Repository<Bot>>()
                .RegisterType<IRepositoryAsync<GameLog>, Repository<GameLog>>()
                .RegisterType<IRepositoryAsync<UserRole>, Repository<UserRole>>()
                .RegisterType<IRepositoryAsync<Player>, Repository<Player>>()
                .RegisterType<IRepositoryAsync<DebutGame>, Repository<DebutGame>>()
                .RegisterType<IUserService, UserService>(new PerThreadLifetimeManager())
                .RegisterType<IGameService, GameService>(new PerThreadLifetimeManager())
                .RegisterType<IPlayerService, PlayerService>(new PerThreadLifetimeManager())
                .RegisterType<IInvitationService, InvitationService>(new PerThreadLifetimeManager())
                .RegisterType<IChessboard, Chessboard>()
                .RegisterType<ICreatorBishop, CreatorBishop>(new PerThreadLifetimeManager())
                .RegisterType<ICreatorKing, CreatorKing>(new PerThreadLifetimeManager())
                .RegisterType<ICreatorKnight, CreatorKnight>(new PerThreadLifetimeManager())
                .RegisterType<ICreatorPawn, CreatorPawn>(new PerThreadLifetimeManager())
                .RegisterType<ICreatorQueen, CreatorQueen>(new PerThreadLifetimeManager())
                .RegisterType<ICreatorRook, CreatorRook>(new PerThreadLifetimeManager())
                .RegisterType<IMoveMediator, MoveMediator>(new PerThreadLifetimeManager())
                .RegisterType<IBishopColleague, BishopColleague>(new PerThreadLifetimeManager())
                .RegisterType<IKingColleague, KingColleague>(new PerThreadLifetimeManager())
                .RegisterType<IKnightColleague, KnightColleague>(new PerThreadLifetimeManager())
                .RegisterType<IPawnColleague, PawnColleague>(new PerThreadLifetimeManager())
                .RegisterType<IQueenColleague, QueenColleague>(new PerThreadLifetimeManager())
                .RegisterType<IRookColleague, RookColleague>(new PerThreadLifetimeManager())
                .RegisterType<IBotMediator, BotMediator>(new PerThreadLifetimeManager())
                .RegisterType<IMonkeyBotColleague, MonkeyBotColleague>(new PerThreadLifetimeManager())
                .RegisterType<IAlphaBetaBotColleague, AlphaBetaBotColleague>(new PerThreadLifetimeManager())
                .RegisterType<ICastling, KingColleague>(new PerThreadLifetimeManager());

            return container;
        }
    }
}
