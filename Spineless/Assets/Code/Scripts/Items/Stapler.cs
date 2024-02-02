using UnityEngine;

public class Stapler : MonoBehaviour, Interactable
{
    public void Interact()
    {
        PlayerHealthTest.Instance.ChangeHealth(1);
        Debug.Log("Item Worked");
        gameObject.SetActive(false);
    }
}
