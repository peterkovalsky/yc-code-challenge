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

    [HttpGet("quarterly/{quarter}/{year}/{employeeCode?}")]
    public ActionResult<QuarterlyReportResponse> QuarterlyReport(int quarter, int year, double? employeeCode)
    {
        var disbursements = _calculationService.CalculateDisbursements(quarter, year, employeeCode);
        var otePayments = _calculationService.CalculateOTE(quarter, year, employeeCode);
        var superPayments = _calculationService.CalculateSuper(otePayments);

        return new QuarterlyReportResponse
        {
            EmployeeReports = disbursements.Select(d => new EmployeeQuarterlyReport
            {
                EmployeeCode = d.Key,
                TotalDisbursed = d.Value,
                TotalOTE = otePayments.GetValueOrDefault(d.Key),
                TotalSuperPayable =superPayments.GetValueOrDefault(d.Key),
                
            }).ToList()
        };
    }
}
