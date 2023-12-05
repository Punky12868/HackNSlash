using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject flashback;
    [HideInInspector] public int flashbackMenu;
    public bool isMenu;
    public void LoadFlashback()
    {
        Instantiate(flashback, transform);
    }
    public void LoadLevel(int i)
    {
        if (isMenu)
        {
            isMenu = false;
            flashbackMenu = i;
            //LoadFlashback();
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
    IEnumerator LoadAsynchronously(int i)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(i);
        yield return null;
    }
}
