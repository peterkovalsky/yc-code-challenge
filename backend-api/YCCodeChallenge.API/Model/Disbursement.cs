using YCCodeChallenge.Excel;

namespace YCCodeChallenge.Model
{
    [ExcelSheet("Disbursements")]
    public class Disbursement
    {
        [ExcelColumn("sgc_amount")]
        public decimal SgcAmount { get; set; }

        [ExcelColumn("payment_made")]
        public DateTime PaymentMade { get; set; }

        [ExcelColumn("pay_period_from")]
        public DateTime PayPeriodFrom { get; set; }

        [ExcelColumn("pay_period_to")]
        public DateTime PayPeriodTo { get; set; }

        [ExcelColumn("employee_code")]
        public double EmployeeCode { get; set; }
    }
}
