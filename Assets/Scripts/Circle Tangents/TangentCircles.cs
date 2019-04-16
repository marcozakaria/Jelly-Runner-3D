using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangentCircles : CircleTangent
{
    public GameObject circlePrefap;

    private GameObject innerCircleGO, outerCircleGO, tangentCircleGO;

    public Vector4 innerCircle, outerCircle; // vector3 for position + loat for the radius of circle
    public float tangentCircleRadius;
    public float degree;

    private void Start()
    {
        innerCircleGO = (GameObject)Instantiate(circlePrefap);
        outerCircleGO = (GameObject)Instantiate(circlePrefap);
        tangentCircleGO = (GameObject)Instantiate(circlePrefap);
    }

    private void Update()
    {
        innerCircleGO.transform.position = new Vector3(innerCircle.x, innerCircle.y, innerCircle.z);
        innerCircleGO.transform.localScale = new Vector3(innerCircle.w, innerCircle.w, innerCircle.w) * 2;// multiply by 2 to get diameter
        outerCircleGO.transform.position = new Vector3(outerCircle.x, outerCircle.y, outerCircle.z);
        outerCircleGO.transform.localScale = new Vector3(outerCircle.w, outerCircle.w, outerCircle.w) * 2;// multiply by 2 to get diameter

        tangentCircleGO.transform.position = GetRotatedTangent(degree, outerCircle.w) + outerCircleGO.transform.position;
        tangentCircleGO.transform.localScale = new Vector3(tangentCircleRadius, tangentCircleRadius, tangentCircleRadius)*2;
    }
}
