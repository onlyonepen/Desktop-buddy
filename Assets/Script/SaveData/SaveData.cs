using System.IO;
using UnityEngine;
using VInspector;

public class SaveData : MonoBehaviour
{
    private static readonly string SavePath = Path.Combine(Application.persistentDataPath, "EventData.json");
    public static DailyLayer SaveCache;
    public static EventData CurrentEvent;
    public static int CurrentSlot;


    public static void Init()
    {
        if(!File.Exists(SavePath))
        {
            Debug.Log("No save found. Creating new save data.");
            SaveCache = new DailyLayer();
            SaveFile();
        }
        else
        {
            string json = File.ReadAllText(SavePath);
            Debug.Log("Get saved file from: " + SavePath);
            SaveCache = JsonUtility.FromJson<DailyLayer>(json);
        }
    }

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

        SaveCache.EventList[CurrentSlot] = CurrentEvent;
    }

    public static EventData CreateEvent(EventData data, int slot)
    {
        SaveCache.EventList.Insert(slot, data);

        CurrentSlot = slot;
        CurrentEvent = data;

        Debug.Log("Create event: " + slot);
        SaveFile();
        return CurrentEvent;
    }

    public static void DeleteEvent(int slot)
    {
        SaveCache.EventList[slot] = new EventData();
        CurrentEvent = null;
        Debug.Log("Delete event: " + slot);
        SaveFile();
    }

    public static DailyLayer LoadSavedEvent()
    {
        if (!File.Exists(SavePath)) return null;

        string json = File.ReadAllText(SavePath);
        DailyLayer loadedData = JsonUtility.FromJson<DailyLayer>(json);
        SaveCache = loadedData;

        return loadedData;
    }

    public static void ClearAllEvent()
    {
        File.Delete(SavePath);
    }
}
