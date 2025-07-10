using UnityEngine;
using UnityEngine.Events;

public class Merge : MonoBehaviour
{
    [SerializeField] private SlimeData slimeData;
    [SerializeField] private Divide divide;

    private SlimeStats stats1, stats2;

    private SlimeController mainController, minorController;
    private SlimeStats mainStats, minorStats;

    private GameObject mainSlime;
    private GameObject minorSlime;

    private Vector3 sizeIncrease;
    private int aux;

    public bool isMerging;

    public UnityEvent merge;

    public void MergeSlimes(GameObject slime1, GameObject slime2)
    {
        Debug.Log("Merge entre os slimes: " + slime1.name + " e " + slime2.name);

        //Obtem os stats dos slimes
        stats1 = slime1.GetComponent<SlimeStats>();

        stats2 = slime2.GetComponent<SlimeStats>();

        //Define o slime mais importante
        mainSlime = slime1;
        if (stats2.GetId() < stats1.GetId())
            mainSlime = slime2;

        //Obtém o controller e stats do slime mais e do menos importante
        mainController = mainSlime.GetComponent<SlimeController>();
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
}

/*void OnTriggerEnter2D(Collider2D other){
        if (isMerging)
            return;

        isMerging = true;

        if (other.CompareTag("Player"))
            MergePlayer();

        else if (other.CompareTag("CopyDetector"))
            MergeCopy(other);
    }

    void MergePlayer(){
        Debug.Log("Tentando fundir com o player. O meu id é: " + copyId);

        Vector3 sizeIncrease;

        if(hasMerged)
            sizeIncrease = new Vector3(0.5f, 0.5f);
        else
            sizeIncrease = new Vector3(0.25f, 0.25f);

        playerTransform.localScale += sizeIncrease;

        AudioController.instance.PlayMergeSound();

        Destroy(gameObject);
    }

    void MergeCopy(Collider2D other){
        Debug.Log("Tentando fundir com uma cópia. O meu id é: " + copyId);

        CopyController otherController = other.GetComponentInParent<CopyController>();
        int otherId = otherController.copyId;

        if (copyId < otherId)
        {
            transform.localScale += new Vector3(0.125f, 0.125f);
            
            AudioController.instance.PlayMergeSound();

            Destroy(other.transform.parent.gameObject);

            hasMerged = true;
        }

        else if (copyId > otherId)
        {
            other.transform.parent.localScale += new Vector3(0.125f, 0.125f);

            AudioController.instance.PlayMergeSound();

            Destroy(gameObject);

            otherController.hasMerged = true;   
        }

        HandleParticleValues();
    }*/