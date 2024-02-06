using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHoverText : MonoBehaviour
{
    [SerializeField] private string itemName;
    private MeshRenderer mesh;
    public Color hoverEmissionColor;
    public float hoverEmissionIntensity;
    private GameObject itemText;

    void Start()
    {
        if (GameObject.Find(itemName + " Text") != null)
        {
            itemText = GameObject.Find(itemName + " Text");
        }
        else
        {
            itemText = null;
            Debug.Log("Could not find " + itemName + " Text");
        }
        itemText.SetActive(false);
        mesh = GetComponent<MeshRenderer>();
    }
    private void OnMouseEnter()
    {
        mesh.material.SetColor("_Emissive", hoverEmissionColor * hoverEmissionIntensity);
        itemText.SetActive(true);
    }
    private void OnMouseExit()
    {
        mesh.material.SetColor("_Emissive", Color.black);
        itemText.SetActive(false);
    }
    private void OnMouseDown()
    {
        itemText.SetActive(false);
    }
}
