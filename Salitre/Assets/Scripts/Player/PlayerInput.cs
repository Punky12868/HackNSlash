using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerInput : MonoBehaviour
{
    public Player player;

    GameObject playerRenderer;
    Animator playerAnim;
    [SerializeField] float heightOffset;

    [SerializeField] Transform orientation;

    [HideInInspector] public Vector3 moveDir;
    Rigidbody rb;
    float h, v;

    [HideInInspector] public float speed;

    public float dashSpeed;
    public float dashCooldown;
    float dashCooldownStored;

    bool canDash;

    float maxVelocity;
    private void Awake()
    {
        canDash = true;
        dashCooldownStored = dashCooldown;

        player = ReInput.players.GetPlayer(0);
        maxVelocity = speed;
        playerRenderer = GetComponentInChildren<LookAtCamera>().gameObject;
        //playerAnim = GetComponentInChildren<Animator>();

        playerRenderer.name = gameObject.name + " Renderer";
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        h = player.GetAxisRaw("Move Horizontal");
        v = player.GetAxisRaw("Move Vertical");

        playerRenderer.transform.position = new Vector3(transform.position.x, transform.position.y - heightOffset, transform.position.z);

        if (player.GetButton("Dash") && dashCooldown == 0)
        {
            Dash();
        }
    }
    private void FixedUpdate()
    {
        if (WeaponCombo.canMove)
        {
            moveDir = orientation.forward * v + orientation.right * h;
            rb.AddForce(moveDir.normalized * speed * 10, ForceMode.Force);

            if (rb.velocity.magnitude > maxVelocity)
            {
                rb.velocity = rb.velocity.normalized * maxVelocity;
            }
        }

        if (canDash)
        {
            Vector3 dashDir = GetComponentInChildren<WeaponCombo>().knockbackOrientation.forward /*+ GetComponentInChildren<WeaponCombo>().knockbackOrientation.right*/;
            rb.AddForce(dashDir.normalized * dashSpeed * 10, ForceMode.Impulse);

            canDash = false;
        }

        DashTimer();
    }
    void Dash()
    {
        canDash = true;
        dashCooldown = dashCooldownStored;
    }
    void DashTimer()
    {
        if (dashCooldown != 0)
        {
            dashCooldown -= Time.fixedDeltaTime;

            if (dashCooldown < 0.001)
            {
                dashCooldown = 0;
            }
        }
    }
}
