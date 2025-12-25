using System.Runtime.InteropServices;
using UnityEngine;
using VInspector;

public class TaskbarCollider : MonoBehaviour
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    private const int SPI_GETWORKAREA = 0x0030;

    [DllImport("user32.dll")]
    static extern int FindWindow(int nIndex);

    [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
    public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref RECT pvParam, uint fWinIni);

    private void Awake()
    {
        CreateFloorCollider(GetTaskbarHeight());
    }

    public int GetTaskbarHeight()
    {
        if (Application.platform != RuntimePlatform.WindowsPlayer && Application.platform != RuntimePlatform.WindowsEditor)
        {
            Debug.LogWarning("Taskbar height utility only works on Windows.");
            return 0;
        }

        RECT workAreaRect = new RECT();
        bool success = SystemParametersInfo(SPI_GETWORKAREA, 0, ref workAreaRect, 0);

        if (success)
        {
            int totalScreenHeight = Screen.currentResolution.height;
            int usableHeight = workAreaRect.Bottom - workAreaRect.Top;
            int taskbarHeight = totalScreenHeight - usableHeight;

            //Debug.Log($"Total Height: {totalScreenHeight}, Usable Height: {usableHeight}, Taskbar Height: {taskbarHeight}");
            return taskbarHeight;
        }
        else
        {
            Debug.LogError("Failed to retrieve system parameters.");
            return 0;
        }
    }

    public void CreateFloorCollider(int taskbarHeight)
    {
        if (Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return; }

        var cam = Camera.main;
        if (!cam.orthographic) { Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return; }

        Vector3 taskbarheight = Vector2.up * taskbarHeight;

        var bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane) + taskbarheight);
        var topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        var bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane) + taskbarheight);

        var edge = GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : GetComponent<EdgeCollider2D>();

        var edgePoints = new[] { bottomLeft, topLeft, topRight, bottomRight, bottomLeft };
        edge.points = edgePoints;
    }

}
