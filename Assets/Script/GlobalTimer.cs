using System;
using UnityEngine;

public class GlobalTimer : MonoBehaviour
{
    public static GlobalTimer Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public event Action OnMinutesCheck;

    int timestamp = 61;

    private void Update()
    {
        if (timestamp != DateTime.Now.Minute)
        {
            OnMinutesCheck?.Invoke();
            timestamp = DateTime.Now.Minute;
        }
    }
}
