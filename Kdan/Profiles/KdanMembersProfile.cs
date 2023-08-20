using AutoMapper;
using Kdan.Models;
using Kdan.Parameters;

namespace Kdan.Profiles
{
    public class KdanMembersProfile : Profile
    {
        public KdanMembersProfile()
        {
            CreateMap<KdanMemberPara, KdanMembers>()
                .ForMember(o => o.EmployeeNumber, y => y.MapFrom(s => s.EmployeeNumber))
                .ForMember(o => o.ClockIn, y => y.MapFrom(s => s.ClockIn))
                .ForMember(o => o.ClockOut, y => y.MapFrom(s => s.ClockOut));
        }
    }
}
