using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyClickReciveir : MonoBehaviour
{
    [Range(0.1f,5)]
    public float waveTimeMultiplier = 1f;
    public float multiplier = 5;
    public bool moving = false;

    [Space(10)]
    public float timeToDeform = 1f;

    private Material modelMatrial;
    private float controlTime;
    private Camera mainCamera;

    private RaycastHit hit;
    private Ray clickRay;

    void Start ()
    {
        modelMatrial = GetComponent<Renderer>().material;
        mainCamera = Camera.main;

        StartCoroutine(DeformMesh());
	}
	
	void Update ()
    {
        controlTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (!moving)
            {
                clickRay = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(clickRay, out hit))
                {
                    controlTime = 0;
                    modelMatrial.SetVector("_ModelOrigin", transform.position);
                    modelMatrial.SetVector("_ImpactOrigin", hit.point);
                }
            }
            else
            {
                /*clickRay = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(clickRay, out hit))
                {
                    controlTime = 0;
                    modelMatrial.SetVector("_ModelOrigin", transform.position + new Vector3(0, 0.1f, 1)*multiplier);
                    modelMatrial.SetVector("_ImpactOrigin", transform.position + -Vector3.forward);
                }*/
                AddDeformation();
            }
        }

        //Mathf.Sin(Time.time) * multiplier)
        
        modelMatrial.SetFloat("_ControlTime", Mathf.PingPong(Time.time, multiplier));//(-1,1, Mathf.Sin(Time.time) * multiplier));
        //Debug.Log(Mathf.Sin(Time.time) * multiplier);
        //modelMatrial.SetFloat("_ControlTime", controlTime * waveTimeMultiplier);
    }

    IEnumerator DeformMesh()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            //controlTime = 1;
            modelMatrial.SetVector("_ModelOrigin", transform.position + new Vector3(0, 0.1f, 1) * multiplier);
            modelMatrial.SetVector("_ImpactOrigin", transform.position + Vector3.forward);
        }
    }

    public void AddDeformation()
    {
        controlTime = 0;
        //Vector3 positionMouse =  mainCamera.ScreenToWorldPoint(Input.mousePosition);
        modelMatrial.SetVector("_ModelOrigin", transform.position + new Vector3(0, 0.1f, 1) * multiplier);
        modelMatrial.SetVector("_ImpactOrigin", transform.position+Vector3.forward);
    }
}
