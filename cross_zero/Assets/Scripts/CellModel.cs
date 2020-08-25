using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellModel : Singleton<CellModel>
{ 
    public GameObject targetCell;

    public int NumberPlayer = 0 ;

    
    public void PassTurn()
    {
        if (NumberPlayer == 1) NumberPlayer = 2;
        else NumberPlayer = 1;
    }
}
