using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] Material debugCanEnter;
    [SerializeField] Material debugCannotEnter;

    [SerializeField] Transform assignedDoor;
    [SerializeField] Transform tpPoint;
    [SerializeField] bool canEnter;

    public UnityEvent OnEnter;
    private void Awake()
    {
        if (assignedDoor == null || tpPoint == null || !canEnter)
        {
            GetComponent<Renderer>().material.color = debugCannotEnter.color;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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
