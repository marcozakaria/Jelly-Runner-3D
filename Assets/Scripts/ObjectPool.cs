using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefap;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public static ObjectPool instance = null;

    public bool creatingObjectsDone = false;

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
        // DontDestroyOnLoad(gameObject);

        //MakePools();
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefap);
                obj.SetActive(false);
                obj.transform.parent = this.transform;
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

        creatingObjectsDone = true;
    }

    public void SpawnFromPool(string tag,Vector3 position)
    {
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;

        poolDictionary[tag].Enqueue(objectToSpawn);
    }

    public void SpawnParticleWithColor(string tag, Vector3 position,Color color)
    {
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        /*ParticleSystemRenderer particleSystemRenderer = objectToSpawn.GetComponent<ParticleSystemRenderer>();
        particleSystemRenderer.trailMaterial.color = color;
        particleSystemRenderer.material.color = color;*/
        //objectToSpawn.GetComponent<ParticleSystem>().startColor = color;       

        poolDictionary[tag].Enqueue(objectToSpawn);
    }
}
