using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    Rooms designatedroom;
    public GameObject player;
    IndividualPos[] weaponSelect;
    bool cannotBeUsed;
    private void Awake()
    {
        designatedroom = GetComponentInParent<Rooms>();
        player = GameObject.FindGameObjectWithTag("Player");
        weaponSelect = FindObjectsOfType<IndividualPos>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !cannotBeUsed)
        {
            SavePlayer();
            cannotBeUsed = true;
        }
    }
    public void SavePlayer()
    {
        player.GetComponent<EmeraldAI.Example.EmeraldAIPlayerHealth>().currentCheckpoint = this;

        ES3.Save("CheckPoint" + gameObject.name, cannotBeUsed);

        ES3.Save("PlayerPosition", player.transform);
        ES3.Save("PlayerHealth", player.GetComponent<EmeraldAI.Example.EmeraldAIPlayerHealth>().CurrentHealth);
        ES3.Save("PlayerHealthUI", player.GetComponent<HealthSlider>().slider.value);
        ES3.Save("PlayerSpecialAttack", GameObject.FindGameObjectWithTag("PowerSlider").GetComponent<UnityEngine.UI.Slider>().value);

        for (int i = 0; i < weaponSelect.Length; i++)
        {
            ES3.Save("WeaponType" + i, weaponSelect[i].weaponType);
            ES3.Save("WeaponDamage" + i, weaponSelect[i].damage);
            ES3.Save("WeaponPos" + i, weaponSelect[i].i);
            ES3.Save("WeaponBlock" + i, weaponSelect[i].blocked);
        }

        Debug.Log("Saved");
    }
    public void LoadPlayer()
    {
        designatedroom.ActivateRoom();
        designatedroom.GetComponentInChildren<DoorController>().assignedDoor.GetComponentInParent<Rooms>().DeactivateRoom();

        cannotBeUsed = ES3.Load<bool>("CheckPoint" + gameObject.name);

        player.transform.position = ES3.Load<Transform>("PlayerPosition").position;
        player.GetComponent<EmeraldAI.Example.EmeraldAIPlayerHealth>().CurrentHealth = ES3.Load<int>("PlayerHealth");
        player.GetComponent<HealthSlider>().slider.value = ES3.Load<float>("PlayerHealthUI");
        GameObject.FindGameObjectWithTag("PowerSlider").GetComponent<UnityEngine.UI.Slider>().value = ES3.Load<float>("PlayerSpecialAttack");

        for (int i = 0; i < weaponSelect.Length; i++)
        {
            weaponSelect[i].weaponType = ES3.Load<IndividualPos.Type>("WeaponType" + i);
            weaponSelect[i].damage = ES3.Load<int>("WeaponDamage" + i);
            weaponSelect[i].i = ES3.Load<int>("WeaponPos" + i);
            weaponSelect[i].blocked = ES3.Load<bool>("WeaponBlock" + i);
        }

        Debug.Log("Loaded");
    }

}
