using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] Material debugCanEnter;
    [SerializeField] Material debugCannotEnter;

    [HideInInspector] public Transform tpPoint;
    [SerializeField] Transform assignedDoor;
    public bool canEnter;
    public bool nextLevel;
    public int levelID;

    public bool flashback;

    UnityEvent OnEnter;
    private void Awake()
    {
        OnEnter = FindObjectOfType<AssignEvent>().OnDoorEnter;

        if (!nextLevel)
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
        else
        {
            tpPoint = transform.GetChild(0);
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

            if (nextLevel && !flashback)
            {
                FindObjectOfType<SpawnFade>().nextLevelNoFlashback = true;
            }
            else if (nextLevel && flashback)
            {
                FindObjectOfType<SpawnFade>().flashback = true;
            }
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
        if (!nextLevel)
        {
            tpPoint.gameObject.GetComponentInParent<Rooms>().ActivateDialog();
        }
    }
    public void OpenCloseDoor(bool b)
    {
        if (!b)
        {
            canEnter = false;
        }
        else
        {
            canEnter = true;
        }
    }
}
