using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public TextAsset json;
    public WavesInfo wInfo;
    void Start()
    {
        LoadLevel(json.text);
    }

    public void LoadLevel(string json)
    {
        wInfo = JsonUtility.FromJson<WavesInfo>(json);
        Debug.Log($"mana {wInfo.mana}, hearts {wInfo.hearts}");
    } 
    
    
}
