using System;
using System.Collections.Generic;
using AutoMapper;
using Chess.Entities.Models;
using Chess.Helpers;
using Chess.WebAPI.Models;
using Repository.Pattern.Infrastructure;

namespace Chess.WebAPI.Mappings
{
    public class UserModelProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();
            MapRegisterViewModelToUserEntity();
        }

        private void MapRegisterViewModelToUserEntity()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(user => user.UserId, expression => expression.Ignore())
                .ForMember(user => user.UserName, expression => expression.MapFrom(model => model.UserName))
                .ForMember(user => user.Email, expression => expression.MapFrom(model => model.Email))
                .ForMember(user => user.PasswordHash, expression => expression.MapFrom(model => PasswordHashHelper.GetHash(model.Password)))
                .ForMember(user => user.FirstName, expression => expression.MapFrom(model => model.FirstName))
                .ForMember(user => user.SecondName, expression => expression.MapFrom(model => model.LastName))
                .ForMember(user => user.Tokens, expression => expression.MapFrom(model => new List<Token> { new Token { ObjectState = ObjectState.Added } }))
                .ForMember(user => user.ObjectState, expression => expression.MapFrom(model => ObjectState.Added))
                .ForMember(user => user.Active, expression => expression.MapFrom(model => true))
                .ForMember(user => user.UserRoles, expression => expression.MapFrom(model => new List<UserRole> { new UserRole { ObjectState = ObjectState.Added, RoleId = 2 } }))
                .ForMember(user => user.Player, expression => expression.MapFrom(model => new Player { IsBot = false, IdentityNumber = Guid.NewGuid(), ObjectState = ObjectState.Added }));

        }
    }
}