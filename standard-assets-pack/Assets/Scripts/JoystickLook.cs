﻿using System; 
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class JoystickLook : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {

        //PLEASE
        //PLEASE
        //PLEASE

            //dont forget to go into mouselook and turn your screen lock function back on
            //then it was also called in the fpscontroller once

        public enum AxisOption  
        {
            // Options for which axes to use
            Both, // Use both
            OnlyHorizontal, // Only horizontal
            OnlyVertical // Only vertical
        }

        public int MovementRange = 100;
        public AxisOption axesToUse = AxisOption.Both; 
        public string horizontalAxisName = "LookVertical"; 
        public string verticalAxisName = "LookHorizontal"; 

        Vector3 m_StartPos;
        bool m_UseX; 
        bool m_UseY; 

        //remember these are new variables
        CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; 
        CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis;

        void OnEnable()
        {
            CreateVirtualAxes();  
        }

        void Start()
        {
            m_StartPos = transform.position;  
        }

        void UpdateVirtualAxes(Vector3 value)  
        {
            var delta = m_StartPos - value; 
            delta.y = -delta.y;  
            delta /= MovementRange;  
            if (m_UseX) 
            {
                m_HorizontalVirtualAxis.Update(-delta.x);
            }

            if (m_UseY) 
            {
                m_VerticalVirtualAxis.Update(delta.y);
            }
        }  

        void CreateVirtualAxes()  
        {
            // set axes to use
            m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);  
            m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

            if (m_UseX)
            {
                m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);  
                CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
            }
            if (m_UseY)
            {
                m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
                CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
            }
        }


        public void OnDrag(PointerEventData data)  
        {
            Vector3 newPos = Vector3.zero;

            if (m_UseX)
            {
                int delta = (int)(data.position.x - m_StartPos.x);
                delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
                newPos.x = delta;
            }

            if (m_UseY)
            {
                int delta = (int)(data.position.y - m_StartPos.y);
                delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
                newPos.y = delta;
            }
            transform.position = new Vector3(m_StartPos.x + newPos.x, m_StartPos.y + newPos.y, m_StartPos.z + newPos.z);
            UpdateVirtualAxes(transform.position);
        }


        public void OnPointerUp(PointerEventData data)
        {
            transform.position = m_StartPos;  
            UpdateVirtualAxes(m_StartPos);
        }


        public void OnPointerDown(PointerEventData data) { }  

        void OnDisable()
        {
            if (m_UseX)
            {
                m_HorizontalVirtualAxis.Remove();
            }
            if (m_UseY)
            {
                m_VerticalVirtualAxis.Remove();
            }
        }
    }
}
