﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellModel : Singleton<CellModel>
{
    public Sprite cross;
    public Sprite zero;

    public int row;
    public int column;
    public int numOfMoves; 
    public int countingOfMoves;
    public bool dontWin;

    public int zeroWinnes;
    public int crosswWinnes;

    public GameObject targetCell;
    public int numberNextPlayer;

    public Canvas screenCanvas;
    public Text textTurns;
    public Text textZero;
    public Text textCross;

    public GameObject simpleLine;
    public GameObject PrefabCell;
    public GameObject GridCells;
    public GameObject dial;
    public GameObject[,] allCells;


}
