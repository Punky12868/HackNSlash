using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnFade : MonoBehaviour
{
    [Tooltip ("Debug option, uncheck on build")]
    [SerializeField] bool usingInput;

    [SerializeField] GameObject[] fades;
    public UnityEvent onFadeInSpawn, onFadeInEnd, onFadeOutSpawn, onMidFadeOut, onFadeOutEnd, onLoadingScreenSpawn, onLoadingScreenEnd, onDoorFadeInSpawn, onDoorFadeInEnd;
    public void SpawnBasicFadeIn()
    {
        Instantiate(fades[0], transform);
        onFadeInSpawn.Invoke();

        Debug.Log("SpawnBasicFadeIn");
    }
    public void SpawnFadeInOut()
    {
        Instantiate(fades[1], transform);
        onFadeInSpawn.Invoke();

        Debug.Log("SpawnFadeInOut");
    }
    public void SpawnDoorFadeInOut()
    {
        Instantiate(fades[2], transform);
        onDoorFadeInSpawn.Invoke();

        Debug.Log("SpawnFadeInOut");
    }
    public void SpawnLoadingFadeIn()
    {
        Instantiate(fades[3], transform);
        onFadeInSpawn.Invoke();

        Debug.Log("SpawnLoadingFadeIn");
    }
    public void SpawnFadeOut()
    {
        Instantiate(fades[4], transform);
        onFadeOutSpawn.Invoke();

        Debug.Log("SpawnFadeOut");
    }
    public void SpawnLoadingScreen()
    {
        Instantiate(fades[5], transform);
        onLoadingScreenSpawn.Invoke();

        Debug.Log("SpawnLoadingScreen");
    }

    private void Update()
    {
        if (usingInput)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                SpawnBasicFadeIn();
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                SpawnFadeInOut();
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                SpawnLoadingFadeIn();
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                SpawnFadeOut();
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                SpawnLoadingScreen();
            }
        }
    }
}
