using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class TouchableButton : MonoBehaviour
{
    private System.Action onClickAction;

    public void Initialize(System.Action onClick)
    {
        onClickAction = onClick;
    }

    void Update()
    {
        if (Touchscreen.current == null) return;

        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        foreach (var touch in activeTouches)
        {
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                HandleTouch(touch.screenPosition);
            }
        }
    }

    private void HandleTouch(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                onClickAction?.Invoke();
            }
        }
    }
}
