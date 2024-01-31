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
    [SerializeField] AudioSource SwimSoundEffect;

    [SerializeField] bool PlayerIsAlive = true;
    [SerializeField] GameObject torch;
    [SerializeField] ParticleSystem Death;

    private float currentSpeed;
    private float dashTimer;
    private bool dashing = false;
    private float DeathTimer = 0;

    private float swimSoundTimer;
    private bool hasplaydeathparticle = false;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = moveSpeed;
        DeathTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerAlive();


        if (PlayerIsAlive) playerMovements();
        else DoDeath();
    }

    void playerMovements()
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

        if (dashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                dashing = false;
                currentSpeed = moveSpeed;
            }
        }

        // move player character

        Vector2 moveDirection = new Vector2(horizontal, vertical);
        float inputMagnitude = Mathf.Clamp01(moveDirection.magnitude);
        moveDirection.Normalize();

        transform.Translate(moveDirection * currentSpeed * inputMagnitude * Time.deltaTime, Space.World);

        // rotate player character

        if (moveDirection != Vector2.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);

            if (!SwimSoundEffect.isPlaying && swimSoundTimer <= 0.0f)
            {
                SwimSoundEffect.Play();
                swimSoundTimer = Random.Range(0.3f, 3.0f);
            }
        }
        else
        {
            SwimSoundEffect.Pause();
        }
        swimSoundTimer -= Time.deltaTime;
    }

    void checkPlayerAlive()
    {
        if (PlayerParameters.instance.PlayerCurrentOxygen > 0) PlayerIsAlive = true;
        else PlayerIsAlive = false;
    }

    void DoDeath()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        DeathTimer += Time.deltaTime;

        if (DeathTimer <= 1)
         sprite.color = new Color(1, 1, 1, 1 - DeathTimer);

        torch.SetActive(false);

        if (!!hasplaydeathparticle)
        {
            Death.Play();
            hasplaydeathparticle = true;
        }
    }
}
