using System;

public class TimeChanged
{
    public DateTime Time { get; private set; }

    public TimeChanged(DateTime time)
    {
        Time = time;
    }
}
