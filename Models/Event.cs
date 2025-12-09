using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTrackerApp.Models;

internal class Event
{
    public int ID { get; set; }
    public required string StartTime { get; set; }
    public string? EndTime { get; set; }
    public string? Details { get; set; }
}
