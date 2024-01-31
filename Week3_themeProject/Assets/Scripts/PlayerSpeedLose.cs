using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedLose : MonoBehaviour
{
    PlayerMovement player;
    [SerializeField] bool hasFish = false;
    [SerializeField] ParticleSystem FishParticle;

    float originMoveSpeed;
    float originOxygenSpeed;

    void Start()
    {
        player = GetComponent<PlayerMovement>();

        originMoveSpeed = player.moveSpeed;
        originOxygenSpeed = PlayerParameters.instance.PlayerLoseOxygenRate;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, PlayerParameters.instance.PlayerDetectFishDistance);

        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<FishFlock>())
            {
                hasFish = true;
                break;
            }
            else hasFish = false;
        }

        if (hasFish)
        {
            if (!FishParticle.isPlaying)
                FishParticle.Play();
            player.moveSpeed = PlayerParameters.instance.PlayerLoseSpeedRate * originMoveSpeed;
            PlayerParameters.instance.PlayerLoseOxygenRate = PlayerParameters.instance.PlayerOxygenCostIncreaseRate * originOxygenSpeed;
        }
        else
        {
            FishParticle.Stop();
            player.moveSpeed = originMoveSpeed;
            PlayerParameters.instance.PlayerLoseOxygenRate = originOxygenSpeed;
        }
    }
}
