using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    Animator menuAnimator;
    [SerializeField]
    LoadCheckpoint loadCheckpoint;

    public void SairJogo()
    {
        Application.Quit();
    }

    // Funções para transição de Menu
    public void AtivarMenu(string menu)
    {
        menuAnimator.SetBool(menu, true);
        AudioController.instance.PlayButtonClip();
    }

    public void DesativarMenu(string menu)
    {
        menuAnimator.SetBool(menu, false);
        AudioController.instance.PlayButtonClip();
    }

    // Funções do Seletor de Fase
    public void CarregarFase(int index)
    {
        loadCheckpoint.index = index;

        SceneManager.LoadScene("Levels");
    }

}