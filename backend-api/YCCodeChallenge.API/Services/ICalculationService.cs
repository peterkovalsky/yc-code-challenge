namespace YCCodeChallenge.Services
{
    public interface ICalculationService
    {
        Dictionary<double, decimal> CalculateOTE(int quarter, int year);

        Dictionary<double, decimal> CalculateSuper(Dictionary<double, decimal> otePayments);

        Dictionary<double, decimal> CalculateDisbursements(int quarter, int year);

        bool IsOTE(string code);
    }
}
