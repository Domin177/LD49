using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResetScript : MonoBehaviour
{
    [SerializeField]
    private TimerScript _timerScript;

    [SerializeField]
    private BlockSpawnerScript _blockSpawnerScript;

    [SerializeField]
    private EnviromentScript _enviromentScript;
    
    [SerializeField]
    private ScoreScript _scoreScript;

    [SerializeField] private HealthScript _healthScript;

    public void reset()
    {
        _timerScript.reset();
        _blockSpawnerScript.reset();
        _enviromentScript.reset();
        _scoreScript.reset();
        _healthScript.reset();
    }
}
