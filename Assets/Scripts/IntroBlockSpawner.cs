using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBlockSpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("List of spawnable blocks")]
    private List<Transform> blocks;

    private List<Transform> spawnedBlocks = new List<Transform>();
    private float timeElapsed;
    
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 2f) {
            timeElapsed %= 2f;
            spawnBlock();
        }

        List<Transform> toRemove = new List<Transform>();
        
        foreach (Transform spawnedBlock in spawnedBlocks)
        {
            Vector3 direction = new Vector3(0, -BlockSpawnerScript.fallSpeed * Time.deltaTime, 0);
            spawnedBlock.Translate(direction);

            if (spawnedBlock.transform.position.y < -7.5f)
            {
                toRemove.Add(spawnedBlock);
            }
        }
        
        foreach (var transform1 in toRemove)
        {
            spawnedBlocks.Remove(transform1);
            Destroy(transform1.gameObject);
        }
        
        toRemove.Clear();
    }
    
    private void spawnBlock()
    {
        int index = Random.Range(0, this.blocks.Count);
        float xPosition = Random.Range(-9, 10);
        Transform transform = Instantiate(blocks[index], new Vector3(xPosition, 7.5f), Quaternion.identity);
        transform.gameObject.SetActive(true);
        spawnedBlocks.Add(transform);
        
    }
}
