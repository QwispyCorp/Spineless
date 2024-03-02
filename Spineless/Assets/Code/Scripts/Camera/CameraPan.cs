using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class CameraPan : MonoBehaviour
{
    public float senseX;
    public float senseY;
    public Transform orientation;
    float xRotation;
    float yRotation;
    private float _senseX; //to store sense to revert after animation ends
    private float _senseY; //to store sense to revert after animation ends
    private bool playerLookingUp;
    [SerializeField] private float camLookUpSpeed; //speed the player looks up at the enemy 

    void OnEnable()
    {
        StateManager.OnEnemyTurnStarted += PlayerLookUp;
        EnemyCardInteraction.OnEnemyTurnFinished += UnlockCamera;
        EnemyHealthTest.OnEnemyFingerLost += PlayerLookUp;
        EnemyAnimationTrigger.OnEnemyAnimationFinished += UnlockCamera;
    }

    void OnDisable()
    {
        StateManager.OnEnemyTurnStarted -= PlayerLookUp;
        EnemyCardInteraction.OnEnemyTurnFinished -= UnlockCamera;
        EnemyHealthTest.OnEnemyFingerLost -= PlayerLookUp;
        EnemyAnimationTrigger.OnEnemyAnimationFinished -= UnlockCamera;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //change to locked when we fix raycast interaction for items and add crosshair/ dot in center of screen
        playerLookingUp = false;
        Cursor.visible = false; //change to false when above is changed to locked

        _senseX = senseX;
        _senseY = senseY;
    }
    //--------------------------------------------------------
    void Update()
    {
        if (!playerLookingUp)
        {
            float mouseX = -Input.GetAxisRaw("Mouse X") * Time.deltaTime * senseX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senseY;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -16f, 52f); //limit player camera vertical rotation
            yRotation = Mathf.Clamp(yRotation, -60f, 60); //limit player camera horizontal rotation

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.localRotation = Quaternion.Euler(0, yRotation, 0);
        }
        if (playerLookingUp)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, new Quaternion(0, 0, 0, 1), camLookUpSpeed * Time.deltaTime);
            xRotation = transform.localRotation.x; //to prevent snapbacks
            yRotation = transform.localRotation.y; //to prevent snapbacks
        }

    }

    private void PlayerLookUp()
    {
        playerLookingUp = true;
        senseX = 0;
        senseY = 0;
    }

    private void UnlockCamera()
    {
        playerLookingUp = false;
        senseX = _senseX;
        senseY = _senseY;
    }
}