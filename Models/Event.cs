using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTrackerApp.Models;

public class Event
{
    public int ID { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public string? Details { get; set; }

    //TODO - Need to add time duration data and a way to calculate it
}
