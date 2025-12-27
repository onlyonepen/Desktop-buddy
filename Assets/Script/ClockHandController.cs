using UnityEngine;

public class ClockHandController : MonoBehaviour
{
    [SerializeField] public Transform HrHand;
    [SerializeField] public Transform MinHand;

    private void FixedUpdate()
    {
        int hr = System.DateTime.Now.Hour;
        int Min = System.DateTime.Now.Minute;

        float hrAngle = (hr * 30) + (Min * 0.5f);
        float minAngle = (Min * 6);

        HrHand.localRotation = Quaternion.Euler(new Vector3(0, 0, -hrAngle));
        MinHand.localRotation = Quaternion.Euler(new Vector3(0, 0, -minAngle));
    }
}
