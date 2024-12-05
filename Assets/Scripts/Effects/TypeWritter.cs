using System.Collections;
using TMPro;
using UnityEngine;

public class Typewriter : MonoBehaviour
{
    [SerializeField] private TMP_Text textMeshPro; // Referência ao componente TextMeshPro
    [SerializeField] private float delayBetweenLetters; // Tempo entre cada letra
    [SerializeField] private string fullText; // O texto completo a ser exibido

    private string currentText = "";

    private void OnEnable()
    {
        if (textMeshPro == null)
            textMeshPro = GetComponent<TMP_Text>();
        
        StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        textMeshPro.text = ""; // Garante que o texto inicie vazio

        for (int i = 0; i < fullText.Length; i++)
        {
            currentText += fullText[i];
            textMeshPro.text = currentText;
            yield return new WaitForSeconds(delayBetweenLetters);
        }

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}