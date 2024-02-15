using UnityEngine;
using UnityEngine.SceneManagement;

public class Stapler : MonoBehaviour, Interactable
{
    public IntegerReference playerHealth;
    public IntegerReference maxPlayerHealth;
    public void Interact()
    {
        if (SceneManager.GetActiveScene().name == "Encounter")
        {
            if (playerHealth.Value < maxPlayerHealth.Value) //ifplayer is under max health, add a finger
            {
                PlayerHealthTest.Instance.ChangeHealth(1);
                AudioManager.Instance.PlaySound("Stapler");
                Debug.Log("Item Worked");
            }
            else //if player is at max health, still consume but break stapler
            {
                Debug.Log("Already at max fingers!");
                //Play broken SFX here
            }
            gameObject.SetActive(false);
        }
    }
}
