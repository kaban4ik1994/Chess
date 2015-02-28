using System;
using AutoMapper;
using Chess.Entities.Models;
using Chess.WebAPI.Models;
using Repository.Pattern.Infrastructure;

namespace Chess.WebAPI.Mappings
{
    public class InvitationModelProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();
            MapAddInvitationViewModelToInvitationEntity();
        }

        private void MapAddInvitationViewModelToInvitationEntity()
        {
            CreateMap<AddInvitationViewModel, Invitation>()
                .ForMember(invitation => invitation.CreateDate, expression => expression.MapFrom(model => DateTime.Now))
                .ForMember(invitation => invitation.Id, expression => expression.Ignore())
                .ForMember(invitation => invitation.IsAccepted, expression => expression.MapFrom(model => false))
                .ForMember(invitation => invitation.IsDeclined, expression => expression.MapFrom(model => false))
                .ForMember(invitation => invitation.InvitatorId,
                    expression => expression.MapFrom(model => model.InvitatorId))
                .ForMember(invitation => invitation.AcceptorId, expression => expression.Ignore())
                .ForMember(invitation => invitation.GameId, expression => expression.Ignore())
                .ForMember(invitation => invitation.ObjectState,
                    expression => expression.MapFrom(model => ObjectState.Added))
                .ForMember(invitation => invitation.Invitator, expression => expression.Ignore())
                .ForMember(invitation => invitation.Acceptor, expression => expression.Ignore())
                .ForMember(invitation => invitation.Game, expression => expression.Ignore());
        }
    }
}