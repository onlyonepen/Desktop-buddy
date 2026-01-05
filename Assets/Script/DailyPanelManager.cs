using System;
using UnityEngine;
using UnityEngine.UI;

public class DailyPanelManager : MonoBehaviour
{
    public GameObject EventPref;

    public Canvas MainCanvas;
    public Transform EventTransform;

    private void Start()
    {
        EventData newEvent = new EventData();
        newEvent.EventName = "TestEvent";
        newEvent.EventColor = Color.red;
        newEvent.EventStartTime = new DateTime(2011, 6, 10, 6, 30, 16);
        newEvent.EventEndTime = new DateTime(2011, 6, 10, 20, 30, 16);

        LoadEvent(newEvent);
    }

    public void Init()
    {

    }

    public GameObject LoadEvent(EventData @event)
    {
        GameObject thisEvent = Instantiate(EventPref, EventTransform);
        DailyEventElement eventElement = thisEvent.GetComponent<DailyEventElement>();
        GameObject pie = eventElement.Pie;
        Image IconImage = eventElement.Icon.GetComponent<Image>();

        thisEvent.transform.localScale = Vector3.one;
         
        Image _Image = pie.GetComponent<Image>();
        Color _color = @event.EventColor;
        _color.a = 0.5f;
        _Image.color = _color;

        TimeSpan duration = @event.EventEndTime - @event.EventStartTime;
        float _fillAmount = (((float)duration.TotalMinutes) / 720f) / 2;
        _Image.fillAmount = _fillAmount;

        int startHr = @event.EventStartTime.Hour;
        int startMin = @event.EventStartTime.Minute;
        float startAngle = ((startHr * 30) + (startMin * 0.5f)) / 2;
        float IconAngle = (startAngle + (_fillAmount * 360 / 2));
        pie.transform.Rotate(Vector3.forward * -startAngle);
        eventElement.IconParent.Rotate(Vector3.forward * -IconAngle);

        //TODO: ApplyIcon
        //IconImage.sprite = @event.EventImage;
        IconImage.color = @event.EventColor;
        IconImage.transform.rotation = Quaternion.Euler(Vector3.zero);

        return thisEvent;
    }
}
