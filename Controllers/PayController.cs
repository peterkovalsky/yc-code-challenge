using Microsoft.AspNetCore.Mvc;
using YCCodeChallenge.Repository;

namespace YCCodeChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public class PayController : ControllerBase
{
    private readonly ISuperRepository _superRepository;
    private readonly ILogger<PayController> _logger;

    public PayController(ILogger<PayController> logger, ISuperRepository superRepository)
    {
        _logger = logger;
        _superRepository = superRepository;
    }

    public ActionResult Get()
    {
        _superRepository.GetPayCodes();

        return Ok("Done");
    }
}
