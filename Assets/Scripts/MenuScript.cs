using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;

    [SerializeField] private Button publishButton;
    
    // Start is called before the first frame update

    public void showMainMenu()
    {
        hideAll();
        gameObject.SetActive(true);
        mainMenu.SetActive(true);
    }

    public void showGameOverMenu(bool hidePublishButton = false)
    {
        hideAll();
        gameObject.SetActive(true);
        gameOverMenu.SetActive(true);
        if (hidePublishButton)
        {
            this.publishButton.gameObject.SetActive(false);
        }
        else
        {
            this.publishButton.gameObject.SetActive(true);
        }
    }
    
    public void showPauseMenu()
    {
        hideAll();
        gameObject.SetActive(true);
        pauseMenu.SetActive(true);
    }

    public void hideAll()
    {
        gameObject.SetActive(false);
        this.pauseMenu.SetActive(false);
        this.mainMenu.SetActive(false);
        this.gameOverMenu.SetActive(false);
    }
}
