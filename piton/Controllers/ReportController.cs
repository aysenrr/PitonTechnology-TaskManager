using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace piton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
public class ReportController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public ReportController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpGet("daily")]
    public ActionResult<IEnumerable<Task>> GetDailyReport()
    {
        var tasks = _dbContext.Tasks.Where(t => t.DueDate.Date == DateTime.Today.Date);
        return Ok(tasks);
    }


    [HttpGet("weekly")]
    public ActionResult<IEnumerable<Task>> GetWeeklyReport()
    {
   
        var startDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        var endDate = startDate.AddDays(6);

        var tasks = _dbContext.Tasks.Where(t => t.DueDate.Date >= startDate.Date && t.DueDate.Date <= endDate.Date);
        return Ok(tasks);
    }

    
    [HttpGet("monthly")]
    public ActionResult<IEnumerable<Task>> GetMonthlyReport()
    {
        var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var tasks = _dbContext.Tasks.Where(t => t.DueDate.Date >= startDate.Date && t.DueDate.Date <= endDate.Date);
        return Ok(tasks);
    }
}
}
