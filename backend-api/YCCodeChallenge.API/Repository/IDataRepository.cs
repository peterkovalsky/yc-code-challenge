using YCCodeChallenge.Model;

namespace YCCodeChallenge.Repository
{
    public interface IDataRepository
    {
        List<PayCode> GetPayCodes();

        List<Disbursement> GetDisbursements();

        List<Payslip> GetPayslips();
    }
}
