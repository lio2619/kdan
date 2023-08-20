using AutoMapper;
using Kdan.Models;
using Kdan.Parameters;

namespace Kdan.Profiles
{
    public class KdanClockProfile : Profile
    {
        public KdanClockProfile()
        {
            CreateMap<KdanClockPara, KdanMembers>()
                .ForMember(o => o.EmployeeNumber, y => y.MapFrom(s => s.EmployeeNumber))
                .ForMember(o => o.ClockIn, y => y.MapFrom(s => s.Clock));
        }
    }
}
