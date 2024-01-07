#nullable disable

using Microsoft.EntityFrameworkCore;


public class SleepContext : DbContext
{
  public SleepContext(DbContextOptions<SleepContext> options) : base(options) { }

  public DbSet<SleepRecord> SleepRecords { get; set; }
}
