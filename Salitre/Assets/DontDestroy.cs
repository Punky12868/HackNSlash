using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public enum Type { MainCamera, VirtualCamera, DollyCart, Dollytrack, Player, Canvas, CanvasTwo}
    public Type type;
    string gameObjectTag;
    void Awake()
    {
        switch (type)
        {
            case Type.MainCamera:
                gameObjectTag = "MainCamera";
                break;
            case Type.VirtualCamera:
                gameObjectTag = "a";
                break;
            case Type.DollyCart:
                gameObjectTag = "c";
                break;
            case Type.Dollytrack:
                gameObjectTag = "b";
                break;
            case Type.Player:
                gameObjectTag = "Player";
                break;
            case Type.Canvas:
                gameObjectTag = "d";
                break;
            case Type.CanvasTwo:
                gameObjectTag = "e";
                break;
            default:
                break;
        }

        GameObject[] objs = GameObject.FindGameObjectsWithTag(gameObjectTag);

        if (objs.Length > 1)
        {
            if (gameObjectTag == "Player")
            {
                objs[0].transform.position = this.transform.position;
                Destroy(this.gameObject);
            }
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }
}
