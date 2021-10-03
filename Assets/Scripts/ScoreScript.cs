using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    [SerializeField]
    private Text maxScore;

    private int maxScoreValue;
    private int actualScoreValue;

    private Text actualScore;

    private int previousScore;

    private void Start()
    {
        this.actualScore = this.GetComponent<Text>();
        
        maxScore.text =  "Max height: 0";
        actualScore.text =  "Actual height: 0";
    }
    // Start is called before the first frame update

    public void setScore(float score)
    {
        int calculatedScore = normalize((int) (score * 10));

        if (calculatedScore > maxScoreValue)
        {
            this.maxScore.text = "Max height: " +  calculatedScore;
            this.maxScoreValue = calculatedScore;
        }

        actualScore.text = "Actual height: " + calculatedScore;
        actualScoreValue = calculatedScore;

        this.previousScore = calculatedScore;
    }

    private int normalize(int calculatedScore)
    {
        return Math.Abs(calculatedScore - previousScore) > 3 ? calculatedScore : previousScore;
    }

    public void reset()
    {
        maxScoreValue = 0;
        actualScoreValue = 0;
        maxScore.text =  "Max height: 0";
        actualScore.text =  "Actual height: 0";
    }

    public int getActualScore()
    {
        return actualScoreValue;
    }
    
    public int getMaximumScore()
    {
        return maxScoreValue;
    }
}
