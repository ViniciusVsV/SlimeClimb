using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Death : BaseState
{
    [SerializeField] private Transform slimeTransform;
    [SerializeField] private ParticleSystem deathParticles;
    private List<SlimeController> allSlimes = new();

    private bool gameOver;

    public UnityEvent<ParticleSystem> death;

    private void Start()
    {
        allSlimes = FindObjectsByType<SlimeController>(FindObjectsSortMode.None).ToList();
    }

    public override void StateEnter()
    {
        //Invoca o evento de morte
        death.Invoke(deathParticles);

        //Move para posição origem e inclina a rotação
        slimeTransform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(new Vector3(0f, 0f, 30f)));

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

        slimeTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 30f));

        if (gameOver)
            DeathMenuController.Instance.DeathMenu();
    }
}