using System.Collections;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator CamAnimation;
    public void Free2Game()
    {
        StartCoroutine(AnimationCoroutine("Free2GameAnimation"));
    }

    public void Game2Free()
    {
        StartCoroutine(AnimationCoroutine("Game2FreeAnimation"));
    }

    public void Free2Cabinet()
    {
        StartCoroutine(AnimationCoroutine("Free2CabinetAnimation"));
    }

    public void Cabinet2Free()
    {
        StartCoroutine(AnimationCoroutine("Cabinet2FreeAnimation"));
    }


    private IEnumerator AnimationCoroutine(string animationName)
    {
        switch (animationName)
        {
            case "Free2GameAnimation":
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                CamAnimation.SetTrigger("Free2Board");
                break;

            case "Game2FreeAnimation":
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                CamAnimation.SetTrigger("Board2Free");
                break;

            case "Free2CabinetAnimation":
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                CamAnimation.SetTrigger("Free2Cabinet");
                break;

            case "Cabinet2FreeAnimation":
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                CamAnimation.SetTrigger("Cabinet2Free");
                break;

            default:
                Debug.LogWarning("Animation not found: " + animationName);
                break;
        }

        yield return new WaitForSeconds(1.5f);
        // Add Lock state logic here
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        yield return null;
    }
}
