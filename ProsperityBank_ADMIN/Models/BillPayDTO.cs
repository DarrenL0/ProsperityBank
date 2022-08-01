using System;


namespace ProsperityBank_ADMIN.Models
{
    public class BillPayDTO
    {

        public int BillPayId { get; set; }
        public int AccountNumber { get; set; }
        public int PayeeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int PeriodType { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool? Blocked { get; set; }
    }
}
