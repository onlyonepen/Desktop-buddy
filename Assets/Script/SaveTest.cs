using UnityEngine;
using VInspector;

public class SaveTest : MonoBehaviour
{
    private void Awake()
    {
        SaveData.Init();
    }

    [Button]
    public void ClearAllSavedEvent()
    {
        SaveData.ClearAllData();
    }
}
