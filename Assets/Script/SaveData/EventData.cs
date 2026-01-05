using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class DailyData
{
    public List<DailyLayer> EventList = new List<DailyLayer>();
}

[Serializable]
public class DailyLayer
{
    public string LayerName;
    public bool IsVisible;
    public List<EventData> EventList = new List<EventData>();
}

[Serializable]
public class EventData
{
    public string EventName;
    public Color EventColor;
    public Sprite EventImage;
    public Sprite EventIcon;
    public DateTime EventStartTime;
    public DateTime EventEndTime;
    public string EventDetail;
}