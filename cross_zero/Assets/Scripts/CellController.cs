using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CellController : MonoBehaviour
{
        
    private void OnMouseDown()
    {
        CellModel.Instance.targetCell = gameObject;
        MakeMove();

        Debug.Log(CellModel.Instance.NumberPlayer);
    }
    public void MakeMove()
    {
        
        var spriteCell = CellModel.Instance.targetCell.GetComponent<SpriteRenderer>();
        var checkTurnCell = GetComponent<Cell>();

        if (CellModel.Instance.NumberPlayer == 1 && checkTurnCell.CheckTurn==false )
        {
            spriteCell.sprite = CellModel.Instance.cross;
            CellModel.Instance.NumberPlayer = 2;
        }
        else if(checkTurnCell.CheckTurn == false)
        {
            spriteCell.sprite = CellModel.Instance.zero;
            CellModel.Instance.NumberPlayer = 1;
        }
        checkTurnCell.CheckTurn = true;
    }

}
