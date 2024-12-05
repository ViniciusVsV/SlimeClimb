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
    GameObject levelButtons;

    public Button[] buttons;
    LoadCheckpoint loadCheckpoint;

    private void Awake()
    {
        ButtonsToArray();

        for (int i = 1; i <= buttons.Length; i++){
            if(PlayerPrefs.GetInt($"checkpoint{i}", 0) == 1)
                buttons[i - 1].interactable = true;
            else
                buttons[i - 1].interactable = false;
        }
    }

    void Start(){
        loadCheckpoint = FindFirstObjectByType<LoadCheckpoint>();

        AudioController.instance.PlayBackgroundMusic();
    }

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

    void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];

        for (int i = 0; i < childCount; i++) {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}