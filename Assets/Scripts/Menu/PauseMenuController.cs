using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController Instance;

    [SerializeField]
    GameObject menuPause;

    [SerializeField]
    GameObject menuConfig;

    [SerializeField]
    GameObject menuControles;

    public bool isPaused;

    private void Start()
    {
        Instance = this;

        menuConfig.SetActive(false);
        menuControles.SetActive(false);
        menuPause.SetActive(false);

    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
                Continuar();
            else
                PauseMenu();
        }

        HandleRestart();
    }

    public void PauseMenu()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0;

        isPaused = true;
    }

    public void Continuar()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1;

        isPaused = false;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Configuracao()
    {
        menuConfig.SetActive(true);
        menuPause.SetActive(false);
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void ConfigControles()
    {
        menuConfig.SetActive(false);
        menuControles.SetActive(true);
    }

    public void ConfigVoltar()
    {
        menuConfig.SetActive(false);
        menuPause.SetActive(true);
    }

    public void ControlesVoltar()
    {
        menuConfig.SetActive(true);
        menuControles.SetActive(false);
    }

    void HandleRestart()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
