using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource SFX_BreathingUnderwater;
    [SerializeField] AudioSource SFX_BreathingAboveWater;
    [SerializeField] AudioSource SFX_Swimming;
    [SerializeField] AudioSource SFX_Dash;
    [SerializeField] AudioSource SFX_HeartBeat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        instance = this;
    }

    public void MuteSFX()
    {
        SFX_BreathingUnderwater.volume = 0;
        SFX_BreathingAboveWater.volume = 0;
        SFX_Swimming.volume = 0;
        SFX_Dash.volume = 0;
        SFX_HeartBeat.volume = 0;
    }
}
