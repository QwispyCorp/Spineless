using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightScript : MonoBehaviour
{
    public Color highlightColor = new Color(1f, 1f, 1f, 0.5f);
    public Color unHighlightColor = Color.black;
    public MeshRenderer CardMesh;

    public void OnMouseEnter()
    {
        CardMesh.material.SetColor("_EmissiveColor", highlightColor); //highlight card
    }
    public void OnMouseExit()
    {
        CardMesh.material.SetColor("_EmissiveColor", unHighlightColor); //unhighlight card 
    }
}
