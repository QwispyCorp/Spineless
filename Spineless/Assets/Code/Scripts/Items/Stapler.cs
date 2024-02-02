using UnityEngine;

public class Stapler : MonoBehaviour, Interactable
{
    public void Interact()
    {
        PlayerHealthTest.Instance.ChangeHealth(1);
        AudioManager.Instance.PlaySound("Stapler");
        Debug.Log("Item Worked");
        gameObject.SetActive(false);
    }
}
