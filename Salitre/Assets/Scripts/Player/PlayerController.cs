using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    private int playerID = 0;
    [SerializeField] Rewired.Player player;

    GameObject playerRenderer;
    Animator playerAnim;
    [SerializeField] float heightOffset;

    [SerializeField] Transform orientation;
    [SerializeField] Player _data;

    Rigidbody rb;
    Vector3 moveDir;
    float h, v;

    [HideInInspector] public float health;
    [HideInInspector] public float speed;
    float maxVelocity;
    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerID);

        health = _data.health;
        speed = _data.speed;
        maxVelocity = speed;

        playerRenderer = GetComponentInChildren<LookAtCamera>().gameObject;
        //playerAnim = GetComponentInChildren<Animator>();

        playerRenderer.name = gameObject.name + " Renderer";
        //playerRenderer.transform.SetParent(GameObject.FindGameObjectWithTag("Renderers").transform);
        
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        h = player.GetAxisRaw("Move Horizontal");
        v = player.GetAxisRaw("Move Vertical");

        playerRenderer.transform.position = new Vector3(transform.position.x, transform.position.y - heightOffset, transform.position.z);
    }
    private void FixedUpdate()
    {
        if (WeaponCombo.canAim)
        {
            moveDir = orientation.forward * v + orientation.right * h;
            rb.AddForce(moveDir.normalized * speed * 10, ForceMode.Force);

            if (rb.velocity.magnitude > maxVelocity)
            {
                rb.velocity = rb.velocity.normalized * maxVelocity;
            }
        }
    }
}
