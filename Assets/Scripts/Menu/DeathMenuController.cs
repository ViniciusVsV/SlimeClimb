using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuController : MonoBehaviour
{
    public static DeathMenuController Instance;

    [SerializeField] Animator deathMenuAnimator;

    public bool isActive;

    private void Start()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isActive)
            deathMenuAnimator.SetBool("ativado", false);
    }

    public void DeathMenu()
    {
        deathMenuAnimator.SetBool("ativado", true);
        isActive = true;
    }

    public void Reset()
    {
        deathMenuAnimator.SetBool("ativado", false);
        isActive = false;

        CheckpointController checkpointController = FindFirstObjectByType<CheckpointController>();
        checkpointController.Reset();
    }

    public void MenuPrincipal()
    {
        isActive = false;
        SceneManager.LoadScene("Menu");
        AudioController.Instance.PlayButtonClip();
    }
}
