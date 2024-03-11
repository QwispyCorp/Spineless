using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Subtitle
{
    [TextArea(1,20)]
    public string text;
    public float duration;

}
