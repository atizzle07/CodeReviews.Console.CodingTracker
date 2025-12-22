namespace CodingTrackerApp.Models;

public class Event
{
    public int ID { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public string? Details { get; set; }

    public TimeSpan? Duration
    {
        get
        {
            if (EndTime != null)
            {
                DateTime _startTime = Convert.ToDateTime(StartTime);
                DateTime _endTime = Convert.ToDateTime(EndTime);
                return _endTime - _startTime;
            }
            return null;
        }
    }
}
