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

  [HttpPost]
  [Route("start")]
  public IActionResult StartSleeping()
  {
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

  [HttpPost]
  [Route("pause")]

  public IActionResult PauseSleeping()
  {
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


}