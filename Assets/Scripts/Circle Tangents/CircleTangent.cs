using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTangent : MonoBehaviour
{
    protected Vector3 GetRotatedTangent(float degree,float radius)
    {
        double angle = degree * Mathf.Deg2Rad;
        float x = radius * (float)System.Math.Sin(angle); // system.math uses double not float ,double give us more persion
        float z = radius * (float)System.Math.Cos(angle);

        return new Vector3(x, 0, z);
    }

    /// <summary>
    /// FindTangentCircle to two circles,vector4 for position and radius
    /// </summary>
    /// <param name="A"> for first circle</param>
    /// <param name="B">for second circle</param>
    /// <param name="degree">to talk to get rotated tangent funcion</param>
    /// <returns>Tanent circle position and radius</returns>
    protected Vector4 FindTangentCircle(Vector4 A,Vector4 B,float degree)
    {
        // tangent point on outer circle tangentPoint C
        Vector3 C = GetRotatedTangent(degree, A.w);

        float AB = Vector3.Distance(new Vector3(A.x, A.y, A.z), new Vector3(B.x, B.y, B.z));
        AB = Mathf.Max(AB, 0.1f); // to make sure its greater than zero 
        float AC = Vector3.Distance(new Vector3(A.x, A.y, A.z), C);
        float BC = Vector3.Distance(new Vector3(B.x, B.y, B.z), C);

        float angleCAB = ((AC * AC) + (AB * AB) - (BC * BC)) / (2 * AC * AB);
        // radius of tangent circle
        float r = (((A.w * A.w) - (B.w * B.w) + (AB * AB)) - (2 * A.w * AB * angleCAB)) / (2 * (A.w + B.w - AB * angleCAB));
        C = GetRotatedTangent(degree, A.w - r);

        return new Vector4(C.x, C.y, C.z, r);
    }
}
