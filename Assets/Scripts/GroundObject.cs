using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObject : MonoBehaviour
{
    public float distanceToDiacive = 12f;
    public Transform playerTransform;

    private MeshRenderer meshRenderer;

    private void OnEnable()
    {
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        else
        {
            meshRenderer.material.color = Random.ColorHSV();
        }

        playerTransform = GameObject.FindGameObjectWithTag("PLayer").transform;
    }

    private void Update()
    {
        transform.position += new Vector3(0f, 0f, GameManager.instance.gameSpeed) * Time.deltaTime;

        if (Vector3.Distance(transform.position,playerTransform.position) < 10f)
        {
            Debug.Log(Vector3.Distance(transform.position, playerTransform.position));
            gameObject.SetActive(false);
        }
    }
}
