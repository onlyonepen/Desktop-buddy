using UnityEngine;
using VInspector;

public class SaveTest : MonoBehaviour
{
    [Button]
    public void Init()
    {
        SaveData.Init();
    }

    [Button]
    public void CreateEvent(string name, int slot)
    {
        EventData newEvent = new EventData();
        newEvent.EventName = name;

        SaveData.CreateEvent(newEvent, slot);
    }

    [Button]
    public void LoadSaveName(int slot)
    {
        Debug.Log(SaveData.LoadSavedEvent().EventDatas[slot].EventName);
    }

    [Button]
    public void CheckSaveSize()
    {
        Debug.Log(SaveData.LoadSavedEvent().EventDatas.Capacity);
    }

    [Button]
    public void ClearAllSavedEvent()
    {
        SaveData.ClearAllEvent();
    }
}
