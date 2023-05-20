using Microsoft.Extensions.Options;
using YCCodeChallenge.Model;
using YCCodeChallenge.Repository;

namespace YCCodeChallenge.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly IDataRepository _dataRepository;
        private readonly CalculationOptions _options;
        private readonly List<PayCode> _payCodes;
        private readonly List<Payslip> _payslips;
        private readonly List<Disbursement> _disbursements;

        public CalculationService(IDataRepository dataRepository, IOptions<CalculationOptions> options)
        {
            _dataRepository = dataRepository;
            _options = options.Value;

            _payCodes = _dataRepository.GetPayCodes();
            _payslips = _dataRepository.GetPayslips();
            _disbursements = _dataRepository.GetDisbursements();
        }

        public Dictionary<double, decimal> CalculateOTE(int quarter, int year)
        {
            (DateTime periodFrom, DateTime periodTo) = DateHelper.GetQuarterPeriod(quarter, year);

            return _payslips
                .GroupBy(p => p.EmployeeCode)
                .ToDictionary(g => g.Key, g => g
                    .Where(d => d.End >= periodFrom && d.End <= periodTo && IsOTE(d.PayCode))
                    .Sum(d => d.Amount));
        }

        public Dictionary<double, decimal> CalculateSuper(Dictionary<double, decimal> otePayments)
        {
            return otePayments.ToDictionary(p => p.Key, p => (p.Value * _options.SuperPercentage) / 100);
        }

        public Dictionary<double, decimal> CalculateDisbursements(int quarter, int year)
        {
            (DateTime periodFrom, DateTime periodTo) = DateHelper.GetQuarterPeriod(quarter, year);

            var payFrom = periodFrom.AddDays(_options.DaysAfterPeriodEnds);
            var payTo = periodTo.AddDays(_options.DaysAfterPeriodEnds);

            return _disbursements
                .GroupBy(g => g.EmployeeCode)
                .ToDictionary(g => g.Key, g => g
                    .Where(d => d.PaymentMade >= payFrom && d.PaymentMade <= payTo)
                    .Sum(d => d.SgcAmount));
        }

        public bool IsOTE(string code)
        {
            return _payCodes.Any(c => c.Code == code && c.OTETreatment == "OTE");
        }
    }
}
