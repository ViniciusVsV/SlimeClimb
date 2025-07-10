using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Death : BaseState
{
    [SerializeField] private Transform slimeTransform;
    [SerializeField] private ParticleSystem deathParticles;
    private List<SlimeController> allSlimes = new();
    private Vector2 deathPosition;

    private bool gameOver;

    public UnityEvent<ParticleSystem, Vector2> death;

    private void Start()
    {
        allSlimes = FindObjectsByType<SlimeController>(FindObjectsSortMode.None).ToList();
    }

    public override void StateEnter()
    {
        //Salva a posição da morte
        deathPosition = slimeTransform.position;

        //Move para posição origem e inclina a rotação
        slimeTransform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0f, 0f, 30f));

        //Invoca o evento de morte
        death.Invoke(deathParticles, deathPosition);

        //Desativa o game object
        controller.isInactive = true;

        //Checa se deu gameOver
        gameOver = true;

        foreach (SlimeController slime in allSlimes)
        {
            if (!slime.isInactive)
            {
                gameOver = false;
                break;
            }
        }

        if (gameOver)
            DeathMenuController.Instance.DeathMenu();
    }
}