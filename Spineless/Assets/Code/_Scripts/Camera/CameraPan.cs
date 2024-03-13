using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class CameraPan : MonoBehaviour
{
    public float senseX;
    public float senseY;
    private float mouseX;
    private float mouseY;
    public Transform orientation;
    float xRotation;
    float yRotation;
    private float _senseX; //to store sense to revert after animation ends
    private float _senseY; //to store sense to revert after animation ends
    private bool playerLookingUp;
    private bool playerLookingMid;
    private bool playerLookingCenter;
    [SerializeField] private float camLookUpSpeed; //speed the player looks up at the enemy 

    void OnEnable()
    {
        EnemyHealthTest.OnEnemyFingerLost += PlayerStopLookUp;
        PlayerHealthTest.OnPlayerFingerLost += PlayerLookMid;
        PlayerAnimationTrigger.OnAnimationFinished += StartCamReset;
        EnemyHealthTest.OnEnemyFingerLost += PlayerStopLookMid;
        EnemyHealthTest.OnEnemyFingerLost += PlayerLookUp;
        EnemyAnimationTrigger.OnEnemyAnimationFinished += StartCamReset;
    }

    void OnDisable()
    {
        EnemyHealthTest.OnEnemyFingerLost -= PlayerStopLookUp;
        PlayerHealthTest.OnPlayerFingerLost -= PlayerLookMid;
        PlayerAnimationTrigger.OnAnimationFinished -= StartCamReset;
        EnemyHealthTest.OnEnemyFingerLost -= PlayerStopLookMid;
        EnemyHealthTest.OnEnemyFingerLost -= PlayerLookUp;
        EnemyAnimationTrigger.OnEnemyAnimationFinished -= StartCamReset;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //change to locked when we fix raycast interaction for items and add crosshair/ dot in center of screen
        playerLookingUp = false;
        playerLookingMid = false;
        Cursor.visible = false; //change to false when above is changed to locked

        _senseX = senseX;
        _senseY = senseY;
    }
    //--------------------------------------------------------
    void Update()
    {
        if (!playerLookingUp && !playerLookingMid)
        {
            mouseX = -Input.GetAxisRaw("Mouse X") * Time.deltaTime * senseX;
            mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senseY;

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.localRotation = Quaternion.Euler(0, yRotation, 0);

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -16f, 52f); //limit player camera vertical rotation
            yRotation = Mathf.Clamp(yRotation, -60f, 60); //limit player camera horizontal rotation


        }
        if (playerLookingCenter)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, new Quaternion(0, 0, 0, 1), camLookUpSpeed * Time.deltaTime);
            xRotation = transform.localRotation.x; //to prevent snapbacks
            yRotation = transform.localRotation.y; //to prevent snapbacks
            orientation.localRotation = Quaternion.Euler(0, yRotation, 0);
            mouseX = 0;
            mouseY = 0;
        }
        else if (playerLookingUp)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, new Quaternion(-.1f, 0, 0, 1), camLookUpSpeed * Time.deltaTime);
            xRotation = transform.localRotation.x; //to prevent snapbacks
            yRotation = transform.localRotation.y; //to prevent snapbacks
            orientation.localRotation = Quaternion.Euler(0, yRotation, 0);
            mouseX = 0;
            mouseY = 0;
        }

        else if (playerLookingMid)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, new Quaternion(0.4f, 0, 0, 1), camLookUpSpeed * Time.deltaTime);
            xRotation = transform.localRotation.x; //to prevent snapbacks
            yRotation = transform.localRotation.y; //to prevent snapback
            orientation.localRotation = Quaternion.Euler(0, yRotation, 0);
            mouseX = 0;
            mouseY = 0;
        }
    }

    private void PlayerLookUp()
    {
        playerLookingUp = true;
        senseX = _senseX;
        senseY = _senseY;
    }

    private void PlayerLookMid()
    {
        playerLookingMid = true;
        senseX = 0;
        senseY = 0;
    }
    private void PlayerStopLookMid()
    {
        playerLookingMid = false;
        senseX = _senseX;
        senseY = _senseY;
    }
    private void PlayerStopLookUp()
    {
        playerLookingUp = false;
        senseX = _senseX;
        senseY = _senseY;
    }

    private void PlayerLookCenter()
    {
        playerLookingCenter = true;
        senseX = _senseX;
        senseY = _senseY;
    }

    private void UnlockCamera()
    {
        playerLookingMid = false;
        playerLookingUp = false;
        playerLookingCenter = false;

        xRotation = transform.localRotation.x; //to prevent snapbacks
        yRotation = transform.localRotation.y; //to prevent snapback
        senseX = _senseX;
        senseY = _senseY;
    }
    private void StartCamReset()
    {
        StartCoroutine("CamReset");
    }
    private IEnumerator CamReset()
    {
        PlayerLookCenter();
        yield return new WaitForSeconds(1.3f);

        UnlockCamera();
    }
}