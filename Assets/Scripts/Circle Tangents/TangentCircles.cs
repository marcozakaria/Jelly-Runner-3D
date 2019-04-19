using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangentCircles : CircleTangent
{
    public GameObject circlePrefap;

    private GameObject innerCircleGO, outerCircleGO;

    public Vector4 innerCircle, outerCircle; // vector3 for position + loat for the radius of circle

    [Range(1,64)]
    public int circleCount;
    private Vector4[] tangentCircles;
    private GameObject[] tangentObjects; 
     
    private void Start()
    {
        innerCircleGO = (GameObject)Instantiate(circlePrefap);
        outerCircleGO = (GameObject)Instantiate(circlePrefap);

        tangentCircles = new Vector4[circleCount];
        tangentObjects = new GameObject[circleCount];

        for (int i = 0; i < circleCount; i++)
        {
            GameObject tangentInstance = (GameObject)Instantiate(circlePrefap);
            tangentObjects[i] = tangentInstance;
            tangentObjects[i].transform.parent = transform;
        }
    }

    private void Update()
    {
        innerCircleGO.transform.position = new Vector3(innerCircle.x, innerCircle.y, innerCircle.z);
        innerCircleGO.transform.localScale = new Vector3(innerCircle.w, innerCircle.w, innerCircle.w) * 2;// multiply by 2 to get diameter
        outerCircleGO.transform.position = new Vector3(outerCircle.x, outerCircle.y, outerCircle.z);
        outerCircleGO.transform.localScale = new Vector3(outerCircle.w, outerCircle.w, outerCircle.w) * 2;// multiply by 2 to get diameter

        for (int i = 0; i < circleCount; i++)
        {
            tangentCircles[i] = FindTangentCircle(outerCircle, innerCircle, (360f / circleCount) * i);
            tangentObjects[i].transform.position = new Vector3(tangentCircles[i].x, tangentCircles[i].y, tangentCircles[i].z);
            tangentObjects[i].transform.localScale = new Vector3(tangentCircles[i].w, tangentCircles[i].w, tangentCircles[i].w) * 2;

        }
    }
}
