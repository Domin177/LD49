using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;
using Random = UnityEngine.Random;

public class EnviromentScript : MonoBehaviour
{
    private float maxSides = 28;
    private float minSpeed = 0.5f;
    private float maxSpeed = 1.2f;
    
    [SerializeField]
    private List<Transform> movingEnviroments;

    private ConcurrentDictionary<long, Enviroment> spawnedEnviroments = new ConcurrentDictionary<long, Enviroment>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(spawn));
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameState.running || GameState.gameOver) return;
        
        foreach (var entry in spawnedEnviroments)
        {
            Transform enviroment = entry.Value.transform;
            Vector3 direction = new Vector3(entry.Value.moveSpeed * Time.deltaTime, 0, 0);
            enviroment.Translate(direction);
            if (Math.Abs(enviroment.position.x) > maxSides)
            {
                Destroy(enviroment.gameObject);
                spawnedEnviroments.TryRemove(entry.Key, out Enviroment removedEnviroment);
            }
        }
    }

    private void spawnEnviroment()
    {
        int index = Random.Range(0, movingEnviroments.Count);
        float y = Random.Range(-2, 2);
        y = transform.position.y;

        float direction = Random.Range(0, 10);
        
        float x;
        float moveSpeed;
        if (direction < 5)
        {
            x = -(maxSides - 0.5f);
            moveSpeed = Random.Range(this.minSpeed, this.maxSpeed);
        }
        else
        {
            x = maxSides - 0.5f;
            moveSpeed = -Random.Range(this.minSpeed, this.maxSpeed);
        }
        
        Transform enviroment = Instantiate(movingEnviroments[index], new Vector3(x, y, 0), Quaternion.identity);
        
        this.spawnedEnviroments.TryAdd(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), new Enviroment(enviroment, moveSpeed));
    }

    IEnumerator spawn()
    {
        for (;;)
        {
            if (GameState.running && !GameState.gameOver)
            {
                spawnEnviroment();
            }

            yield return new WaitForSeconds(10);
        }
    }

    private class Enviroment
    {
        public Transform transform;
        public float moveSpeed;

        public Enviroment(Transform transform, float moveSpeed)
        {
            this.transform = transform;
            this.moveSpeed = moveSpeed;
        }
        
        
    }

    public void reset()
    {
        foreach (KeyValuePair<long,Enviroment> entry in this.spawnedEnviroments)
        {
            Destroy(entry.Value.transform.gameObject);
        }
        
        this.spawnedEnviroments.Clear();
    }
}
