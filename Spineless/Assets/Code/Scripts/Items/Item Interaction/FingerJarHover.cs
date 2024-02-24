using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FingerJarHover : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private GameObject fingerNumberObject;
    private int currentFingers;
    [SerializeField] private GameObject fingerTextObject;
    private MeshRenderer mesh;
    public Color hoverEmissionColor;
    public float hoverEmissionIntensity;

    void OnEnable()
    {
        ItemMouseInteraction.OnItemPurchased += UpdateFingerCount; 
    }
    void OnDisable()
    {
        ItemMouseInteraction.OnItemPurchased -= UpdateFingerCount;
    }

    void Awake()
    {
        currentFingers = saveData.monsterFingers;
    }
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        fingerNumberObject.SetActive(false);
        fingerTextObject.SetActive(false);
    }
    void OnMouseEnter()
    {
        mesh.material.SetColor("_EmissiveColor", hoverEmissionColor * hoverEmissionIntensity); //highlight item
        fingerNumberObject.SetActive(true); //turn on number of fingers
        fingerNumberObject.GetComponent<TextMeshProUGUI>().SetText(currentFingers.ToString()); //update number of fingers to display
        fingerTextObject.SetActive(true); //turn on finger text
    }
    void OnMouseExit()
    {
        mesh.material.SetColor("_EmissiveColor", Color.black); //unhighlight item
        fingerNumberObject.SetActive(false);
        fingerTextObject.SetActive(false);
    }

    void UpdateFingerCount()
    {
        currentFingers = saveData.monsterFingers;
    }
}
