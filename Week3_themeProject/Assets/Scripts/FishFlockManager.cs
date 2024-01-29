using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFlockManager : MonoBehaviour
{
    public static FishFlockManager instance;

    public float speed = 2.0f;
    public float SeperationRate = 0.5f;

    public float rotationSpeed = 2.0f;
    public float neighborRadius = 3.0f;
    public float separationRadius = 1.0f;

    public float ChasingPlayerSpeed = 3.0f;
    public float ChasingPlayerDistance = 5.0f;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }


}
