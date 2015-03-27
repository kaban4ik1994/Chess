using System;
using System.Linq;
using System.Web.UI;
using Chess.Core.FactoryFigures;
using Chess.Core.Mediator;
using Chess.Core.Models;
using Chess.Entities;
using Chess.Entities.Models;
using Chess.Services;
using Chess.Services.Interfaces;
using Chess.WebAPI.Filters.AuthorizationFilters;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;

namespace Chess.WebAPI.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container
                .RegisterType<IDataContextAsync, ChessContext>(new PerResolveLifetimeManager())
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<User>, Repository<User>>()
                .RegisterType<IRepositoryAsync<Invitation>, Repository<Invitation>>()
                .RegisterType<IRepositoryAsync<Game>, Repository<Game>>()
                .RegisterType<IRepositoryAsync<Bot>, Repository<Bot>>()
                .RegisterType<IRepositoryAsync<GameLog>, Repository<GameLog>>()
                .RegisterType<IRepositoryAsync<Player>, Repository<Player>>()
                .RegisterType<IRepositoryAsync<PlayerGame>, Repository<PlayerGame>>()
                .RegisterType<IUserService, UserService>(new PerThreadLifetimeManager())
                .RegisterType<IGameService, GameService>(new PerThreadLifetimeManager())
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
                .RegisterType<IRookColleague, RookColleague>(new PerThreadLifetimeManager());
        }
    }
}
