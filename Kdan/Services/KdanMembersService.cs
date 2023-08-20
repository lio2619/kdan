using AutoMapper;
using Kdan.Repositorys.Interface;
using Kdan.Services.Interface;
using Kdan.Parameters;
using Kdan.Models;

namespace Kdan.Services
{
    public class KdanMembersService : IKdanMembersService
    {
        private readonly IKdanMembersRepository _kdanMembersRepository;
        private readonly IReadFileService _readFileService;
        private readonly IMapper _mapper;
        public KdanMembersService(IKdanMembersRepository kdanMembersRepository, IReadFileService readFileService, IMapper mapper)
        {
            _kdanMembersRepository = kdanMembersRepository;
            _readFileService = readFileService;
            _mapper = mapper;
        }
        /// <summary>
        /// 將json檔裡面的資料移到DB裡面
        /// </summary>
        /// <param name="jsonFilePath"></param>
        /// <returns></returns>
        public async Task JsonFileToDB(string jsonFilePath)
        {
            var response = _readFileService.ReadJsonFile<KdanMemberPara>(jsonFilePath);
            var kdanMembers = _mapper.Map<IEnumerable<KdanMembers>>(response);
            await _kdanMembersRepository.CreateRange(kdanMembers);
        }
    }
}
