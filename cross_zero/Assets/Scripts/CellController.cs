using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class CellController : MonoBehaviour
{
    //private void OnMouseDown()
    //{
    //    CellModel.Instance.targetCell = gameObject;


    //    MakeMove();

    //    Debug.Log(CellModel.Instance.numberPlayer);
    //}
    private void Awake()
    {
        CreateAllCells();
        SetTurnText(CellModel.Instance.numberPlayer);
    }

    public void MakeMove()
    {

        var spriteCell = CellModel.Instance.targetCell.GetComponent<SpriteRenderer>();
        var checkTurnCell = CellModel.Instance.targetCell.GetComponent<Cell>();

        if (CellModel.Instance.numberPlayer == 1 && checkTurnCell.CheckTurn == false)
        {

            spriteCell.sprite = CellModel.Instance.cross;
            SetTurnText(CellModel.Instance.numberPlayer = 2);
            checkTurnCell.Status = 1;
            CallFind(1);
        }
        else if (checkTurnCell.CheckTurn == false)
        {

            spriteCell.sprite = CellModel.Instance.zero;
            SetTurnText(CellModel.Instance.numberPlayer = 1);
            checkTurnCell.Status = 2;
            CallFind(2);
        }
        checkTurnCell.CheckTurn = true;
    }

    private void SetTurnText(int turn)
    {
        switch (turn)
        {
            case 1:
                CellModel.Instance.textTurns.text = "Turn Cross";
                break;
            case 2:
                CellModel.Instance.textTurns.text = "Turn Zero";
                break;
            default:
                CellModel.Instance.textTurns.text = "кто-то ходит";
                break;
        }
    }

    public void CreateAllCells()
    {

        for (int i = 0; i < 9; i++)
        {
            CellModel.Instance.allCells.Add(Instantiate(CellModel.Instance.PrefabCell));
        }
        foreach (var item in CellModel.Instance.allCells)
        {
            item.transform.parent = CellModel.Instance.GridCells.transform;
        }
    }

    public float ClculateCellHorisontal()
    {
        var RectCell1 = CellModel.Instance.allCells[0].GetComponent<RectTransform>().position;
        var RectCell2 = CellModel.Instance.allCells[1].GetComponent<RectTransform>().position;
        return Math.Abs(Vector3.Distance(RectCell1, RectCell2));
    }

    public float ClculateCellDiagonal()
    {
        var h = ClculateCellHorisontal();
        return (float)Math.Abs(Math.Sqrt(h * h + h * h));
    }
    public void CallFind(int status)
    {
        float h = ClculateCellHorisontal();
        var targetPos = CellModel.Instance.targetCell.GetComponent<RectTransform>().position;

        List<Vector3> coord = new List<Vector3>();
        coord.Add(new Vector3(h, 0, 0));
        coord.Add(new Vector3(h, h, 0));
        coord.Add(new Vector3(0, h, 0));
        coord.Add(new Vector3(-h, h, 0));
        coord.Add(new Vector3(-h, 0, 0));
        coord.Add(new Vector3(-h, -h, 0));
        coord.Add(new Vector3(0, -h, 0));
        coord.Add(new Vector3(h, -h, 0));

        foreach (var vect in coord)
        {
           Vector3 vectPos = vect + targetPos;
            foreach (var cell in CellModel.Instance.allCells)
            {
                Vector3 cellPos = cell.GetComponent<RectTransform>().position;
                int stat = cell.GetComponent<Cell>().Status;
                if (cellPos == vectPos && stat == status)
                {
                    Debug.Log("две нашел");
                    Vector3 winPos = cellPos + vect;
                    Vector3 winPos2 = targetPos + new Vector3(vect.x * -1, vect.y * -1, vect.z * -1);
                    foreach (var el in CellModel.Instance.allCells)
                    {
                        int stat2 = el.GetComponent<Cell>().Status;
                        if (el.GetComponent<RectTransform>().position == winPos && stat2 == status || el.GetComponent<RectTransform>().position == winPos2 && stat2 == status)
                        {
                           
                            Debug.Log("победа");
                            
                            Vector3[] tor = new Vector3[3] { targetPos, cellPos, winPos };
                            var linia = LineRenderer.Instantiate(CellModel.Instance.SinpleLine).GetComponent<LineRenderer>();
                            linia.SetPositions(tor);
                           return;
                        }
                    }
                }
            }
        }

    }
}

