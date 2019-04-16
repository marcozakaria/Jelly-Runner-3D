using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public float gameSpeed = 0.05f;
    public float speedIncreaseRate = 0.001f;

    public Transform playerPivotStartPosition;
    public Transform playerTransform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        gameSpeed += speedIncreaseRate * Time.deltaTime;
    }

    public void PlayerDie()
    {
        playerTransform.position = playerPivotStartPosition.position;
       // playerTransform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
