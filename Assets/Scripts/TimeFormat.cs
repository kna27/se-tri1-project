using UnityEngine;

public class TimeFormat
{
    public static string FormatTime(float minutes, float seconds)
    {
        string addZero = "";
        if (seconds <= 9.5)
        {
            addZero = "0";
        }
        else
        {
            addZero = "";
        }
        return minutes.ToString() + ": " + addZero + Mathf.Round(seconds).ToString();
    }
}
