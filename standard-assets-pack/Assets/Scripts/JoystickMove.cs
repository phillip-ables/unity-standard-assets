using System;  // how is it not using System, this seems impossible
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class JoystickMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public enum AxisOption  // what the fuck is an enum
        {
            // Options for which axes to use
            Both, // Use both
            OnlyHorizontal, // Only horizontal
            OnlyVertical // Only vertical
        }

        public int MovementRange = 100;
        public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
        public string horizontalAxisName = "LookHorizontal"; // The name given to the horizontal axis for the cross platform input
        public string verticalAxisName = "LookVertical"; // The name given to the vertical axis for the cross platform input

        Vector3 m_StartPos;
        bool m_UseX; // Toggle for using the x axis
        bool m_UseY; // Toggle for using the Y axis
        CrossPlatformInputManager.VirtualAxis m_lookHorizontalVirtualAxis; // Reference to the joystick in the cross platform input
        CrossPlatformInputManager.VirtualAxis m_lookVerticalVirtualAxis; // Reference to the joystick in the cross platform input

        void OnEnable()
        {
            CreateVirtualAxes();  // this is so janky, like the whole hey lets make our own update method in android thing
        }

        void Start()
        {
            m_StartPos = transform.position;  // how is there a rigid  body attached to any of this, is this why my guy moved even though he had the fps script, i doubt it becuase how did the footsteps work if they run on isWalk which would have to be triggered
        }

        void UpdateVirtualAxes(Vector3 value)  // this is man made
                                               //this is called on drag and on pointer down
        {
            var delta = m_StartPos - value; // almost like fps but for position
            delta.y = -delta.y;  // invert y, same as mouse scroll
            delta /= MovementRange;  // dont fully understand this logic
            if (m_UseX)  // see now these bools become so cool, if is both directions or x
            {
                m_lookHorizontalVirtualAxis.Update(-delta.x);
            }

            if (m_UseY)  // if is both or just y, if both then we done got the x
            {
                m_lookVerticalVirtualAxis.Update(delta.y);
            }
        }  // that is literally all this class does

        void CreateVirtualAxes()  // this is called first, this is basically called on start but when its enabled
        {
            // set axes to use
            m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);  // that is so crazy that you can do this to a bool, makes sense since a bool is just a fancy if statement
            m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

            // create new axes based on axes to use
            if (m_UseX)
            // basically-> get then register, just dont get the need for cross platfrom input manager
            {
                m_lookHorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);  // dont fully get this, not even a little
                CrossPlatformInputManager.RegisterVirtualAxis(m_lookHorizontalVirtualAxis);  // aside from the reference this is the entire four lines of CrossPlatformInputManager
            }
            if (m_UseY)
            {
                m_lookVerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
                CrossPlatformInputManager.RegisterVirtualAxis(m_lookVerticalVirtualAxis);
            }
        }


        public void OnDrag(PointerEventData data)  // i have yet to see use of the i pointer down things i included in the class
                                                   // on drag get position that is used and pass it back
        {
            Vector3 newPos = Vector3.zero;

            // get the change in swipe clamped to range then put in new pos and add it to start pos

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
            // pas the start pos back to virtual axes
            transform.position = new Vector3(m_StartPos.x + newPos.x, m_StartPos.y + newPos.y, m_StartPos.z + newPos.z);
            UpdateVirtualAxes(transform.position);  // doesnt use a return type but instead calls a function to store a type, okay
                                                    // i guess this causes it to loop from this function to update axes function until its released
        }


        public void OnPointerUp(PointerEventData data)
        {
            transform.position = m_StartPos;  // so basically on click we get position and pass that current position
            UpdateVirtualAxes(m_StartPos);
        }


        public void OnPointerDown(PointerEventData data) { }  // why is this empty, why have it if its empty,
                                                              // would it crash without this? 

        void OnDisable()
        {
            // remove the joysticks from the cross platform input
            if (m_UseX)
            {
                m_lookHorizontalVirtualAxis.Remove();
            }
            if (m_UseY)
            {
                m_lookVerticalVirtualAxis.Remove();
            }
        }
    }
}
