using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseKeyboardController
{
    //The controller class used for mouse control

    Vector3 m_worldPos;
    bool m_down;
    bool m_up;

    void Start()
    {
        m_down = false;
        m_worldPos = Vector3.zero;
    }

    public void UpdateController()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        m_worldPos = position;

        m_down = Input.GetMouseButton(0);
        m_up = !Input.GetMouseButton(0);
    }

    public bool GetPressedDown()
    {
        return m_down;
    }

    public bool GetReleased()
    {
        return m_up;
    }


    public Vector3 GetPositionInWorld()
    {
        return m_worldPos;
    }
}
