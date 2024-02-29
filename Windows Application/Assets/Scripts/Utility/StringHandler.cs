using System.Text.RegularExpressions;
using UnityEngine;

public class StringHandler : MonoBehaviour
{
    public static Vector3 ExtractGyroVector(string message, string name)
    {
        string pattern = name + @"\((-?\d+\.\d+), (-?\d+\.\d+), (-?\d+\.\d+)\)";
        Match match = Regex.Match(message, pattern, RegexOptions.IgnoreCase);

        if (match.Success)
        {
            float x = float.Parse(match.Groups[1].Value) / 100;
            float y = float.Parse(match.Groups[2].Value) / 100;
            float z = float.Parse(match.Groups[3].Value) / 100;

            return new Vector3(x, y, z);
        }
        else
        {
            Debug.LogError("Failed to extract vector for " + name);
            return Vector3.zero;
        }
    }

    public static Timeframe ExtractTime(string message)
    {
        string[] timeParts = message.Split(':');

        int hours = int.Parse(timeParts[1]);
        int minutes = int.Parse(timeParts[2]);

        return new Timeframe(hours, minutes);
    }
}
