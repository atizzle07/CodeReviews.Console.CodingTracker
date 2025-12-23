using System.ComponentModel;

namespace CodingTrackerApp.Data;

public enum MenuOption
{
    [Description("Add Entry")]
    AddEntry,
    [Description("View Saved Entries")]
    ViewSavedEntries,
    UpdateEntry,
    DeleteEntry,
    Reports,
    ExitApplication
}
