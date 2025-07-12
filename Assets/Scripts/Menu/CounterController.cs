using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterController : MonoBehaviour
{
    public static CounterController Instance;

    public int totalJumps = 0;
    public int totalResets = 0;
    public int totalDeaths = 0;
    public int totalDivisions = 0;

    //Contadores para cada seção
    public int sectionJumps = 0;
    public int sectionResets = 0;
    public int sectionDeaths = 0;
    public int sectionDivisions = 0;

    [SerializeField] GameObject totalJumpsDisplay;
    [SerializeField] GameObject totalResetsDisplay;
    [SerializeField] GameObject totalDeathsDisplay;
    [SerializeField] GameObject totalDivisionsDisplay;

    [SerializeField] GameObject sectionJumpsDisplay;
    [SerializeField] GameObject sectionResetsDisplay;
    [SerializeField] GameObject sectionDeathsDisplay;
    [SerializeField] GameObject sectionDivisionsDisplay;

    private void Start()
    {
        Instance = this;
    }

    public void IncreaseJumps()
    {
        totalJumps++;
        sectionJumps++;
        totalJumpsDisplay.GetComponent<TextMeshProUGUI>().text = totalJumps.ToString();
        sectionJumpsDisplay.GetComponent<TextMeshProUGUI>().text = sectionJumps.ToString();
    }
    public void IncreaseResets()
    {
        totalResets++;
        sectionResets++;
        totalResetsDisplay.GetComponent<TextMeshProUGUI>().text = totalResets.ToString();
        sectionResetsDisplay.GetComponent<TextMeshProUGUI>().text = sectionResets.ToString();
    }
    public void IncreaseDeaths()
    {
        totalDeaths++;
        sectionDeaths++;
        totalDeathsDisplay.GetComponent<TextMeshProUGUI>().text = totalDeaths.ToString();
        sectionDeathsDisplay.GetComponent<TextMeshProUGUI>().text = sectionDeaths.ToString();
    }
    public void IncreaseDivisions()
    {
        totalDivisions++;
        sectionDivisions++;
        totalDivisionsDisplay.GetComponent<TextMeshProUGUI>().text = totalDivisions.ToString();
        sectionDivisionsDisplay.GetComponent<TextMeshProUGUI>().text = sectionDivisions.ToString();
    }

    public void ResetSection()
    {
        sectionJumps = 0;
        sectionDeaths = 0;
        sectionResets = 0;
        sectionDivisions = 0;

        sectionJumpsDisplay.GetComponent<TextMeshProUGUI>().text = sectionJumps.ToString();
        sectionDeathsDisplay.GetComponent<TextMeshProUGUI>().text = sectionDeaths.ToString();
        sectionResetsDisplay.GetComponent<TextMeshProUGUI>().text = sectionResets.ToString();
        sectionDivisionsDisplay.GetComponent<TextMeshProUGUI>().text = sectionResets.ToString();
    }
}
