using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TransparentWindow : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    [DllImport("dwmapi.dll")]
    private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMargins);

    private const int GWL_EXSTYLE = -20;
    private const uint WS_EX_LAYERED = 0x00080000;
    private const uint WS_EX_TRANSPARENT = 0x00000020;
    private const uint LWA_COLORKEY = 0x00000001;
    private const uint LWA_ALPHA = 0x00000002;
    private IntPtr hWnd;

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    [StructLayout(LayoutKind.Sequential)]
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    private PointerEventData pointerEventData;
    private EventSystem eventSystem;
    private GraphicRaycaster uiRaycaster;
    public Canvas canvas;
    void Start()
    {
#if !UNITY_EDITOR
        MakeWindowTransparent();
#endif
        uint styles = GetWindowLong(hWnd, GWL_EXSTYLE);
        SetWindowLong(hWnd, GWL_EXSTYLE, styles | WS_EX_LAYERED);

        eventSystem = EventSystem.current;
        if (eventSystem == null)
        {
            GameObject es = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            eventSystem = es.GetComponent<EventSystem>();
        }

        if (canvas != null)
        {
            uiRaycaster = canvas.GetComponent<GraphicRaycaster>();
            if (uiRaycaster == null)
                uiRaycaster = canvas.gameObject.AddComponent<GraphicRaycaster>();
        }
    }

    private void Update()
    {
        bool isHoveringUI = IsPointerOverUI();
        bool isHovering2D = IsPointerOver2D();

        bool shouldClickThrough = !(isHoveringUI || isHovering2D);
        if (Input.GetKey(KeyCode.LeftAlt)) SetClickThrough(false);
        else SetClickThrough(!shouldClickThrough);
    }

    private bool IsPointerOverUI()
    {
        if (uiRaycaster == null || eventSystem == null)
            return false;

        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        uiRaycaster.Raycast(pointerData, results);
        return results.Count > 0;
    }

    private bool IsPointerOver2D()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.OverlapPoint(worldPoint) != null;
    }

    private void SetClickThrough(bool clickThrough)
    {
        uint styles = GetWindowLong(hWnd, GWL_EXSTYLE);
        if (clickThrough)
        {
            //SetWindowLong(hWnd, GWL_EXSTYLE, /*styles &*/ ~WS_EX_LAYERED);
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
        }
        else
        {
            //SetWindowLong(hWnd, GWL_EXSTYLE, /*styles |*/ WS_EX_TRANSPARENT);
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }
    }


    private void MakeWindowTransparent()
    {
        hWnd = GetActiveWindow();

        uint styles = GetWindowLong(hWnd, GWL_EXSTYLE);
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);

        MARGINS margins = new MARGINS()
        {
            cxLeftWidth = -1
        };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);

        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
    }

}
