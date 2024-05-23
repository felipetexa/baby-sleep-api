#nullable disable

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


public class SleepContext : DbContext
{
  public SleepContext(DbContextOptions<SleepContext> options) : base(options) { }

  public DbSet<SleepRecord> SleepRecords { get; set; }

}
