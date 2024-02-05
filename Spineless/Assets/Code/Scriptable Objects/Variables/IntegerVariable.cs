using UnityEngine;

//A scriptable object to store/save shared integer values across objects

[CreateAssetMenu(fileName = "Integer", menuName = "Variables/Integer", order = 0)]
public class IntegerVariable : ScriptableObject {
    public int Value;
}
