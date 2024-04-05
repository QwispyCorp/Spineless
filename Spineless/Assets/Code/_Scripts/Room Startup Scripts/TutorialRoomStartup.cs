using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/*when tutorial room starts:

1 delay playing of tutorial video
2 play video
3 start coroutine that's the length of the video
4 when video ends, turn off its game object
5 transition to encounter room
*/

public class TutorialRoomStartup : MonoBehaviour
{
    private float videoDuration;
    [SerializeField] private GameObject tvCanvas;
    [SerializeField] private GameObject tvStaticSound;
    [SerializeField] private VideoClip tutorialVideo;
    [SerializeField] private SubtitlePlayer tutorialSubs;
    private static TutorialRoomStartup _instance;
    public static TutorialRoomStartup Instance { get { return _instance; } } //to use any method from this manager call MenuManager.Instance."FunctionName"(); anywhere in any script
    void Awake()
    {
        //on awake check for existence of manager and handle accordingly
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        videoDuration = (float)tutorialVideo.length;
        tvCanvas.SetActive(false); //start room with tv off
        tvStaticSound.SetActive(false);
        Invoke("TurnTVOn", 1);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //skip tutorial if space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            tvCanvas.SetActive(false);
            tutorialSubs.StopSubtitles();
            LightManager.Instance.StartFlickeringTransitionTo("Encounter");
        }
    }

    //function to delay tv turning on when room is entered
    private void TurnTVOn()
    {
        tutorialSubs.PlaySubtitles();
        tvCanvas.SetActive(true);
        tvStaticSound.SetActive(true);
        StartCoroutine("turnTVOff");
    }

    //coroutine to delay tv turning off
    private IEnumerator turnTVOff()
    {
        yield return new WaitForSeconds(videoDuration);
        tvCanvas.SetActive(false);
        tvStaticSound.SetActive(false);
        tutorialSubs.StopSubtitles();
        LightManager.Instance.StartFlickeringTransitionTo("Encounter");
    }
}
