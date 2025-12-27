using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavedEventData
{
    public List<EventData> EventDatas = new List<EventData>();
}

[Serializable]
public class EventData
{
    public String EventName;
    public Color EventColor;
    public Sprite EventImage;
    public Sprite EventIcon;
    public int EventStartTime;
    public int EventEndTime;
    public string EventDetail;
}