using AutoMapper;
using Demokrata.Model;

namespace Demokrata.Contracts
{
    public class DemokrataAppProfile : Profile
    {
        public DemokrataAppProfile() 
        {
            this.CreateMap<UserDTO, User>()
                .ForMember(p => p.CreationDate, m => m.Ignore())
                .ForMember(p => p.UpdateDate, m => m.Ignore());
        }
    }
}
