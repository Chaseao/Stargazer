using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    [SerializeField] GlobalSetting setting;

    void Start()
    {
        foreach(var childSource in GetComponentsInChildren<AudioSource>())
        {
            childSource.volume = setting.Value / 100f;
        }
    }
}
