using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class DailyData
{
    public List<DailyLayer> LayerList = new List<DailyLayer>();
    public int SelectedLayer = 0;
}

[Serializable]
public class DailyLayer
{
    public string LayerName;
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