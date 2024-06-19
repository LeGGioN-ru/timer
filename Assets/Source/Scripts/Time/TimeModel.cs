using System;

public class TimeModel
{
    public DateTime Time { get; private set; }

    public void ChangeTime(DateTime newDateTime)
    {
        Time = newDateTime;
    }
}
