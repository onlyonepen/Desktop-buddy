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
    String EventName;
    Color EventColor;
    Sprite EventImage;
    Sprite EventIcon;
    int EventStartTime;
    int EventEndTime;
    string EventDetail;
}