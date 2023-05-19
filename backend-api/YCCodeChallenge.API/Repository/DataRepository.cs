using YCCodeChallenge.Excel;
using YCCodeChallenge.Model;

namespace YCCodeChallenge.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly IExcelReader _excelReader;

        public DataRepository(IExcelReader excelReader)
        {
            _excelReader = excelReader;
        }

        public List<PayCode> GetPayCodes()
        {
            return _excelReader.GetItems<PayCode>();
        }

        public List<Disbursement> GetDisbursements()
        {
            return _excelReader.GetItems<Disbursement>();
        }

        public List<Payslip> GetPayslips()
        {
            return _excelReader.GetItems<Payslip>();
        }
    }
}
