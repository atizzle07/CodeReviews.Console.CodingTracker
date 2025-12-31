namespace CodingTrackerApp.Services;

public class Validation
{
    public static bool IsValidId(int input)
    {
        List<int> records = DataConnection.GetAllRecordIDs();

        if (records.Contains(input))
            return true;
        else
            return false;
    }
}
