using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuController : MonoBehaviour
{
    public static DeathMenuController Instance;

    [SerializeField] Animator deathMenuAnimator;

    private void Start()
    {
        Instance = this;
    }

    public void DeathMenu(){
        deathMenuAnimator.SetBool("ativado", true);
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
        AudioController.instance.PlayButtonClip();
    }
}
