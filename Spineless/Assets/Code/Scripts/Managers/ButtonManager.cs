using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerButtonInteraction : MonoBehaviour
{
    public float interactionDistance = 5f;

    private GameObject lastHitObject;

    void Update()
    {
        // Create a ray from the camera's position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast to detect objects in the scene
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // Check if the hit object has an EventTrigger component
            EventTrigger eventTrigger = hit.collider.GetComponent<EventTrigger>();
            if (eventTrigger != null)
            {
                // Check if the hit object is different from the last hit object
                if (hit.collider.gameObject != lastHitObject)
                {
                    // Handle Pointer Exit for the last hit object
                    if (lastHitObject != null)
                    {
                        ExecuteEvents.Execute(lastHitObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                    }

                    // Handle Pointer Enter for the current hit object
                    ExecuteEvents.Execute(hit.collider.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);

                    // Update the last hit object
                    lastHitObject = hit.collider.gameObject;
                }

                // Check if the mouse button is pressed
                if (Input.GetMouseButtonDown(0))
                {
                    // Simulate Pointer Click event
                    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                    ExecuteEvents.Execute(hit.collider.gameObject, pointerEventData, ExecuteEvents.pointerClickHandler);
                }
            }
        }
        else
        {
            // Handle Pointer Exit for the last hit object when there is no hit
            if (lastHitObject != null)
            {
                ExecuteEvents.Execute(lastHitObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                lastHitObject = null;
            }
        }
    }
}
