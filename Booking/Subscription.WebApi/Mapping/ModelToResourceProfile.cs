using AutoMapper;
using Subscription.Core.Security.Tokens;
using Subscription.WebApi.Models.Authentication.Response;
using System.Linq;

namespace Subscription.WebApi.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {

            CreateMap<AccessToken, LogInResponse>()
                .ForMember(a => a.AccessToken, opt => opt.MapFrom(a => a.Token));
        }
    }
}
