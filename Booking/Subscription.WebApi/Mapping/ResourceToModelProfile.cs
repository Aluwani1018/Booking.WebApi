using AutoMapper;
using Subscription.Core.Domain;
using Subscription.WebApi.Models.Account.Requests;

namespace Subscription.WebApi.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<User, RegisterRequest>()
              .ForMember(x => x.Password, v => v.Ignore())
              .ForMember(x => x.ConfirmPassword, v => v.Ignore())
              .ForMember(x => x.Email, v => v.MapFrom(a => a.UserName))
              .ReverseMap()
               .ForMember(x => x.AccessFailedCount, v => v.Ignore())
               .ForMember(x => x.Claims, v => v.Ignore())
               .ForMember(x => x.ConcurrencyStamp, v => v.Ignore())
               .ForMember(x => x.CreatedDate, v => v.Ignore())
               .ForMember(x => x.EmailConfirmed, v => v.Ignore())
               .ForMember(x => x.Id, v => v.Ignore())
               .ForMember(x => x.LockoutEnabled, v => v.Ignore())
               .ForMember(x => x.LockoutEnd, v => v.Ignore())
               .ForMember(x => x.NormalizedEmail, v => v.Ignore())
               .ForMember(x => x.NormalizedUserName, v => v.Ignore())
               .ForMember(x => x.PhoneNumberConfirmed, v => v.Ignore())
               .ForMember(x => x.Roles, v => v.Ignore())
               .ForMember(x => x.SecurityStamp, v => v.Ignore())
               .ForMember(x => x.TwoFactorEnabled, v => v.Ignore())
               .ForMember(x => x.UpdatedDate, v => v.Ignore());
        }
    }
}
