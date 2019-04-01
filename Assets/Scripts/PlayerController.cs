using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 50;

    private Rigidbody myrigidbody;
    private MeshDeformer meshDeformer;
    private MeshFilter meshFilter;
    private Vector3[] meshVertices;

    private void Start()
    {
        myrigidbody = GetComponent<Rigidbody>();
        meshDeformer = GetComponent<MeshDeformer>();
        meshFilter = GetComponent<MeshFilter>();

        meshVertices = meshFilter.mesh.vertices;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        myrigidbody.AddForce(new Vector3(0f, jumpForce, 0f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        int i = 0;
        MeshDeformerInput.pressed = true;
        foreach (ContactPoint contact in collision.contacts)
        {
            i++;
            Debug.DrawRay(contact.point, contact.normal, Color.red);
            //meshDeformer.AddDeformingForce(contact.point, 10f);
            meshDeformer.AddDeformingForce(-contact.point, 50f);
        }
        Debug.Log(i);

       // DeformVertices();
    }

    private void DeformVertices()
    {
        for (int i = 0; i < meshVertices.Length; i++)
        {
            meshDeformer.AddDeformingForce(meshVertices[i], -50f);
        }
    }
}
