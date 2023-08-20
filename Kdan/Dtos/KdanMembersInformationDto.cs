namespace Kdan.Dtos
{
    public class KdanMembersInformationDto
    {
        public int EmployeeNumber { get; set; }
        public TimeOnly ClockIn { get; set; }
        public TimeOnly ClockOut { get; set; }
        public TimeOnly BreakTime { get; set; }
        public string TotalWorkingTime { get; set; }
    }
}
