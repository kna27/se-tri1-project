using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WantednessCalculations
{
    public static float AddWantedness(float current, float amount)
    {
        return Mathf.Clamp(current + amount, 0, 100f);
    }
}
