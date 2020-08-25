using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellView : MonoBehaviour
{
    private CellController _cellController;

    private void Awake()
    {
        _cellController = new CellController();
    }
    private void OnMouseDown()
    {
        CellModel.Instance.targetCell = gameObject;


        _cellController.MakeMove();

        
        //Debug.Log(_cellController.ClculateCellHorisontal());
        //Debug.Log(_cellController.ClculateCellDiagonal());
    }
}
