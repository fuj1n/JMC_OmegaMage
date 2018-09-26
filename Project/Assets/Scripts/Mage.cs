using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class Mage : PT_MonoBehaviour
{
    public static Mage instance;
    public const bool DEBUG = true;

    /// <summary>
    /// Time it takes to register a tap
    /// </summary>
    public float mouseTapTime = 0.1F;

    /// <summary>
    /// Distance in pixels for mouse drag
    /// </summary>
    public float mouseDragDistance = 5;

    /// <summary>
    /// How much of the screen should be used
    /// </summary>
    public float activeScreenWidth = 1;

    // public bool _______________; // ???

    public MousePhase mousePhase = MousePhase.IDLE;
    public List<MouseInfo> mouseInfoBuffer = new List<MouseInfo>();

    public MouseInfo lastMouseInfo
    {
        get
        {
            if (mouseInfoBuffer.Count == 0) return null;
            return mouseInfoBuffer[mouseInfoBuffer.Count - 1];
        }
    }

    private void Awake()
    {
        instance = this;
        mousePhase = MousePhase.IDLE;
    }

    private void Update()
    {
        // Input handling
        bool b0Down = Input.GetMouseButtonDown(0);
        bool b0Up = Input.GetMouseButtonUp(0);

        bool inActiveArea = Input.mousePosition.x / Screen.width < activeScreenWidth;

        switch (mousePhase)
        {
            case MousePhase.IDLE:
                if (inActiveArea && b0Down)
                {
                    mouseInfoBuffer.Clear();
                    AddMouseInfo();

                    if (mouseInfoBuffer[0].hit)
                    {
                        MouseDown();
                        mousePhase = MousePhase.DOWN;
                    }
                }
                break;
            case MousePhase.DOWN:
                MouseInfo lastMouseInfo = AddMouseInfo();
                if (b0Up)
                {
                    MouseTap();
                    mousePhase = MousePhase.IDLE;
                }
                else if (Time.time - mouseInfoBuffer[0].time > mouseTapTime)
                {
                    float dragDist = (lastMouseInfo.screenPos - mouseInfoBuffer[0].screenPos).magnitude;
                    if (dragDist >= mouseDragDistance)
                        mousePhase = MousePhase.DRAG;
                }
                break;
            case MousePhase.DRAG:
                AddMouseInfo();
                if (b0Up)
                {
                    MouseDragUp();
                    mousePhase = MousePhase.IDLE;
                }
                else
                {
                    MouseDrag();
                }
                break;
        }
    }

    private MouseInfo AddMouseInfo()
    {
        MouseInfo info = new MouseInfo();
        info.screenPos = Input.mousePosition;
        info.position = Utils.mouseLoc;
        info.ray = Utils.mouseRay;

        info.time = Time.time;
        info.Raycast();


        if (mouseInfoBuffer.Count == 0 || info.time != mouseInfoBuffer[mouseInfoBuffer.Count - 1].time)
            mouseInfoBuffer.Add(info);

        return info;
    }

    private void MouseDown()
    {
        this.ConditionalLog(DEBUG, "Mage.MouseDown()");
    }

    private void MouseTap()
    {
        this.ConditionalLog(DEBUG, "Mage.MouseTap()");
    }

    private void MouseDrag()
    {
        this.ConditionalLog(DEBUG, "Mage.MouseDrag()");
    }

    private void MouseDragUp()
    {
        this.ConditionalLog(DEBUG, "Mage.MouseDragUp()");
    }
}

/// <summary>
/// Used to track mouse interaction phase
/// </summary>
public enum MousePhase
{
    IDLE,
    DOWN,
    DRAG
}

/// <summary>
/// Stores information about the mouse in each frame of interaction
/// </summary>
[System.Serializable]
public class MouseInfo
{
    public Vector3 position;
    public Vector3 screenPos;
    public Ray ray;
    public float time;

    public RaycastHit hitInfo;
    public bool hit;

    /// <returns>True if the mouse ray hits anything</returns>
    public RaycastHit Raycast()
    {
        hit = Physics.Raycast(ray, out hitInfo);
        return hitInfo;
    }

    /// <param name="mask">The layer mask for this raycast</param>
    /// <returns>True if the mouse ray hits anything</returns>
    public RaycastHit Raycast(int mask)
    {
        hit = Physics.Raycast(ray, out hitInfo, mask);
        return hitInfo;
    }
}
