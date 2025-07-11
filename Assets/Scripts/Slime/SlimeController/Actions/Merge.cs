using UnityEngine;
using UnityEngine.Events;

public class Merge : MonoBehaviour
{
    [SerializeField] private SlimeData slimeData;
    [SerializeField] private Divide divide;

    private SlimeStats stats1, stats2;

    private SlimeController minorController;
    private SlimeStats mainStats, minorStats;

    private GameObject mainSlime;
    private GameObject minorSlime;

    private Vector3 sizeIncrease;
    private int aux;

    public bool isMerging;

    public UnityEvent merge;

    public void MergeSlimes(GameObject slime1, GameObject slime2)
    {
        if (isMerging)
            return;
        isMerging = true;

        Debug.Log("Merge entre os slimes: " + slime1.name + " e " + slime2.name);

        //Obtem os stats dos slimes
        stats1 = slime1.GetComponent<SlimeStats>();

        stats2 = slime2.GetComponent<SlimeStats>();

        //Define o slime mais importante
        mainSlime = slime1;
        minorSlime = slime2;

        if (stats2.GetId() < stats1.GetId())
        {
            mainSlime = slime2;
            minorSlime = slime1;
        }

        //Obtém o controller e stats do slime menos importante
        mainStats = mainSlime.GetComponent<SlimeStats>();

        minorController = minorSlime.GetComponent<SlimeController>();
        minorStats = minorSlime.GetComponent<SlimeStats>();

        //Desativa o slime menos importante (SetInactiveState)
        minorController.SetInactiveState();

        //Aumenta o tamanho do slime mais importante
        sizeIncrease = slimeData.GetDivisionSizeReduction();
        sizeIncrease = minorStats.hasMerged ? sizeIncrease * 2 : sizeIncrease;

        mainSlime.transform.localScale += sizeIncrease;

        //Se o slime principal é uma cópia, torna hasMerged como True
        mainStats.hasMerged = mainSlime.CompareTag("PlayerCopy");

        //Se o slime principal é o original, diminui o contador de cópias do Divide
        if (mainSlime.CompareTag("Player"))
        {
            aux = minorStats.hasMerged ? 2 : 1;
            divide.copyCount -= aux;
        }

        //Invoca evento de merge
        merge.Invoke();
    }

    private void Update()
    {
        isMerging = false;
    }
}