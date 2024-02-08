using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ItemHoverText : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private PlayerSaveData saveData;
    private MeshRenderer mesh;
    public Color hoverEmissionColor;
    public float hoverEmissionIntensity;
    private GameObject itemText;

    void Start()
    {
        if (GameObject.Find(itemName + " Text") != null)
        {
            itemText = GameObject.Find(itemName + " Text");
            itemText.SetActive(false);
        }
        else
        {
            itemText = null;
            Debug.Log("Could not find text object for " + itemName + ".");
        }
        mesh = GetComponent<MeshRenderer>();
    }
    private void OnMouseEnter()
    {
        mesh.material.SetColor("_Emissive", hoverEmissionColor * hoverEmissionIntensity);
        if (itemText)
        {
            itemText.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        mesh.material.SetColor("_Emissive", Color.black);
        if (itemText)
        {
            itemText.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        if (itemText)
        {
            itemText.SetActive(false);
        }
        saveData.EquippedItems.Remove(saveData.EquippedItems.Find(x => x.name == itemName));
    }
}
