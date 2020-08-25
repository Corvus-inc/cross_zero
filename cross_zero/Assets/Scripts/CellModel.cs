using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellModel : Singleton<CellModel>
{
    public Sprite cross;
    public Sprite zero;

    public GameObject targetCell;
    public int numberPlayer = 0 ;

    public Canvas screenCanvas;
    public Text textTurns;

    public GameObject SinpleLine;
    public GameObject PrefabCell;
    public GameObject GridCells;
    public List<GameObject> allCells;
    
   
    
}
