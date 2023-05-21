using Microsoft.AspNetCore.Mvc;
using YCCodeChallenge.Services;
using YCCodeChallenge.ViewModel;

namespace YCCodeChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly ICalculationService _calculationService;
    private readonly ILogger<ReportController> _logger;

    public ReportController(ILogger<ReportController> logger, ICalculationService calculationService)
    {
        _logger = logger;
        _calculationService = calculationService;
    }

    [HttpGet("quarterly/{quarter}/{year}")]
    public ActionResult<QuarterlyReportResponse> QuarterlyReport(int quarter, int year)
    {
        var disbursements = _calculationService.CalculateDisbursements(quarter, year);
        var otePayments = _calculationService.CalculateOTE(quarter, year);
        var superPayments = _calculationService.CalculateSuper(otePayments);

        return new QuarterlyReportResponse
        {
            EmployeeReports = disbursements.Select(d => new EmployeeQuarterlyReport
            {
                EmployeeCode = d.Key,
                TotalDisbursed = Math.Round(d.Value, 2),
                TotalOTE = Math.Round(otePayments.GetValueOrDefault(d.Key), 2),
                TotalSuperPayable = Math.Round(superPayments.GetValueOrDefault(d.Key), 2)
            }).ToList()
        };
    }
}
