using System.Collections.Generic;
using UnityEngine;

public class WingManager : MonoBehaviour
{
    public List<GameObject> wingPrefabs;

    public Transform position1;
    public Transform position2;
    public Transform position3;

    void Start()
    {
        //WING 1: find and instantiate a random wing for position 1
        int wing1PrefabIndex = Random.Range(0, wingPrefabs.Count);
        GameObject wing1Prefab = wingPrefabs[wing1PrefabIndex];
        GameObject _wing1 = Instantiate(wing1Prefab, position1.position, position1.rotation);
        _wing1.transform.parent = position1;

        //WING 2: find and instantiate a random wing not spawned in first position
        int wing2PrefabIndex;
        do
        {
            wing2PrefabIndex = Random.Range(0, wingPrefabs.Count);
        } while (wing2PrefabIndex == wing1PrefabIndex);

        GameObject wing2Prefab = wingPrefabs[wing2PrefabIndex];
        GameObject _wing2 = Instantiate(wing2Prefab, position2.position, position2.rotation);
        _wing2.transform.parent = position2;

        //WING 3: find and instantiate a random wing not spawned in first or second position
        int wing3PrefabIndex;
        do
        {
            wing3PrefabIndex = Random.Range(0, wingPrefabs.Count);
        } while (wing3PrefabIndex == wing1PrefabIndex || wing3PrefabIndex == wing2PrefabIndex);

        GameObject wing3Prefab = wingPrefabs[wing3PrefabIndex];
        GameObject _wing3 = Instantiate(wing3Prefab, position3.position, position3.rotation);
        _wing3.transform.parent = position3;
    }
}
