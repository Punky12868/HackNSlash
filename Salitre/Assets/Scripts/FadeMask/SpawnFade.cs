using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnFade : MonoBehaviour
{
    [SerializeField] bool runtime;

    [SerializeField] GameObject[] fades;
    public UnityEvent onFadeInSpawn, onFadeInEnd, onFadeOutSpawn, onMidFadeOut, onFadeOutEnd, onLoadingScreenSpawn, onLoadingScreenEnd, onDoorFadeInSpawn, onDoorFadeInEnd;
    public bool flashback;
    public bool nextLevelNoFlashback;
    private void Awake()
    {
        if (runtime)
        {
            SpawnFade[] spawnFadeScripts = FindObjectsOfType<SpawnFade>();

            for (int i = 0; i < spawnFadeScripts.Length; i++)
            {
                if (spawnFadeScripts[i] != this)
                {
                    spawnFadeScripts[i].onFadeInSpawn = onFadeInSpawn;
                    spawnFadeScripts[i].onFadeInEnd = onFadeInEnd;
                    spawnFadeScripts[i].onFadeOutSpawn = onFadeOutSpawn;
                    spawnFadeScripts[i].onMidFadeOut = onMidFadeOut;
                    spawnFadeScripts[i].onFadeOutEnd = onFadeOutEnd;
                    spawnFadeScripts[i].onLoadingScreenSpawn = onLoadingScreenSpawn;
                    spawnFadeScripts[i].onLoadingScreenEnd = onLoadingScreenEnd;
                    spawnFadeScripts[i].onDoorFadeInSpawn = onDoorFadeInSpawn;
                    spawnFadeScripts[i].onDoorFadeInEnd = onDoorFadeInEnd;

                    spawnFadeScripts[i].flashback = false;
                    spawnFadeScripts[i].nextLevelNoFlashback = false;
                }
            }
            Destroy(gameObject);
        }
    }
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
}
