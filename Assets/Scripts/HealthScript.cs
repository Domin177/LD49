using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    private SoundScript _soundScript;

    [SerializeField] private MenuScript _menuScript;
    
    private Image[] healths;

    private int healthAmount = 16;
    private int maxHealth = 16;

    private Object _lock = new Object();
    // Start is called before the first frame update
    void Start()
    {
        this.healths = this.GetComponentsInChildren<Image>();
    }

    public void minusHealth()
    {
        lock (_lock)
        {
            if (this.healthAmount == 1)
            {
                GameState.gameOver = true;
                GameState.running = false;
                this._soundScript.stopBackgroundMusic();
                this._soundScript.playGameOverMusic();
                this.healths[healthAmount].enabled = false;
                healthAmount--;
                this._menuScript.showGameOverMenu();
                
                return;
            }
            
            
            if (this.healthAmount >= 1){
                this.healths[healthAmount].enabled = false;
                healthAmount--;
            }
        }
    }

    public void reset()
    {
        this.healthAmount = maxHealth;
        foreach (Image health in this.healths)
        {
            health.enabled = true;
        }
    }
}
