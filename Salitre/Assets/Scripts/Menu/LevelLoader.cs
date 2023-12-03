using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject flashback;
    [HideInInspector] public int flashbackMenu;
    public bool isMenu;
    public void LoadFlashback()
    {
        Instantiate(flashback, GameObject.FindGameObjectWithTag("e").transform);
    }
    public void LoadLevel(int i)
    {
        if (isMenu)
        {
            isMenu = false;
            flashbackMenu = i;
            LoadFlashback();
        }
        else
        {
            StartCoroutine(LoadAsynchronously(i));
        }
    }
    IEnumerator LoadAsynchronously(int i)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(i);
        yield return null;
    }
}
