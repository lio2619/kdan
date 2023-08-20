namespace Kdan.Models
{
    public class KdanMembers
    {
        /// <summary>
        /// 當作主鍵用
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 員工編號
        /// </summary>
        public string EmployeeNumber { get; set; }
        /// <summary>
        /// 上班打卡時間
        /// </summary>
        public DateTime? ClockIn { get; set; }
        /// <summary>
        /// 下班打卡時間
        /// </summary>
        public DateTime? ClockOut { get; set; }
    }
}
