using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameters : MonoBehaviour
{
    public static PlayerParameters instance;

    public float PlayerMaxOxygen = 5;                  //player max oxygen  
    public float PlayerCurrentOxygen = 5;              //player current oxygen, decreasing if player is in water.
    public float PlayerLoseOxygenRate = 1;             //how long it will take for player oxygen rate from max to zero
    public float OxygenSupplyTime = 1;                // how long it will take for player oxygen rate can rise from 0 to max;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
}
