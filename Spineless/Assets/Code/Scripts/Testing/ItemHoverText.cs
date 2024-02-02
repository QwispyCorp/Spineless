using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHoverText : MonoBehaviour
{
    private MeshRenderer mesh;
    public Color hoverEmissionColor;
    public GameObject itemText;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    private void OnMouseEnter()
    {
        mesh.material.SetColor("_EmissionColor", hoverEmissionColor);
        itemText.SetActive(true);
    }
    private void OnMouseExit()
    {
        mesh.material.SetColor("_EmissionColor", Color.black);
        itemText.SetActive(false);
    }
    private void OnMouseDown()
    {
        itemText.SetActive(false);
    }
}
