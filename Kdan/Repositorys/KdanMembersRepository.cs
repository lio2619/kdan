using Microsoft.EntityFrameworkCore;
using Kdan.Repositorys.Interface;
using Kdan.Models;
using Kdan.Context;

namespace Kdan.Repositorys
{
    public class KdanMembersRepository : BaseRepository<KdanMembers>, IKdanMembersRepository
    {
        private readonly KdanContext _kdanContext;
        public KdanMembersRepository(KdanContext kdanContext) : base(kdanContext)
        {
            _kdanContext = kdanContext;
        }
    }
}
