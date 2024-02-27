using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordSpin : MonoBehaviour
{
    [SerializeField] private float spinSpeed;
    void Update()
    {
        transform.Rotate(0, 0, spinSpeed, Space.Self);
    }
}
