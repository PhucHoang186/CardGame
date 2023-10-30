using System.Collections;
using System.Collections.Generic;
using MilkShake;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    [SerializeField] private Shaker cameraShaker;
    [SerializeField] private ShakePreset shakePreset;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void Shake()
    {
        cameraShaker.Shake(shakePreset);
    }


}
