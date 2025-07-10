using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Divide : MonoBehaviour
{
    private List<GameObject> copies = new();
    private List<SlimeController> copyControllers = new();
    private GameObject copy;

    [SerializeField] private Transform slimeTransform;

    [SerializeField] private int copyLimit;
    private int copyCount;

    public UnityEvent divide;

    private void Start()
    {
        copies = GameObject.FindGameObjectsWithTag("PlayerCopy").ToList();

        foreach (GameObject copy in copies)
            copyControllers.Add(copy.GetComponent<SlimeController>());
    }

    public void DivideSlime(Vector2 direction)
    {
        //Verifica se atingiu o limite de cópias
        if (copyCount == copyLimit)
            return;

        //Seleciona a próxima cópia do vetor
        copy = copies[copyCount];

        //Ativa a cópia e a coloca na posição
        copyControllers[copyCount].isInactive = false;
        copy.transform.position = slimeTransform.position;

        //Invoca o evento de divisão
        divide.Invoke();

        //Inicia cópia no estado de jump na direção de divisão
        copyControllers[copyCount].nextJumpDirection = direction;
        copyControllers[copyCount].SetJumpState();

        //Incrementa o contador de cópias
        copyCount++;
    }
}