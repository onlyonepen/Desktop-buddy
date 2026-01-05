using UnityEngine;

public class ClockHandController : MonoBehaviour
{
    [SerializeField] public Transform HrHand;
    [SerializeField] public Transform MinHand;

    private void Awake()
    {
        GlobalTimer.Instance.OnMinutesCheck += OnMinutesTick;
    }

    private void OnMinutesTick()
    {
        int hr = System.DateTime.Now.Hour;
        int Min = System.DateTime.Now.Minute;

        float hrAngle = (hr * 30) / 2 + (Min * 0.25f);
        float minAngle = (Min * 6);

        HrHand.localRotation = Quaternion.Euler(new Vector3(0, 0, -hrAngle));
        MinHand.localRotation = Quaternion.Euler(new Vector3(0, 0, -minAngle));
    }
}
