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

    public static string ExtractAnswer(string message)
    {
        string[] parts = message.Split(':');  

        string answer = parts[1].Trim();

        return answer;
    }

    public static string ExtractCallState(string message)
    {
        string inputString = "CallState: Start, Contact";

        string[] parts = inputString.Split(':');
        string callStatePart = parts[1].Trim();
        string[] callStateParts = callStatePart.Split(',');

        string state = callStateParts[0].Trim();
        string contact = callStateParts[1].Trim();

        return state;
    }
}
