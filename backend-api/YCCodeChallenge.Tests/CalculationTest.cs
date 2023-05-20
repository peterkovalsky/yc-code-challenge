using Microsoft.Extensions.Options;
using Moq;
using YCCodeChallenge.Model;
using YCCodeChallenge.Repository;
using YCCodeChallenge.Services;

namespace YCCodeChallenge.Tests;

public class CalculationTest
{
    private readonly ICalculationService _calculationService;

    public CalculationTest()
    {
        IOptions<CalculationOptions> options = Options.Create<CalculationOptions>(new CalculationOptions
        {
            SuperPercentage = 9.5m,
            DaysAfterPeriodEnds = 28
        });

        var repositoryMock = new Mock<IDataRepository>();

        repositoryMock.Setup(m => m.GetPayCodes())
            .Returns(new List<PayCode>
            {
                new PayCode
                {
                    Code = "1 - Normal",
                    OTETreatment = "OTE"
                },
                new PayCode
                {
                    Code = "A073 - NW Site Allowance",
                    OTETreatment = "OTE"
                },
                new PayCode
                {
                    Code = "A012 - Pager/On call",
                    OTETreatment = "Not OTE"
                },
                new PayCode
                {
                    Code = "P001 - Co. Super 9.5%",
                    OTETreatment = "Not OTE"
                },
            });

        repositoryMock.Setup(m => m.GetPayslips())
            .Returns(new List<Payslip>
            {
                new Payslip
                {
                    Id = "1",
                    EmployeeCode = 100,
                    End = new DateTime(2022, 1, 1),
                    PayCode = "1 - Normal",
                    Amount = 5000
                },
                new Payslip
                {
                    Id = "2",
                    EmployeeCode = 100,
                    End = new DateTime(2022, 1, 1),
                    PayCode = "A012 - Pager/On call",
                    Amount = 1500
                },
                new Payslip
                {
                    Id = "3",
                    EmployeeCode = 100,
                    End = new DateTime(2022, 1, 1),
                    PayCode = "P001 - Co. Super 9.5%",
                    Amount = 475
                },
                new Payslip
                {
                    Id = "4",
                    EmployeeCode = 100,
                    End = new DateTime(2022, 2, 1),
                    PayCode = "1 - Normal",
                    Amount = 5000
                },
                new Payslip
                {
                    Id = "5",
                    EmployeeCode = 100,
                    End = new DateTime(2022, 2, 1),
                    PayCode = "P001 - Co. Super 9.5%",
                    Amount = 475
                },
                new Payslip
                {
                    Id = "6",
                    EmployeeCode = 100,
                    End = new DateTime(2022, 3, 1),
                     PayCode = "1 - Normal",
                    Amount = 5000
                },
                new Payslip
                {
                    Id = "7",
                    EmployeeCode = 100,
                    End = new DateTime(2022, 1, 11),
                    PayCode = "P001 - Co. Super 9.5%",
                    Amount = 475
                },
            });

        repositoryMock.Setup(m => m.GetDisbursements())
            .Returns(new List<Disbursement>
            {
                new Disbursement
                {
                    EmployeeCode = 100,
                    PaymentMade = new DateTime(2022, 02, 27),
                    SgcAmount = 500
                },
                new Disbursement
                {
                    EmployeeCode = 100,
                    PaymentMade = new DateTime(2022, 03, 30),
                    SgcAmount = 500
                },
                new Disbursement
                {
                    EmployeeCode = 100,
                    PaymentMade = new DateTime(2022, 04, 30),
                    SgcAmount = 500
                },
            });

        _calculationService = new CalculationService(repositoryMock.Object, options);
    }

    [Fact]
    public void Should_Correctly_Calculate_OTE_Payments()
    {
        var otePayments = _calculationService.CalculateOTE(1, 2022);

        Assert.Equal(1, otePayments.Count);
        Assert.Equal(100, otePayments.First().Key);
        Assert.Equal(15000, otePayments.First().Value);
    }

    [Fact]
    public void Should_Correctly_Calculate_Super_Payments()
    {
        var otePayments = _calculationService.CalculateOTE(1, 2022);
        var superPayments = _calculationService.CalculateSuper(otePayments);

        Assert.Equal(1, superPayments.Count);
        Assert.Equal(100, superPayments.First().Key);
        Assert.Equal(1425, superPayments.First().Value);
    }

    [Fact]
    public void Should_Correctly_Calculate_Disbursements()
    {
        var disbursements = _calculationService.CalculateDisbursements(1, 2022);

        Assert.Equal(1, disbursements.Count);
        Assert.Equal(100, disbursements.First().Key);
        Assert.Equal(1000, disbursements.First().Value);
    }

    [Theory]
    [InlineData("1 - Normal", true)]
    [InlineData("A073 - NW Site Allowance", true)]
    [InlineData("A012 - Pager/On call", false)]
    [InlineData("P001 - Co. Super 9.5%", false)]
    public void Should_Correctly_Determine_OTE(string payCode, bool expectedIsOTE)
    {
        var isOTE = _calculationService.IsOTE(payCode);

        Assert.Equal(expectedIsOTE, isOTE);
    }
}
