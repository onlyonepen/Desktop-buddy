using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class DailyTimelineHandController : MonoBehaviour
{
    [SerializeField] Transform StartingPoint;
    [SerializeField] Transform EndingPoint;
    [SerializeField] Transform NowMarker;
    [SerializeField] TextMeshProUGUI NowText;

    public float timelineLength;
    private void Awake()
    {
        timelineLength = EndingPoint.localPosition.x - StartingPoint.localPosition.x;
        GlobalTimer.Instance.OnMinutesCheck += OnMinutesTick;
    }

    private void OnMinutesTick()
    {
        int hr = DateTime.Now.Hour;
        int Min = DateTime.Now.Minute;

        float minutesPassTillNowRatio = (float)DateTime.Now.TimeOfDay.TotalMinutes / 1440f;
        float nowXPos = timelineLength * minutesPassTillNowRatio;

        NowMarker.localPosition = new Vector3(nowXPos - timelineLength / 2, NowMarker.localPosition.y);

        NowText.text = DateTime.Now.ToString("HH:mm");
    }
}
