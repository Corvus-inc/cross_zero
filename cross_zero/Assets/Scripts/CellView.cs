using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CellView : MonoBehaviour
{
    private CellController _cellController;

    private void Start()
    {
        _cellController = new CellController();
    }
    private void OnMouseDown()
    {
        CellModel.Instance.targetCell = gameObject;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        _cellController.MakeMove();
        _cellController.DirectionsToWin();
        _cellController.CountingOfMoves();
        
        Debug.Log("тык"); 
    }
}
