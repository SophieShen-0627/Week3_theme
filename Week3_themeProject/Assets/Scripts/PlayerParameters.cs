using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerParameters : MonoBehaviour
{
    public static PlayerParameters instance;

    public int PlayerMaxOxygen = 5;                  //player max oxygen  
    public float PlayerCurrentOxygen = 5;              //player current oxygen, decreasing if player is in water.
    public float PlayerLoseOxygenRate = 1;             //how long it will take for player oxygen rate from max to zero
    public float OxygenSupplyTime = 1;                // how long it will take for player oxygen rate can rise from 0 to max;
    public float PlayerDetectFishDistance = 2;
    public float PlayerLoseSpeedRate = 0.6f;
    public float PlayerOxygenCostIncreaseRate = 1.5f;
    public float PlayerDashingCostOxygen = 0.6f;
    public AudioClip DeathAudio;

    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (PlayerCurrentOxygen <= 0)
        {
            AudioManager.instance.MuteSFX();

            GetComponent<AudioSource>().PlayOneShot(DeathAudio);

        }
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
