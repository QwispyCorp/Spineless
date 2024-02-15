using System.Collections.Generic;
using UnityEngine;

public class WingManager : MonoBehaviour
{
    public List<GameObject> wing1Prefabs;
    public List<GameObject> wing2Prefabs;
    public List<GameObject> wing3Prefabs;

    public Transform position1;
    public Transform position2;
    public Transform position3;

    void Start()
    {
        GameObject wing1Prefab = wing1Prefabs[Random.Range(0, wing1Prefabs.Count)];
        GameObject wing2Prefab = wing2Prefabs[Random.Range(0, wing2Prefabs.Count)];
        GameObject wing3Prefab = wing3Prefabs[Random.Range(0, wing3Prefabs.Count)];
        Instantiate(wing1Prefab, position1.position, position1.rotation);
        Instantiate(wing2Prefab, position2.position, position2.rotation);
        Instantiate(wing3Prefab, position3.position, position3.rotation);
    }
}
