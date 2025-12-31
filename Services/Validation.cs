using System.Globalization;

namespace CodingTrackerApp.Services;

public class Validation
{
    public static bool isValidID(int input)
    {
        List<int> records = DataConnection.GetAllRecordIDs();

        if (records.Contains(input))
            return true;
        else
            return false;
    }

    internal static bool IsValidDatePair(string startTime, string endTime)
    {
        DateTime? _startTime;
        DateTime? _endTime;
        if (DateTime.TryParseExact(startTime, "MM-dd-yyyy-HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedStartDate))
            _startTime = parsedStartDate;
        else _startTime = null;

        if (DateTime.TryParseExact(endTime, "MM-dd-yyyy-HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedEndDate))
            _endTime = parsedEndDate;
        else _endTime = null;

        if (_startTime != null || _endTime != null)
        {
            if (_endTime > _startTime)
                return false;
        }
        else return true;
    }
}
