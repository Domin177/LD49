using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtonScript : MonoBehaviour
{
    [SerializeField]
    private SoundScript _soundScript;

    [SerializeField]
    private Image soundOn;
    [SerializeField]
    private Image soundOff;

    private bool playing = true;

    private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        this._button = GetComponent<Button>();
        this._button.onClick.AddListener(btnClick);
        this.soundOff.enabled = false;
    }

    private void btnClick()
    {
        if (playing)
        {
            _soundScript.disableBackgroundMusic();
            this.soundOn.enabled = false;
            this.soundOff.enabled = true;
            this.playing = false;

        }
        else
        {
            _soundScript.enableBackgroundMusic();
            this.soundOn.enabled = true;
            this.soundOff.enabled = false;
            this.playing = true;
        }
    }
}
