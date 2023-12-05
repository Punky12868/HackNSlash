using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcId : MonoBehaviour
{
    public int ID;

    private void Awake()
    {
        GetComponent<OnEnemyDeath>().npcId = this;
    }
}
