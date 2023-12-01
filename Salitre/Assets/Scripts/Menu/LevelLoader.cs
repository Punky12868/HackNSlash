using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject flashback;
    public void LoadFlashback()
    {
        Instantiate(flashback, transform);
    }
    public void LoadLevel(int i)
    {
        StartCoroutine(LoadAsynchronously(i));
    }
    IEnumerator LoadAsynchronously(int i)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(i);
        yield return null;
    }
}
