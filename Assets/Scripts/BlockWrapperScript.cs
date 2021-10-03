using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class BlockWrapperScript : MonoBehaviour
{
    public bool collided;
    public bool fixedMovement;

    // Update is called once per frame
    void Update()
    {
        updatePosition();
    }
    
    private void updatePosition()
    {
        if (!GameState.running || GameState.gameOver) return;
        
        if (collided || fixedMovement) return;
        
        float move = 0;
        if (Input.GetKey("left"))
        {
            move = -BlockSpawnerScript.sideSpeed;
        } else if (Input.GetKey("right"))
        {
            move = BlockSpawnerScript.sideSpeed;
        }

        Vector3 direction = new Vector3(move * Time.deltaTime, -BlockSpawnerScript.fallSpeed * Time.deltaTime, 0);
        this.transform.Translate(direction);
    }
}
