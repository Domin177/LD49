using System;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class GameState : MonoBehaviour
    {
        public static bool running = false;
        public static bool gameOver = false;

        [SerializeField] private MenuScript _menuScript;
        [SerializeField] private SoundScript soundScript;
        [SerializeField] private GameResetScript gameResetScript;

        private void Update()
        {
            if (gameOver) return;
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (running)
                {
                    this._menuScript.showPauseMenu();
                    if (soundScript.isBackgroundMusicEnabled())
                    {
                        this.soundScript.stopBackgroundMusic();
                    }
                    running = false;
                }
                else
                {
                    this._menuScript.hideAll();
                    if (soundScript.isBackgroundMusicEnabled())
                    {
                        this.soundScript.playBackgroundMusic();
                    }
                    running = true;
                }
            }
        }

        public void continueGame()
        {
            this._menuScript.hideAll();
            if (soundScript.isBackgroundMusicEnabled())
            {
                this.soundScript.playBackgroundMusic();
            }
            running = true;
        }
    
        public void playGame()
        {
            running = true;
            this._menuScript.hideAll();
            if (soundScript.isBackgroundMusicEnabled())
            {
                this.soundScript.playBackgroundMusic();
            }
        }

        public void newGame()
        {
            gameResetScript.reset();
            running = true;
            gameOver = false;
            this._menuScript.hideAll();
            if (soundScript.isBackgroundMusicEnabled())
            {
                this.soundScript.playBackgroundMusic();
            }
        }
    }
}