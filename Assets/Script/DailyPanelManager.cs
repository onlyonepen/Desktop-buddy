using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyPanelManager : MonoBehaviour
{
    public GameObject EventPref;
    public List<GameObject> EventLayerGameobjectList;

    public Canvas MainCanvas;
    public Transform EventTransform;

    public DailyTimelineHandController timelineHandController;
    public TextMeshProUGUI TodayDateText;

    private void Start()
    {
        EventData newEvent = new EventData();
        newEvent.EventName = "TestEvent";
        newEvent.EventColor = Color.red;
        newEvent.EventStartTime = new DateTime(2011, 6, 10, 6, 30, 16);
        newEvent.EventEndTime = new DateTime(2011, 6, 10, 20, 30, 16);

        LoadEventToGameobject(newEvent);
        TodayDateText.text = DateTime.Today.ToString("dddd" + " " + "dd" + " " + "MMMM" + " " + "yyyy");
    }

    public void Init()
    {
        TodayDateText.text = DateTime.Today.ToString("dddd" + " " + "dd" + " " + "MMMM" + " " + "yyyy");

        //create EventOBJ and LayerOBJ
        foreach (DailyLayer layer in SaveData.DailyDataCache.LayerList)
        {
            GameObject newObj = new GameObject(layer.LayerName);
            newObj.transform.parent = EventTransform;
            EventLayerGameobjectList.Add(newObj);

            foreach (EventData _event in layer.EventList)
            {
                GameObject tmp = LoadEventToGameobject(_event);
                tmp.transform.parent = newObj.transform;
            }
        }

        //Disable all Layer Except selected one
        int i = 0;
        for (i = 0; i < EventLayerGameobjectList.Count; i++)
        {
            if(i != SaveData.DailyDataCache.SelectedLayer)
            {
                EventLayerGameobjectList[i].SetActive(false);
            }
        }
    }

    public GameObject LoadEventToGameobject(EventData @event)
    {
        GameObject thisEvent = Instantiate(EventPref, EventTransform);
        DailyEventElement eventElement = thisEvent.GetComponent<DailyEventElement>();
        GameObject section = eventElement.Section;
        Image IconImage = eventElement.Icon.GetComponent<Image>();

        thisEvent.transform.localScale = Vector3.one;
         
        Image _Image = section.GetComponent<Image>();
        Color _color = @event.EventColor;
        _color.a = 0.5f;
        _Image.color = _color;

        TimeSpan duration = @event.EventEndTime - @event.EventStartTime;
        float sectionWidth = (float)duration.TotalMinutes / 1440f * timelineHandController.timelineLength;
        RectTransform eventWidth = section.GetComponent<RectTransform>();
        eventWidth.sizeDelta = new Vector2(sectionWidth, eventWidth.sizeDelta.y);

        int startHr = @event.EventStartTime.Hour;
        int startMin = @event.EventStartTime.Minute;
        int startTotalMin = (startHr * 60) + startMin;
        float sectionPos = startTotalMin / 1440f * timelineHandController.timelineLength + (sectionWidth / 2) - (timelineHandController.timelineLength / 2);
        thisEvent.transform.localPosition = Vector2.right * sectionPos;

        //TODO: ApplyIcon
        //IconImage.sprite = @event.EventImage;
        IconImage.color = @event.EventColor;
        IconImage.transform.rotation = Quaternion.Euler(Vector3.zero);

        return thisEvent;
    }
}
