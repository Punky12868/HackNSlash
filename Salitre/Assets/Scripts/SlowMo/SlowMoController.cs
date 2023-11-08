using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoController : MonoBehaviour
{
    [SerializeField] float slowmoTime;
    [SerializeField] float slowmoTimeScale;

    [SerializeField] float slowmoLerpMargen;
    [SerializeField] float slowmoDampingTime;

    public bool startSlowmo;
    bool ctRunning;
    private void Update()
    {
        if (startSlowmo && Time.timeScale != slowmoTimeScale)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, slowmoTimeScale, 1 - Mathf.Exp(-slowmoDampingTime * Time.unscaledDeltaTime));

            if (Time.timeScale < slowmoTimeScale + slowmoLerpMargen)
            {
                Time.timeScale = slowmoTimeScale;
            }
        }
        else if (!startSlowmo && Time.timeScale != 1)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, 1 - Mathf.Exp(-slowmoDampingTime * Time.unscaledDeltaTime));

            if (Time.timeScale > 1 - slowmoLerpMargen)
            {
                Time.timeScale = 1;
            }
        }

        if (Time.timeScale == slowmoTimeScale && startSlowmo)
        {
            if (!ctRunning)
            {
                StartCoroutine(SetNormalTime());
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            startSlowmo = true;
        }
        //Debug.Log(Time.timeScale);
    }
    IEnumerator SetNormalTime()
    {
        ctRunning = true;
        yield return new WaitForSeconds(slowmoTime);
        startSlowmo = false;
        ctRunning = false;
    }
}
