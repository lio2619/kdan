using AutoMapper;
using Kdan.Models;
using Kdan.Dtos;
using Kdan.Parameters;

namespace Kdan.Profiles
{
    public class KdanMembersInformationProfile : Profile
    {
        public KdanMembersInformationProfile()
        {
            CreateMap<KdanMembers, KdanMembersInformationDto>()
                .ForMember(o => o.EmployeeNumber, y => y.MapFrom(s => s.EmployeeNumber))
                .ForMember(o => o.ClockIn, y => y.MapFrom(s => (s.ClockIn == null) ? (TimeOnly?)null : (TimeOnly.FromDateTime((DateTime)s.ClockIn))))
                .ForMember(o => o.ClockOut, y => y.MapFrom(s => (s.ClockOut == null) ? (TimeOnly?)null : (TimeOnly.FromDateTime((DateTime)s.ClockOut))));
        }
    }
}
