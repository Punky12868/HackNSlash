using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] Material debugCanEnter;
    [SerializeField] Material debugCannotEnter;

    public Transform tpPoint;
    [SerializeField] Transform assignedDoor;
    public bool canEnter;

    public UnityEvent OnEnter;
    private void Awake()
    {
        if (assignedDoor != null)
        {
            tpPoint = assignedDoor.GetChild(0);
        }
        else if (assignedDoor == null || !canEnter)
        {
            canEnter = false;
            GetComponent<Renderer>().material.color = debugCannotEnter.color;
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
            FindObjectOfType<AllRooms>().currentDoor = this;
            OnEnter.Invoke();
        }
    }
    public void TpPlayer()
    {
        tpPoint.gameObject.GetComponentInParent<Rooms>().ActivateRoom();
        FindObjectOfType<PlayerInput>().transform.position = tpPoint.position;
    }
    public void CheckIfNpcRoom()
    {
        tpPoint.gameObject.GetComponentInParent<Rooms>().ActivateDialog();
    }
}
