using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBreath : MonoBehaviour
{
    private bool breathing = true;
    private PlayerParameters player;
    [SerializeField] AudioSource UnderwaterBreathSoundEffect;
    [SerializeField] AudioSource AboveWaterBreathSoundEffect;

    private float breathTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerParameters.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (breathing)
        {
            player.PlayerCurrentOxygen += Time.deltaTime * (PlayerParameters.instance.PlayerMaxOxygen / PlayerParameters.instance.OxygenSupplyTime);
            player.PlayerCurrentOxygen = Mathf.Min(player.PlayerMaxOxygen, player.PlayerCurrentOxygen);
        }
        else
        {
            player.PlayerCurrentOxygen -= Time.deltaTime * player.PlayerLoseOxygenRate;

            if (!AboveWaterBreathSoundEffect.isPlaying && !UnderwaterBreathSoundEffect.isPlaying && breathTimer <= 0.0f)
            {
                UnderwaterBreathSoundEffect.Play();
                breathTimer = Random.Range(3.0f, 6.0f);
            }
        }

        breathTimer -= Time.deltaTime;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Air" )
        {
            breathing = true;
            AboveWaterBreathSoundEffect.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Air")
        {
            breathing = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Air")
        {
            breathing = true;
        }
    }
}
