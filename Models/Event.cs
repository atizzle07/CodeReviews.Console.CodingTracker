using System.ComponentModel;

namespace CodingTrackerApp.Models;

public class Event
{
    public int ID { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public string? Details { get; set; }
    public decimal? DurationMinutes
    {
        get
        {
            if (EndTime != null)
            {
                DateTime _startTime = Convert.ToDateTime(StartTime);
                DateTime _endTime = Convert.ToDateTime(EndTime);
                TimeSpan _duration = _endTime - _startTime;
                return Math.Round((decimal)_duration.TotalMinutes,2);
            }
            return null;
        }
    }
}
