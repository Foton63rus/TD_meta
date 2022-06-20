using System;
using System.Collections.Generic;
using UnityEngine;

public class ManagerWave : MonoBehaviour
{
    public TextAsset _jsonLevelInfo;
    public WavesInfo _wInfo;

    private bool _isWaiting4Running = false;
    private bool _isSpawning = false;
    private int _waveNumber;
    private float _wavesStartTime;
    private float _timeOfUnitSpawn;


    public void Init(string jsonLevelInfo)
    {
        _isWaiting4Running = false;

        _wInfo = JsonUtility.FromJson<WavesInfo>(jsonLevelInfo);
        if (_wInfo is null)
        {
            return;
        }
        else
        {
            SetDataFromJSON(); //todo добавить ману, хп, спавны и пр. либо отправить дальше
            nextWave(0);
        }

    }

    private void nextWave(int waveNumber)
    {
        _waveNumber = waveNumber;
        _isWaiting4Running = true;
        _isSpawning = false;
        _wavesStartTime = Time.realtimeSinceStartup;
    }

    private void SetDataFromJSON()
    {
        
    }

    private void EnemySpawn()
    {
        
    }

    public void Start()
    {
        Init(_jsonLevelInfo.text);
    }

    public void Update()
    {
        if (_isWaiting4Running)
        {
            if (Time.realtimeSinceStartup - _wavesStartTime >= 
                _wInfo.waves[_waveNumber].sec_to_start)
            {
                _isSpawning = true;
                _isWaiting4Running = false;
            }
        }
    }
}

[Serializable]
public class WavesInfo
{
    public int hearts;
    public int mana;
    public mapTheme map_theme;
    public Vector3[] spawn;
    public Vector3[] castle;
    
    public UnitTrap[] Roads;

    public wave[] waves;
}

[Serializable]
public enum mapTheme
{
    winter = 0,
    summer = 1
}
[Serializable]
public class wave
{
    public float sec_to_start;
    public waveUnitInfo[] units;
}
[Serializable]
public class waveUnitInfo
{
    public float sec_to_go;
    public string unit_type_id;
    public int spawn_id;
}

[Serializable]
public class UnitTrap
{
    public List<Vector3> Knots;
}