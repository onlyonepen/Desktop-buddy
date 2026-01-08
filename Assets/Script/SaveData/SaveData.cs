using System.IO;
using UnityEngine;
using VInspector;

public class SaveData : MonoBehaviour
{
    private static readonly string SavePath = Path.Combine(Application.persistentDataPath, "EventData.json");
    public static DailyData DailyDataCache;
    public static DailyLayer CurerentLayer;
    public static int CurrentSlot;


    public static void Init()
    {
        #region LoadDailyData
        if (!File.Exists(SavePath))
        {
            Debug.Log("No save found. Creating new save data.");
            DailyDataCache = new DailyData();
            SaveFile();
        }
        else
        {
            string json = File.ReadAllText(SavePath);
            Debug.Log("Get saved file from: " + SavePath);
            DailyDataCache = JsonUtility.FromJson<DailyData>(json);
        }
        #endregion
    }

    public static void SaveFile()
    {
        string json = JsonUtility.ToJson(DailyDataCache, true);
        
        File.WriteAllText(SavePath, json);
        Debug.Log("Saved: " + SavePath);
    }

    public static EventData CreateDailyEvent(DailyLayer layer, EventData data)
    {
        layer.EventList.Add(data);

        return data;
    }

    public static void ClearAllData()
    {
        File.Delete(SavePath);
    }
}
