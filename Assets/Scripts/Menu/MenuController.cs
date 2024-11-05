using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    GameObject menuPrincipal;

    [SerializeField]
    GameObject menuConfig;

    [SerializeField]
    GameObject menuControles;

    private void Start()
    {
        menuConfig.SetActive(false);
        menuControles.SetActive(false);

    }

    public void IniciarJogo()
    {
        SceneManager.LoadScene("Vinicius");
    }

    public void Configuracao()
    {
        menuConfig.SetActive(true);
        menuPrincipal.SetActive(false);
    }

    public void SairJogo()
    {
        Application.Quit();
    }

    public void ConfigControles()
    {
        menuConfig.SetActive(false);
        menuControles.SetActive(true);
    }

    public void ConfigVoltar()
    {
        menuConfig.SetActive(false);
        menuPrincipal.SetActive(true);
    }

    public void ControlesVoltar()
    {
        menuConfig.SetActive(true);
        menuControles.SetActive(false);
    }
}