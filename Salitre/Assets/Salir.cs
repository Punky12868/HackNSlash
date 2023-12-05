using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Salir : MonoBehaviour
{
    public bool creditsScene;
    public bool salirMainMenu;
    public float timer;
    public void SalirJuego()
    {
        if (salirMainMenu)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            Application.Quit();
        }
    }

    public void CreditosScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    private void Update()
    {
        if (creditsScene)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
