using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource backgroundMusic;
    
    [SerializeField]
    private AudioSource gameOverMusic;
    
    [SerializeField]
    private AudioSource blockDropMusic;
    
    [SerializeField]
    private AudioSource rotateMusic;
    
    [SerializeField]
    private AudioSource blockFall;

    private bool backgroundMusicEnabled = true;

    public void playBackgroundMusic()
    {
        backgroundMusic.Play();
    }
    public void stopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }
    
    public void disableBackgroundMusic()
    {
        backgroundMusic.Stop();
        backgroundMusicEnabled = false;
    }
    public void enableBackgroundMusic()
    {
        backgroundMusic.Play();
        backgroundMusicEnabled = false;
    }
    
    public void playGameOverMusic()
    {
        gameOverMusic.Play();
    }
    
    public void stopGameOverMusic()
    {
        gameOverMusic.Stop();
    }
    
    public void playBlockDropMusic()
    {
        blockDropMusic.Play();
    }
    
    public void stopBlockDropMusic()
    {
        blockDropMusic.Stop();
    }
    
    public void playRotateMusic()
    {
        rotateMusic.Play();
    }
    
    public void stopRotateMusic()
    {
        rotateMusic.Stop();
    }
    
    public void playBlockFallMusic()
    {
        blockFall.Play();
    }
    
    public void stopBlockFallMusic()
    {
        blockFall.Stop();
    }

    public bool isBackgroundMusicEnabled()
    {
        return backgroundMusicEnabled;
    }
}
