using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [SerializeField] AudioClip[] musicClip;
    [SerializeField] AudioClip[] sfxClip;

    static AudioSource music;
    static AudioSource sfx;

    public static AudioClip[] musicClips;
    public static AudioClip[] sfxClips;

    public int startingMusicClip;
    public float fadeDuration;

    static int clipScriptID;
    static bool isPlaying;
    private void Awake()
    {
        PlaySound[] objs = FindObjectsOfType<PlaySound>();

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        //------------------//

        music = musicSource;
        sfx = sfxSource;

        musicClips = musicClip;
        sfxClips = sfxClip;

        PlayMusic(startingMusicClip);
    }
    private void Update()
    {
        if (isPlaying)
        {
            isPlaying = false;
            StartCoroutine(FadeOutAndChangeClip(clipScriptID));
        }
    }
    public static void PlayMusic(int clipID)
    {
        clipScriptID = clipID;
        isPlaying = true;
    }
    public static void PlaySFX(int clipID)
    {
        sfx.clip = sfxClips[clipID];
        sfx.Play();
    }
    private IEnumerator FadeOutAndChangeClip(int clipID)
    {
        float startVolume = music.volume;

        if (music.isPlaying)
        {
            while (music.volume > 0)
            {
                music.volume -= startVolume * Time.deltaTime / fadeDuration;
                yield return null;
            }
        }
        else
        {
            music.volume = 0;
        }

        music.clip = musicClips[clipID];
        music.Play();

        while (music.volume < startVolume)
        {
            music.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
    }
}
