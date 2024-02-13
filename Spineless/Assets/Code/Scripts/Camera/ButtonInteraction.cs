using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonInteraction : MonoBehaviour
{
    public LayerMask buttonLayer; // Layer containing objects tagged as "Button"

    private GameObject currentButton; // Currently hovered button

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // Cast a ray from the camera's position forward
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, buttonLayer))
            {
                GameObject hitObject = hit.collider.gameObject;

                // Check if the hit object is tagged as "Button"
                if (hitObject.CompareTag("Button"))
                {
                    // Trigger onEnter event if the button is different from the current one
                    if (hitObject != currentButton)
                    {
                        ExecuteOnEnter(hitObject);
                        currentButton = hitObject;
                    }

                    // Left mouse button click
                    if (Input.GetMouseButtonDown(0))
                    {
                        ExecuteOnClick(hitObject);
                    }
                }
            }
            else
            {
                // If the ray doesn't hit any button, trigger onExit event
                if (currentButton != null)
                {
                    ExecuteOnExit(currentButton);
                    currentButton = null;
                }
            }
        }
    }

    void ExecuteOnEnter(GameObject button)
    {
        // Call the PointerEnter event on the button's EventTrigger component
        ExecuteEvents.Execute(button, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
    }

    void ExecuteOnClick(GameObject button)
    {
        // Call the PointerClick event on the button's EventTrigger component
        ExecuteEvents.Execute(button, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
    }

    void ExecuteOnExit(GameObject button)
    {
        // Call the PointerExit event on the button's EventTrigger component
        ExecuteEvents.Execute(button, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
    }
}
