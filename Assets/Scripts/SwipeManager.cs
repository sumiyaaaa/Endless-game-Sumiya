using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeManager : MonoBehaviour
{
    public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    private void Update()
    {
        tap = swipeDown = swipeUp = swipeLeft = swipeRight = false;

        #region Standalone Inputs
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            tap = true;
            isDraging = true;
            startTouch = Mouse.current.position.ReadValue();
        }
        else if (Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Input
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                tap = true;
                isDraging = true;
                startTouch = Touchscreen.current.primaryTouch.position.ReadValue();
            }
        }
        else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
        {
            isDraging = false;
            Reset();
        }
        #endregion

        //Calculate the distance
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
                swipeDelta = Touchscreen.current.primaryTouch.position.ReadValue() - startTouch;
            else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
                swipeDelta = Mouse.current.position.ReadValue() - startTouch;
        }

        //Did we cross the distance?
        if (swipeDelta.magnitude > 100)
        {
            //Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                //Up or Down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}