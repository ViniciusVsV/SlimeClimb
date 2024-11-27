using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController Instance;

    public bool isPaused;
    public bool configMenu;
    public bool controlsMenu;

    [SerializeField]
    Animator pauseAnimator;

    private void Start()
    {
        Instance = this;

    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = pauseAnimator.GetBool("Pausado");
            configMenu = pauseAnimator.GetBool("Configuracoes");
            controlsMenu = pauseAnimator.GetBool("Controles");


            if (controlsMenu)
            {
                DesativarMenu("Controles");
            }
            else if (configMenu)
            {
                DesativarMenu("Configuracoes");
            }
            else if (isPaused)
            {
                Continuar();
            }
            else
            {
                PauseMenu();
            }
        }
    }

    public void PauseMenu()
    {
        AtivarMenu("Pausado");
        Time.timeScale = 0;
        AudioController.instance.PlayButtonClip();
    }

    public void Continuar()
    {
        DesativarMenu("Pausado");
        Time.timeScale = 1;
        AudioController.instance.PlayButtonClip();
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        AudioController.instance.PlayButtonClip();
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
        AudioController.instance.PlayButtonClip();
    }

    // Funções para transição de Menu
    public void AtivarMenu(string menu)
    {
        pauseAnimator.SetBool(menu, true);
        AudioController.instance.PlayButtonClip();
    }

    public void DesativarMenu(string menu)
    {
        pauseAnimator.SetBool(menu, false);
        AudioController.instance.PlayButtonClip();
    }
}
