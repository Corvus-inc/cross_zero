using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CellView : MonoBehaviour
{
    private CellController _cellController;

    private void Start()
    {
        _cellController = new CellController();
    }
    private void Update()
    {
        var hit = new RaycastHit();

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                // Construct a ray from the current touch coordinates.
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                if (Physics.Raycast(ray, out hit))
                {
                    hit.transform.gameObject.SendMessage("OnMouseDown");
                }
            }
        }
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
