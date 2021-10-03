using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class BlockScript : MonoBehaviour
{
    [SerializeField]
    private BlockWrapperScript blockWrapper;

    private HealthScript _healthScript;
    private SoundScript soundScript;
    
    private Rigidbody2D rigidbody;
    private float defaultGravity;
    private float heightToDestroy = -10;
   
    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody = this.GetComponent<Rigidbody2D>();
        this._healthScript = GameObject.Find("/Canvas/TopPanel/HealthPanel").GetComponent<HealthScript>();
        this.soundScript = GameObject.Find("/SoundObject").GetComponent<SoundScript>();
        this.defaultGravity = this.rigidbody.gravityScale;
        this.rigidbody.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= heightToDestroy)
        {
            if (!this.blockWrapper.collided)
            {
                BlockSpawnerScript.canSpawn = true;
            }
            //TODO fail
            this.soundScript.playBlockFallMusic();
            this._healthScript.minusHealth();
            Destroy(this.blockWrapper.gameObject);
        }
        
        if (!GameState.running || GameState.gameOver) return;

        if (this.blockWrapper.collided) return;
        
        if (Input.GetKey("space"))
        {
            unleashObject();
        }  
        else if (Input.GetKeyDown("up"))
        {    
            transform.Rotate(0, 0, 90);
            this.soundScript.playRotateMusic();
        } 
        else if (Input.GetKeyDown("down"))
        {
            transform.Rotate(0, 0, -90);
            this.soundScript.playRotateMusic();
        }
    }

    private void unleashObject()
    {
        this.blockWrapper.fixedMovement = true;
        this.rigidbody.gravityScale = defaultGravity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (this.blockWrapper.collided) return;
        
        this.soundScript.playBlockDropMusic();
        unleashObject();
        this.blockWrapper.collided = true;
        BlockSpawnerScript.canSpawn = true;
    }

    public bool isBlockActive()
    {
        return !this.blockWrapper.collided;
    }

    
}
