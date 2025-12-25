using System.IO;
using UnityEngine;
using VInspector;

public class SaveData : MonoBehaviour
{
    private static readonly string SavePath = Path.Combine(Application.persistentDataPath, "EventData.json");
    public static SavedEventData SaveCache;
    public static EventData CurrentEvent;
    public static int CurrentSlot;


    public static void SaveFile()
    {
        SaveEvent();

        string json = JsonUtility.ToJson(SaveCache, true);
        File.WriteAllText(SavePath, json);

        Debug.Log("Saved: " + SavePath);
    }

    public static void SaveEvent()
    {
        if (CurrentEvent == null) return;

        SaveCache.EventDatas[CurrentSlot] = new EventData();
    }

    [Button]
    public static EventData CreateEvent(EventData data, int slot)
    {
        SaveCache.EventDatas[slot] = new EventData();

        CurrentSlot = slot;
        CurrentEvent = data;

        Debug.Log("Create event: " + slot);
        SaveFile();
        return CurrentEvent;
    }

    [Button]
    public static void DeleteEvent(int slot)
    {
        SaveCache.EventDatas[slot] = new EventData();
        CurrentEvent = null;
        Debug.Log("Delete event: " + slot);
        SaveFile();
    }
}
