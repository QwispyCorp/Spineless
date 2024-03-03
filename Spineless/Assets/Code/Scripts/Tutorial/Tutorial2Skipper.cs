using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tutorial2Skipper : MonoBehaviour
{

    [SerializeField] private EncounterRoomStartup encounterStarter;
    [SerializeField] private GameObject dotCanvas;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.StopSound("Tutorial2");

            dotCanvas.SetActive(true);

            encounterStarter.StopAllCoroutines();

            gameObject.SetActive(false);
        }
    }
}
