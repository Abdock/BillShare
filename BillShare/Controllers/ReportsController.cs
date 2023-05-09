using BillShare.Extensions;
using BillShare.Requests.Reports;
using Contracts.DTOs.Reports;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace BillShare.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ReportsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    [Route("personal")]
    public async Task<ActionResult<Report>> GetReportsForPeriod([FromQuery] ReportForPeriodRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var startDate = DateOnly.Parse(request.StartDate).ToDateTime(TimeOnly.FromTimeSpan(TimeSpan.Zero));
        var endDate = DateOnly.Parse(request.EndDate).ToDateTime(TimeOnly.FromTimeSpan(TimeSpan.FromSeconds(60 * 60 * 24)));
        var userId = User.GetUserId();
        var report = await _serviceManager.ReportService.ReportForPeriodAsync(userId, startDate, endDate, cancellationToken);
        return Ok(report);
    }

    [HttpGet]
    [Route("shared_with/{userId:guid}")]
    public async Task<ActionResult<Report>> GetReportsSharedWithUser([FromRoute] Guid userId)
    {
        var requestSenderId = User.GetUserId();
        var report = await _serviceManager.ReportService.ReportSharedWithUserAsync(requestSenderId, userId);
        return Ok(report);
    }
}