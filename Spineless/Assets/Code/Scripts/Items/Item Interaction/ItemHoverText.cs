using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHoverText : MonoBehaviour
{
    private MeshRenderer mesh;
    public Color hoverEmissionColor;
    public float hoverEmissionIntensity;
    public GameObject itemText;
    void Start()
    {
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
