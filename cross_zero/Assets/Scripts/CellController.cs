using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public Sprite cross;
    public Sprite zero;

    private void OnMouseDown()
    {
        CellModel.Instance.targetCell = gameObject;
        MakeMove();

        Debug.Log(CellModel.Instance.NumberPlayer);
    }
    public void MakeMove()
    {
        
        var spriteCell = CellModel.Instance.targetCell.GetComponent<SpriteRenderer>();

        if (CellModel.Instance.NumberPlayer == 1)
        {
            spriteCell.sprite = cross;
            CellModel.Instance.PassTurn();
        }
        else
        {
            spriteCell.sprite = zero;
            CellModel.Instance.PassTurn();
        }
    }

}
