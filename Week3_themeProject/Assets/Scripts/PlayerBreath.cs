using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBreath : MonoBehaviour
{
    private bool breathing = true;
    private PlayerParameters player;
    [SerializeField] AudioSource BGMPlayer;


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
        }

        if (player.PlayerCurrentOxygen < 2)
        {
            BGMPlayer.pitch = player.PlayerCurrentOxygen / player.PlayerMaxOxygen;

        }
        else
        {
            BGMPlayer.pitch = 1.0f;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Air" )
        {
            breathing = true;
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
