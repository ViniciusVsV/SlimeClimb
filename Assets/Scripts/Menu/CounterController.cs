using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterController : MonoBehaviour{
    public int totalJumps;
    public int totalResets;
    public int totalDeaths;
    public int totalDivisions;

    //Contadores para cada seção
    public int sectionJumps;
    public int sectionResets;
    public int sectionDeaths;
    public int sectionDivisions;
    
    public void IncreaseJumps(){totalJumps++; sectionJumps++;}
    public void IncreaseResets(){totalResets++; sectionResets++;}
    public void IncreaseDeaths(){totalDeaths++; sectionDeaths++;}
    public void IncreaseDivisions(){totalDivisions++; sectionDivisions++;}
    
    public void ResetSection(){
        sectionJumps = 0;
        sectionDeaths = 0;  
        sectionResets = 0;
    }
}
