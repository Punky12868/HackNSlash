using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public enum FadeType {FadeIn, FadeOut}
    public FadeType type;

    [SerializeField] Transform mask;

    [SerializeField] float lerpDamping;

    [SerializeField] float fadeInitialScale;
    [SerializeField] float fadeFinalScale;

    [SerializeField] float margen;

    [SerializeField] bool spawnFade;
    [SerializeField] bool spawnLoadingPanel;
    [SerializeField] bool doorFade;

    Vector3 finalScale;
    Vector3 errorMargen;

    int i;
    private void Awake()
    {
        mask.localScale = new Vector3(fadeInitialScale, fadeInitialScale);
        finalScale = new Vector3(fadeFinalScale, fadeFinalScale);
        errorMargen = new Vector3(margen, margen);
    }
    private void Update()
    {
        switch (type)
        {
            case FadeType.FadeIn:

                if (mask.localScale != finalScale)
                {
                    mask.localScale = Vector3.Lerp(mask.localScale, finalScale, 1 - Mathf.Exp(-lerpDamping * Time.unscaledDeltaTime));

                    if (doorFade && mask.localScale.x < finalScale.x + errorMargen.x)
                    {
                        doorFade = false;
                        FindObjectOfType<SpawnFade>().onDoorFadeInEnd.Invoke();
                    }
                }
                else
                {
                    Destroy(gameObject);
                    FindObjectOfType<SpawnFade>().onFadeInEnd.Invoke();

                    if (spawnFade)
                    {
                        FindObjectOfType<SpawnFade>().SpawnFadeOut();
                    }
                    else if (spawnLoadingPanel)
                    {
                        FindObjectOfType<SpawnFade>().SpawnLoadingScreen();
                    }
                }

                break;
            case FadeType.FadeOut:

                if (mask.localScale != finalScale)
                {
                    mask.localScale = Vector3.Lerp(mask.localScale, finalScale, 1 - Mathf.Exp(-lerpDamping * Time.unscaledDeltaTime));

                    if (mask.localScale.x > finalScale.x - errorMargen.x)
                    {
                        mask.localScale = finalScale;
                    }
                    else if (mask.localScale.x > finalScale.x / 2 && i == 0)
                    {
                        i++;
                        FindObjectOfType<SpawnFade>().onMidFadeOut.Invoke();
                    }
                }
                else
                {
                    Destroy(gameObject);
                    FindObjectOfType<SpawnFade>().onFadeOutEnd.Invoke();
                }

                break;
            default:
                break;
        }
    }
}
