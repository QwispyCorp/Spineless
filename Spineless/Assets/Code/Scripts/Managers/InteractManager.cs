using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractManager : MonoBehaviour
{
    public LayerMask interactLayer;
    public float interactionDistance = 2f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "Encounter")
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactLayer))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
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
