using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;
using Random = UnityEngine.Random;

public class BlockSpawnerScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("List of spawnable blocks")]
    private List<Transform> blocks;

    [Tooltip("What speed the block falls")]
    public static float fallSpeed = 2f;
    
    [Tooltip("Speed of movement to sides")]
    public static float sideSpeed = 3f;

    [SerializeField]
    private Transform camera;

    [SerializeField]
    private GameObject heightChecker;

    public static bool canSpawn = true;

    private bool canSpawnChecker = true;
    private float spawnXLimit = 5;

    private float cameraMin = 0;
    private float cameraOffset = 1f;
    private float minDifference = 1f;
    
    private float cameraDefaultY = 0;

    private Vector3 newCameraPosition;

    private void Start()
    {
        
        newCameraPosition = camera.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.gameOver)
        {
            newCameraPosition = new Vector3(camera.position.x, cameraDefaultY, camera.position.z);
            camera.position = Vector3.MoveTowards(camera.position, newCameraPosition, 1f * Time.deltaTime);
        }
        
        if (!GameState.running || GameState.gameOver) return;

        if (canSpawn)
        {
            this.spawnBlock();
        }

        if (canSpawnChecker)
        {
            spawnChecker();
        }
        
        float speed = 3f * Time.deltaTime;
        camera.position = Vector3.MoveTowards(camera.position, newCameraPosition, speed);
    }

    private void spawnBlock()
    {
        int index = Random.Range(0, this.blocks.Count);
        float xPosition = Random.Range(-2, 2);
        Instantiate(blocks[index], new Vector3(xPosition, camera.position.y + 6), Quaternion.identity);
        
        canSpawn = false;
    }

    private void spawnChecker()
    {
        GameObject heightCheckerObject = Instantiate(heightChecker, new Vector3(0, camera.position.y + 6), Quaternion.identity);
        heightCheckerObject.transform.parent = transform;
        canSpawnChecker = false;
    }

    public void moveCameraTo(float y)
    {
        if (y > cameraMin)
        {
            float actualY = camera.position.y;
            float newY = y + cameraOffset;

            if (Math.Abs(Math.Abs(actualY) - Math.Abs(newY)) <= minDifference)
            {
                canSpawnChecker = true;
                return;
            }
            
            newCameraPosition = new Vector3(camera.position.x, y + cameraOffset, camera.position.z);
            canSpawnChecker = true;
        }
    }
    

    public void setCanSpawnChecker()
    {
        canSpawnChecker = true;
    }

    public void reset()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            Destroy(block);
        }
        newCameraPosition = new Vector3(camera.position.x, cameraDefaultY, camera.position.z);
        this.camera.position = newCameraPosition;
        this.canSpawnChecker = true;
        canSpawn = true;
    }
}
