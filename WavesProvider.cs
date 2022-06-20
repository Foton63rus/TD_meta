using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesProvider : MonoBehaviour
{
    public WavesInfo wInfo;
    void Start()
    {
        Debug.Log(JsonUtility.ToJson(wInfo));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
