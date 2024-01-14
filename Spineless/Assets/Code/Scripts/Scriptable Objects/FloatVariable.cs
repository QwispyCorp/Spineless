using UnityEngine;

//A scriptable object to store/save shared float values across objects

[CreateAssetMenu(fileName = "Float", menuName = "Variables/Float", order = 0)]
public class FloatVariable : ScriptableObject {
    public float Value;
}
