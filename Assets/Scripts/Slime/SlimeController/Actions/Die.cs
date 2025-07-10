using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Die : MonoBehaviour
{
    [SerializeField] private SlimeController slimeController;
    [SerializeField] private Transform slimeTransform;
    [SerializeField] private ParticleSystem deathParticles;

    private List<SlimeStats> allSlimes = new();
    private Vector2 deathPosition;

    private bool gameOver;

    public UnityEvent<ParticleSystem, Vector2> die;

    private void Start()
    {
        allSlimes = FindObjectsByType<SlimeStats>(FindObjectsSortMode.None).ToList();
    }

    public void KillSlime()
    {
        //Salva a posição da morte
        deathPosition = slimeTransform.position;

        //Invoca o evento de morte
        die.Invoke(deathParticles, deathPosition);

        //Desativa o slime
        slimeController.SetInactiveState();

        //Checa se deu gameOver
        gameOver = true;

        foreach (SlimeStats slime in allSlimes)
        {
            if (!slime.isDead)
            {
                gameOver = false;
                break;
            }
        }

        if (gameOver)
            DeathMenuController.Instance.DeathMenu();
    }
}