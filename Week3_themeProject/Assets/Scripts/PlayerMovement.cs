using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    [SerializeField] float rotationSpeed = 160.0f;
    [SerializeField] float dashSpeed = 10.0f;
    [SerializeField] float dashDuration = 2.0f;
    [SerializeField] GameObject DashParticlePrefab;
    [SerializeField] AudioSource DashSoundEffect;

    private float currentSpeed;
    private float dashTimer;
    private bool dashing = false;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // capture input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && !dashing)
        {
            dashing = true;
            dashTimer = dashDuration;
            currentSpeed = dashSpeed;
            DashSoundEffect.Play();
            GameObject go = Instantiate(DashParticlePrefab, transform.position, Quaternion.identity, transform);
            go.transform.localRotation = Quaternion.Euler(0, 0, -60);
            Destroy(go, 1f);

            PlayerParameters.instance.PlayerCurrentOxygen -= PlayerParameters.instance.PlayerDashingCostOxygen;
        }

        // dash
        if(dashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0) 
            {
                dashing = false;
                currentSpeed = moveSpeed;
            }
        }

        Vector2 moveDirection = new Vector2(horizontal, vertical);
        float inputMagnitude = Mathf.Clamp01(moveDirection.magnitude);
        moveDirection.Normalize();

        transform.Translate(moveDirection * currentSpeed * inputMagnitude * Time.deltaTime, Space.World);

        // rotate player character
        // TODO: rotate fast when the move direction is opposite of current facing direction???
        if (moveDirection != Vector2.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
    }
}
