using System;

[Serializable]
public class TimeJson 
{
    public string utc_datetime;
    public DateTime UtcDateTime => DateTime.Parse(utc_datetime);
}
