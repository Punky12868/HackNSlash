using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] Material debugCanEnter;
    [SerializeField] Material debugCannotEnter;

    Transform tpPoint;
    [SerializeField] Transform assignedDoor;
    [SerializeField] bool canEnter;

    public UnityEvent OnEnter;
    private void Awake()
    {
        if (assignedDoor == null || !canEnter)
        {
            canEnter = false;
            GetComponent<Renderer>().material.color = debugCannotEnter.color;
        }
        else if (assignedDoor != null)
        {
            tpPoint = assignedDoor.GetChild(0);
        }
    }
    private void Update()
    {
        if (!canEnter && GetComponent<Renderer>().material.color != debugCannotEnter.color)
        {
            GetComponent<Renderer>().material.color = debugCannotEnter.color;
        }
        else if (canEnter && GetComponent<Renderer>().material.color != debugCanEnter.color)
        {
            GetComponent<Renderer>().material.color = debugCanEnter.color;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canEnter)
        {
            FindObjectOfType<AllDoors>().currentDoor = this;
            OnEnter.Invoke();
        }
    }
    public void TpPlayer()
    {
        FindObjectOfType<PlayerInput>().transform.position = tpPoint.position;
    }
}
