public class SleepRecord
{
  public int Id { get; set; }
  // public string? UserId { get; set; }
  public DateTime StartTime { get; set; }
  public DateTime PauseTime { get; set; }
  public DateTime EndTime { get; set; }
  public int WakeUpCount { get; set; }

}