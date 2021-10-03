using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScorePublishScript : MonoBehaviour
{
    [SerializeField] private MenuScript _menuScript;

    [SerializeField] private InputField nameField;

    [SerializeField] private Text publishScoreText;

    [SerializeField] private ScoreScript scoreScript;

    [SerializeField] private Button btnPublishScore;
    [SerializeField] private Button closeButton;

    [SerializeField] private Text achievedPlaceValue;
    [SerializeField] private Text achievedPlaceText;

    [SerializeField] private TimerScript _timerScript;
    // Start is called before the first frame update
    void Start()
    {
        btnPublishScore.onClick.AddListener(publishScore);
        closeButton.onClick.AddListener(hideScorePublishPanel);
    }

    public void publishScore()
    {
        Debug.Log("clickol som na tuto kekecinku");
        Debug.Log(nameField.text);
        string name = nameField.text;
        if (!isNameValid(name)) return;

        this.btnPublishScore.gameObject.SetActive(false);
        this.closeButton.gameObject.SetActive(true);
        
        sendGameResult(scoreScript.getMaximumScore(), _timerScript.getTimeInSeconds(), name);
    }

    private bool isNameValid(string name)
    {
        return !string.IsNullOrEmpty(name);
    }

    public void showScorePublishPanel()
    {
        this.gameObject.SetActive(true);
        
        this.achievedPlaceText.gameObject.SetActive(false);
        this.achievedPlaceValue.gameObject.SetActive(false);
        
        this.btnPublishScore.gameObject.SetActive(true);
        this.closeButton.gameObject.SetActive(false);
        
        _menuScript.hideAll();
        publishScoreText.text = scoreScript.getMaximumScore().ToString();
    }

    public void hideScorePublishPanel()
    {
        this.gameObject.SetActive(false);
        _menuScript.showGameOverMenu(true);
    }
    
    public void sendGameResult(int stars, int time, string name)
    {
        StartCoroutine(sendPostRequest(stars, time, name));
    }
    
    IEnumerator sendPostRequest(int height, int time, string name)
    {
        LeaderBoardBody body = new LeaderBoardBody {name = name, score = height, time = time};
        //string bodyJsonString = "{\"name\" : \"" + name + "\",\"score\": " + stars + ",\"time\" : " + time + " }";
        string bodyJsonString = JsonUtility.ToJson(body);

        using (UnityWebRequest www = UnityWebRequest.Post("https://leaderboard.nisanick.com/ld49", ""))
        {
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(bodyJsonString));
            www.SetRequestHeader("Content-Type", "application/json");
            
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                LeaderBoardResponse responses =
                    JsonUtility.FromJson<LeaderBoardResponse>("{\"positions\":" + www.downloadHandler.text + "}");

                if (responses.positions.Count > 0)
                {
                    this.achievedPlaceText.gameObject.SetActive(true);
                    this.achievedPlaceValue.gameObject.SetActive(true);
                    achievedPlaceValue.text = responses.positions[0].position + " .place";
                }
            }
        }
    }

    [Serializable]
    private class LeaderBoardBody
    {
        public string name;
        public int score;
        public int time;
    }

    [Serializable]
    private class LeaderBoardResponse
    {
        public List<LeaderBoardPosition> positions;
    }

    [Serializable]
    private class LeaderBoardPosition
    {
        public int position;
    }
}
