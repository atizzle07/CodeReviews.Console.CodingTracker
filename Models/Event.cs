using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTrackerApp.Models;

internal class Event
{
    public int ID { get; set; }
    public string StartTime { get; set; }

    //TODO - NEED TO MAKE SURE THAT CREATED OBJECTS HAVE THE CORRECT DATE FORMAT
    public string? EndTime { get; set; }
    public string? Details { get; set; }


}


