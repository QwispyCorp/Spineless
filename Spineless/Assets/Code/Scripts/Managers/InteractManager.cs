using System.Reflection;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public LayerMask interactLayer;

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera's position to the mouse cursor position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Perform the raycast
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactLayer))
            {
                // Check if the hit object has an Interact method
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    // Call the Interact method on the hit object using reflection
                    MethodInfo methodInfo = interactable.GetType().GetMethod("Interact");
                    if (methodInfo != null)
                    {
                        methodInfo.Invoke(interactable, null);
                    }
                }
            }
        }
    }
}
