using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class HeightChecker : MonoBehaviour
{
    private BlockSpawnerScript _blockSpawnerScript;
    private ScoreScript scoreScript;

    // Start is called before the first frame update
    void Start()
    {
        this._blockSpawnerScript = transform.parent.GetComponent<BlockSpawnerScript>();
        this.scoreScript = GameObject.Find("/Canvas/TopPanel/ActualScore").GetComponent<ScoreScript>();
    }

    private void Update()
    {
        if (!GameState.running || GameState.gameOver) return;
        
        Vector3 direction = new Vector3(0, (-BlockSpawnerScript.fallSpeed - 7f) * Time.deltaTime, 0);
        this.transform.Translate(direction);

        if (transform.position.y < -6.5f)
        {
            Destroy(gameObject);
            this._blockSpawnerScript.setCanSpawnChecker();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            BlockScript blockScript = other.GetComponent<BlockScript>();
            if (!blockScript.isBlockActive())
            {
                if (transform.position.y > 0)
                {
                    this._blockSpawnerScript.moveCameraTo(this.transform.position.y);
                }
                this.scoreScript.setScore(this.transform.position.y + 6.3f);
                this._blockSpawnerScript.setCanSpawnChecker();
                Destroy(gameObject);
            }
        } 
        else if (other.CompareTag("Platform"))
        {
            Destroy(gameObject);
            this._blockSpawnerScript.setCanSpawnChecker();
            this.scoreScript.setScore(0);
        }
    }

}