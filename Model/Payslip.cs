using YCCodeChallenge.Excel;

namespace YCCodeChallenge.Model
{
    [ExcelSheet("Payslips")]
    public class Payslip
    {
        [ExcelColumn("payslip_id")]
        public string Id { get; set; }

        [ExcelColumn("end")]
        public DateTime End { get; set; }

        [ExcelColumn("employee_code")]
        public int EmployeeCode { get; set; }

        [ExcelColumn("code")]
        public string PayCode { get; set; }

        [ExcelColumn("amount")]
        public decimal Amount { get; set; }
    }
}
