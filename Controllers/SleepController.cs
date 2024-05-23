using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("/api/[controller]")]
public class SleepController : ControllerBase
{
  private readonly SleepContext _context;

  public SleepController(SleepContext context)
  {
    _context = context;
  }

  private void ApplyCorsHeaders()
  {
    Response.Headers.Add("Access-Control-Allow-Origin", "*");
    Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
    Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
  }


  // [EnableCors("MyCorsPolicy")]
  [HttpOptions]
  [Route("start")]
  public IActionResult Options()
  {
    ApplyCorsHeaders();
    return Ok(); // Return a minimal response with a 200 OK status.
  }
  [HttpPost]
  [Route("start")]
  public IActionResult StartSleeping()
  {
    ApplyCorsHeaders();
    var sleepRecord = new SleepRecord
    {
      // UserId = "exampleUser",
      StartTime = DateTime.Now,
      EndTime = DateTime.Now,
      WakeUpCount = 0
    };

    _context.SleepRecords.Add(sleepRecord);
    _context.SaveChanges();

    return Ok(new { Message = "Sleep timer started." });
  }

  // [HttpGet]
  // [Route("start")]
  // public IActionResult StartSleepingData()
  // {
  //   ApplyCorsHeaders();

  //   return Ok(new { Message = "Sleep timer started." });
  // }

  [HttpPost]
  [Route("pause")]

  public IActionResult PauseSleeping()
  {
    ApplyCorsHeaders();
    var activeRecord = _context.SleepRecords
         .Where(record =>
        //  record.UserId == "exampleUser" && 
        record.EndTime == record.StartTime)
         .OrderByDescending(record => record.StartTime)
         .FirstOrDefault();

    if (activeRecord != null)
    {
      activeRecord.PauseTime = DateTime.Now;
      activeRecord.WakeUpCount++;
      _context.SaveChanges();
    }
    return Ok(new { Message = "Sleep Timer Paused." });
  }

  [HttpPost]
  [Route("stop")]

  public IActionResult StopSleeping()
  {
    ApplyCorsHeaders();
    var activeRecord = _context.SleepRecords
               .Where(record =>
               //  record.UserId == "exampleUser" && 
               record.EndTime == record.StartTime)
               .OrderByDescending(record => record.StartTime)
               .FirstOrDefault();

    if (activeRecord != null)
    {
      activeRecord.EndTime = DateTime.Now;
      activeRecord.WakeUpCount++;
      _context.SaveChanges();
    }
    return Ok(new { Message = "Sleep Timer Stopped." });
  }

  [HttpGet]
  [Route("report")]
  public IActionResult GetSleepReport()
  {
    ApplyCorsHeaders();

    var sleepRecords = _context.SleepRecords.ToList();

    return Ok(sleepRecords);
  }


}