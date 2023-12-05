using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;
using EmeraldAI.Example;
using EmeraldAI.Utility;
using Unity.Mathematics;
using UnityEngine.AI;

public class Rooms : MonoBehaviour
{
    public bool alwaysActive;
    EmeraldAISystem[] enemys;
    DoorController[] doors;

    public bool activeRoom;
    public bool npcRoom;

    [SerializeField] GameObject[] allGo;
    [SerializeField] GameObject[] enemiesGo;
    [HideInInspector] public int i;
    bool killedAllEnemies;
    CheckPoint checkPoint;

    GameObject[] emptyArray = new GameObject[0];
    private void Awake()
    {
        checkPoint = GetComponentInChildren<CheckPoint>();
        doors = GetComponentsInChildren<DoorController>();

        GetEnemy();

        if (activeRoom)
        {
            ActivateRoom();
        }
        else
        {
            DeactivateRoom();
        }
    }
    public void ActivateRoom()
    {
        activeRoom = true;
        for (int i = 0; i < allGo.Length; i++)
        {
            allGo[i].SetActive(true);
        }

        if (enemiesGo.Length > 0)
        {
            ES3.Save("Enemies", enemiesGo);

            for (int i = 0; i < enemiesGo.Length; i++)
            {
                /*ES3.Save("EnemyDead" + i, enemiesGo[i].GetComponent<EmeraldAISystem>().IsDead);

                ES3.Save("EnemyHealth" + i, enemiesGo[i].GetComponent<EmeraldAISystem>().CurrentHealth);

                ES3.Save("EnemyState" + i, enemiesGo[i].GetComponent<EmeraldAISystem>().CurrentStateInfo);*/

                ES3.Save<Vector3>("EnemyPos" + i, enemiesGo[i].transform.position);
                ES3.Save<Quaternion>("EnemyRot" + i, enemiesGo[i].transform.rotation);
                ES3.Save<Vector3>("EnemyScale" + i, enemiesGo[i].transform.localScale);

                enemiesGo[i].GetComponent<EmeraldAIDetection>().enabled = true;
                enemiesGo[i].GetComponent<EmeraldAILookAtController>().enabled = true;
                enemiesGo[i].GetComponent<EmeraldAISystem>().enabled = true;
                enemiesGo[i].GetComponent<EmeraldAIEventsManager>().ResetAI();
                enemiesGo[i].GetComponent<EmeraldAIEventsManager>().ResetPath();

                Debug.Log("Saved Enemy " + enemiesGo[i].name);
            }
            Debug.Log("Saved All Enemys");
        }
    }
    public void DeactivateRoom()
    {
        if (!alwaysActive)
        {
            activeRoom = false;
            for (int i = 0; i < allGo.Length; i++)
            {
                allGo[i].SetActive(false);
            }
        }
    }
    public void ActivateDialog()
    {
        if (npcRoom)
        {
            GetComponentInChildren<DialogSystem>().ActivateDialog();
        }
    }
    public void CheckOpenDoor()
    {
        if (i == 0 && !killedAllEnemies)
        {
            killedAllEnemies = true;

            if (checkPoint != null)
            {
                checkPoint.gameObject.SetActive(true);
            }

            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].canEnter = true;
            }
        }
    }
    public void OnPlayerDead()
    {
        enemiesGo = ES3.Load("Enemies", enemiesGo);

        for (int i = 0; i < enemiesGo.Length; i++)
        {
            /*enemiesGo[i].GetComponent<EmeraldAISystem>().IsDead = ES3.Load<bool>("EnemyDead" + i);

            enemiesGo[i].GetComponent<EmeraldAISystem>().CurrentHealth = ES3.Load<int>("EnemyHealth" + i);

            enemiesGo[i].GetComponent<EmeraldAISystem>().CurrentStateInfo = ES3.Load<AnimatorStateInfo>("EnemyState" + i);*/

            enemiesGo[i].transform.position = ES3.Load<Vector3>("EnemyPos" + i);
            enemiesGo[i].transform.rotation = ES3.Load<Quaternion>("EnemyRot" + i);
            enemiesGo[i].transform.localScale = ES3.Load<Vector3>("EnemyScale" + i);

            enemiesGo[i].GetComponent<EmeraldAIEventsManager>().ResetAI();
            enemiesGo[i].GetComponent<EmeraldAIEventsManager>().ResetPath();
            
        }
        FindObjectOfType<EmeraldAIPlayerHealth>().currentCheckpoint.LoadPlayer();
    }
    void GetEnemy()
    {
        if (!npcRoom)
        {
            enemys = GetComponentsInChildren<EmeraldAISystem>();
            enemiesGo = new GameObject[enemys.Length];

            if (enemys.Length > 0)
            {
                i = enemys.Length;

                for (int i = 0; i < doors.Length; i++)
                {
                    doors[i].canEnter = false;
                }

                for (int i = 0; i < enemys.Length; i++)
                {
                    enemiesGo[i] = enemys[i].gameObject;
                }
            }
        }
    }

    /* public void ShowCurrentFollowTarget ()
        {
            Invoke("ShowCurrentFollowTargetDelayed", 1);
        }

        void ShowCurrentFollowTargetDelayed ()
        {
            Debug.Log(EmeraldComponent.CurrentFollowTarget.name);
        }*/
}
