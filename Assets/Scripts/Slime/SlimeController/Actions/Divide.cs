using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Divide : MonoBehaviour
{
    [SerializeField] private GameObject[] copies;
    private List<SlimeController> copyControllers = new();

    private GameObject copy;

    [SerializeField] private SlimeData slimeData;
    [SerializeField] private Transform slimeTransform;

    [SerializeField] private int copyLimit;
    public int copyCount;

    public UnityEvent divide;

    private void Start()
    {
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

        //Reduz o tamanho do original
        slimeTransform.localScale -= slimeData.GetDivisionSizeReduction();

        //Atribui o novo tamanho do original à cópia
        copy.transform.localScale = slimeTransform.localScale;

        //Coloca a cópia em posição
        copy.transform.position = slimeTransform.position;

        //Invoca o evento de divisão
        divide.Invoke();

        //Inicia cópia no estado de jump na direção de divisão
        copyControllers[copyCount].nextJumpDirection = direction;
        copyControllers[copyCount].SetJumpState(true);

        //Incrementa o contador de cópias
        copyCount++;
    }
}