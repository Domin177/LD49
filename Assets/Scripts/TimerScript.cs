using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class TimerScript : MonoBehaviour
{
    private Text timerText;
    
    
    private float timeElapsed;
    private DateTime dateTime = new DateTime();
    // Start is called before the first frame update
    void Start()
    {
        this.timerText = GetComponent<Text>();

        this.timerText.text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameState.running || GameState.gameOver) return;
        
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 1f) {
            timeElapsed %= 1f;
            dateTime = dateTime.AddSeconds(1);
            timerText.text = (dateTime.Minute < 10 ? "0" + dateTime.Minute : dateTime.Minute.ToString()) + ":" + (dateTime.Second < 10 ? "0" + dateTime.Second : dateTime.Second.ToString());
        }
    }

    public void reset()
    {
        this.dateTime = new DateTime(2021, 10 ,1, 0, 0, 0);
        this.timeElapsed = 0f;
        this.timerText.text = "00:00";
    }
    
    public int getTimeInSeconds()
    {
        return (this.dateTime.Minute * 60) + this.dateTime.Second;
    }
}
