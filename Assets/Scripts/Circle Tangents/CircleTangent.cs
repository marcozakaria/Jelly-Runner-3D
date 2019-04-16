using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTangent : MonoBehaviour
{
    protected Vector3 GetRotatedTangent(float degree,float scale)
    {
        double angle = degree * Mathf.Deg2Rad;
        float x = scale * (float)System.Math.Sin(angle); // system.math uses double not float , give us more persion
        float z = scale * (float)System.Math.Cos(angle);

        return new Vector3(x, 0, z);
    }
}
