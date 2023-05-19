namespace YCCodeChallenge.ViewModel
{
    public class QuarterlyReportResponse
    {
        public List<EmployeeQuarterlyReport> EmployeeReports { get; set; }
    }

    public class EmployeeQuarterlyReport
    {
        public double EmployeeCode { get; set; }

        public decimal TotalOTE { get; set; }

        public decimal TotalSuperPayable { get; set; }

        public decimal TotalDisbursed { get; set; }

        public decimal Variance
        {
            get
            {
                return TotalDisbursed - TotalSuperPayable;
            }
        }
    }
}
