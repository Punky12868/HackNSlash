using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    float h, v;

    [SerializeField] Transform orientation;

    [SerializeField] float speed;
    [SerializeField] float maxVelocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(h, 0, v) * speed, ForceMode.Force);

        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }
}
