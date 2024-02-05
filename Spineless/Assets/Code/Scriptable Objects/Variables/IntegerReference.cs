using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//A class that allows a reference to a float variable scriptable object for inspector changes and manual input

[Serializable]
public class IntegerReference
{
    public bool UseConstant = true;
    public int ConstantValue;
    public IntegerVariable Variable;

    public int Value
    {
        get
        {
            return UseConstant ? ConstantValue : Variable.Value;
        }
        set
        {
            Variable.Value = value;
        }
    }
}
