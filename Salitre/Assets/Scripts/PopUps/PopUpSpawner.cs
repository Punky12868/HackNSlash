using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PopUpSpawner : MonoBehaviour
{
    [SerializeField] Transform popUpSpawnPoint;
    static Transform staticPopUpSpawnPoint;

    [SerializeField] Transform izqStartPos, izqEndPos, derStartPos, derEndPos;
    public static Transform izqStaticStartPos, izqStaticEndPos, derStaticStartPos, derStaticEndPos;
    public static bool izqPopUp;

    [SerializeField] GameObject[] allPopUps;
    static GameObject[] allStaticPopUps;

    [SerializeField] UnityEvent[] onAwake, onCompleted;
    public static UnityEvent[] onAwakeStatic, onCompletedStatic;
    private void Awake()
    {
        onAwakeStatic = onAwake;
        onCompletedStatic = onCompleted;

        staticPopUpSpawnPoint = popUpSpawnPoint;

        izqStaticStartPos = izqStartPos;
        izqStaticEndPos = izqEndPos;
        derStaticStartPos = derStartPos;
        derStaticEndPos = derEndPos;

        allStaticPopUps = allPopUps;
    }
    public static void MovementPopUp()
    {
        Instantiate(allStaticPopUps[0], staticPopUpSpawnPoint);
    }
    public static void DashPopUp()
    {
        Instantiate(allStaticPopUps[1], staticPopUpSpawnPoint);
    }
    public static void LightAttackPopUp()
    {
        Instantiate(allStaticPopUps[2], staticPopUpSpawnPoint);
    }
    public static void SpecialAttackPopUp()
    {
        Instantiate(allStaticPopUps[3], staticPopUpSpawnPoint);
    }
    public static void CameraOrbitPopUp()
    {
        Instantiate(allStaticPopUps[4], staticPopUpSpawnPoint);
    }
    public static void ZoomPopUp()
    {
        Instantiate(allStaticPopUps[5], staticPopUpSpawnPoint);
    }
    public static void ChangeWeaponPopUp()
    {
        Instantiate(allStaticPopUps[6], staticPopUpSpawnPoint);
    }
}
