using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

public class EnemyDeath : MonoBehaviour
{
    Rooms designedRoom;
    EmeraldAISystem enemySystem;

    int i;
    bool npc;
    private void Start()
    {
        i = 1;

        enemySystem = GetComponent<EmeraldAISystem>();
        designedRoom = GetComponentInParent<Rooms>();

        npc = designedRoom.npcRoom;

        if (!npc)
        {
            designedRoom.enemysAlive++;
        }
    }
    private void Update()
    {
        if (enemySystem.IsDead && !npc && i == 1)
        {
            i--;
            designedRoom.enemysAlive--;
        }
    }
}
