using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FingerJarHover : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private GameObject[] numberObjects;
    private int currentFingers;
    [SerializeField] private GameObject fingerTextObject;
    private MeshRenderer mesh;
    public Color hoverEmissionColor;
    public float hoverEmissionIntensity;


    void OnEnable()
    {
        ItemShopButtonInteraction.OnItemPurchased += UpdateFingerCount;
    }
    void OnDisable()
    {
        ItemShopButtonInteraction.OnItemPurchased -= UpdateFingerCount;
    }

    void Awake()
    {
        currentFingers = saveData.monsterFingers;
    }
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        fingerTextObject.SetActive(false);
    }
    void OnMouseEnter()
    {
        UpdateFingerCount();
        mesh.material.SetColor("_EmissiveColor", hoverEmissionColor * hoverEmissionIntensity); //highlight item

        fingerTextObject.SetActive(true); //turn on finger text

        //turn on number of fingers text
        switch (currentFingers)
        {
            case 0:
                //turn on number 0
                numberObjects[0].SetActive(true);
                break;
            case 1:
                //turn on number 1
                numberObjects[1].SetActive(true);
                break;
            case 2:
                //turn on number 2
                numberObjects[2].SetActive(true);
                break;
            case 3:
                //turn on number 3
                numberObjects[3].SetActive(true);
                break;
            case 4:
                //turn on number 4
                numberObjects[4].SetActive(true);
                break;
            case 5:
                //turn on number 5
                numberObjects[5].SetActive(true);
                break;
            case 6:
                //turn on number 6
                numberObjects[6].SetActive(true);
                break;
            case 7:
                //turn on number 7
                numberObjects[7].SetActive(true);
                break;
            case 8:
                //turn on number 8
                numberObjects[8].SetActive(true);
                break;
            case 9:
                //turn on number 9
                numberObjects[9].SetActive(true);
                break;
            case 10:
                //turn on number 10
                numberObjects[10].SetActive(true);
                break;
            default:
                break;
        }
    }
    void OnMouseExit()
    {
        mesh.material.SetColor("_EmissiveColor", Color.black); //unhighlight item
        fingerTextObject.SetActive(false); //turn off text object
        //turn on number of fingers text
        switch (currentFingers)
        {
            case 0:
                //turn on number 0
                numberObjects[0].SetActive(false);
                break;
            case 1:
                //turn on number 1
                numberObjects[1].SetActive(false);
                break;
            case 2:
                //turn on number 2
                numberObjects[2].SetActive(false);
                break;
            case 3:
                //turn on number 3
                numberObjects[3].SetActive(false);
                break;
            case 4:
                //turn on number 4
                numberObjects[4].SetActive(false);
                break;
            case 5:
                //turn on number 5
                numberObjects[5].SetActive(false);
                break;
            case 6:
                //turn on number 6
                numberObjects[6].SetActive(false);
                break;
            case 7:
                //turn on number 7
                numberObjects[7].SetActive(false);
                break;
            case 8:
                //turn on number 8
                numberObjects[8].SetActive(false);
                break;
            case 9:
                //turn on number 9
                numberObjects[9].SetActive(false);
                break;
            case 10:
                //turn on number 10
                numberObjects[10].SetActive(false);
                break;
            default:
                break;
        }
    }

    void UpdateFingerCount()
    {
        currentFingers = saveData.monsterFingers; //update save data
    }
}
