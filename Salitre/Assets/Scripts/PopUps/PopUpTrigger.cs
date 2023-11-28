using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour
{
    public enum popUps {Movement, Dash, Attack, Special, Orbit, Zoom, ChangeWeapon}
    public popUps popUpType;

    [SerializeField] bool spawnIzq;
    bool alreadyTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !alreadyTriggered)
        {
            if (spawnIzq)
            {
                PopUpSpawner.izqPopUp = true;
            }
            else
            {
                PopUpSpawner.izqPopUp = false;
            }

            switch (popUpType)
            {
                case popUps.Movement:
                    PopUpSpawner.MovementPopUp();
                    break;
                case popUps.Dash:
                    PopUpSpawner.DashPopUp();
                    break;
                case popUps.Attack:
                    PopUpSpawner.LightAttackPopUp();
                    break;
                case popUps.Special:
                    PopUpSpawner.SpecialAttackPopUp();
                    break;
                case popUps.Orbit:
                    PopUpSpawner.CameraOrbitPopUp();
                    break;
                case popUps.Zoom:
                    PopUpSpawner.ZoomPopUp();
                    break;
                case popUps.ChangeWeapon:
                    PopUpSpawner.ChangeWeaponPopUp();
                    break;
                default:
                    break;
            }

            alreadyTriggered = true;
        }
    }
}
