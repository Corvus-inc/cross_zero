using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class CellController : MonoBehaviour
{
    private Cell checkTurnCell;
    private int countWin;
    private Vector2 checkDirection;

    private int row;
    private int column;

    private void Awake()
    {
        row = CellModel.Instance.row;
        column = CellModel.Instance.column;

        CellModel.Instance.numOfMoves = row * column;
        CellModel.Instance.countingOfMoves = 0;
        CellModel.Instance.dontWin = true;

        CreateAllCells(row, column);
        SetTurnText(CellModel.Instance.numberNextPlayer);


    }

    public void MakeMove()
    {
        checkTurnCell = CellModel.Instance.targetCell.GetComponent<Cell>();
        var spriteCell = CellModel.Instance.targetCell.GetComponent<SpriteRenderer>();


        if (CellModel.Instance.numberNextPlayer == 1 && checkTurnCell.CheckTurn == false)
        {

            spriteCell.sprite = CellModel.Instance.cross;
            SetTurnText(CellModel.Instance.numberNextPlayer = 2);
            checkTurnCell.Status = 1;

        }
        else if (checkTurnCell.CheckTurn == false)
        {

            spriteCell.sprite = CellModel.Instance.zero;
            SetTurnText(CellModel.Instance.numberNextPlayer = 1);
            checkTurnCell.Status = 2;

        }
        checkTurnCell.CheckTurn = true;
        CellModel.Instance.countingOfMoves++;
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
            case 3:
                CellModel.Instance.textTurns.text = "Win Zero";
                break;
            case 4:
                CellModel.Instance.textTurns.text = "Win Cross";
                break;
            default:
                CellModel.Instance.textTurns.text = "кто-то ходит";
                break;
        }
    }

    public void CreateAllCells(int column, int row) // Метод создания и сортировки двумерного массива с клетками. С указанием столбцов и колонок в классе клетки.
    {
        int countCell = column * row;
        GameObject[,] allCells = new GameObject[row, column];

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                allCells[i, j] = Instantiate(CellModel.Instance.PrefabCell);
                allCells[i, j].GetComponent<Cell>().rowCell = i;
                allCells[i, j].GetComponent<Cell>().columnCell = j;

            }

        }
        foreach (var item in allCells)
        {
            item.transform.SetParent(CellModel.Instance.GridCells.transform);
        }
        CellModel.Instance.allCells = allCells;
        CellModel.Instance.GridCells.GetComponent<RectTransform>().sizeDelta = new Vector2(row, column); //Меняет размеры рамки для того, чтобы порядок соответствовал матрице(клонки и строки).        
    }

    public void DirectionsToWin()
    {
        List<Vector2> directionsToWin = new List<Vector2>();
        directionsToWin.Add(new Vector2(1, 0));
        directionsToWin.Add(new Vector2(1, 1));
        directionsToWin.Add(new Vector2(0, 1));
        directionsToWin.Add(new Vector2(-1, 1));
        directionsToWin.Add(new Vector2(-1, 0));
        directionsToWin.Add(new Vector2(-1, -1));
        directionsToWin.Add(new Vector2(0, -1));
        directionsToWin.Add(new Vector2(1, -1));


        foreach (var direction in directionsToWin)
        {
            if (CellModel.Instance.dontWin == true)
            {
                checkDirection = direction; 
                StartFindWin(checkTurnCell);
            }
           
        }


    }

    private void StartFindWin(Cell checkTurnCell)
    {
        row = CellModel.Instance.row;
        column = CellModel.Instance.column;
        Vector2 start = new Vector2(checkTurnCell.rowCell, checkTurnCell.columnCell);

        Vector2 firstStep = start + checkDirection;
        if ((firstStep.x > -1 && firstStep.x < row && firstStep.y > -1 && firstStep.y < column) && (CellModel.Instance.allCells[(int)firstStep.x, (int)firstStep.y].GetComponent<Cell>().Status == checkTurnCell.Status))
        {
            Debug.Log("сделал шаг?делай второй");
            Vector2 secondStep = firstStep + checkDirection;

            Vector2 invertDirection = new Vector2(checkDirection.x * -1, checkDirection.y * -1);
            Vector2 backStep = new Vector2(checkTurnCell.rowCell + invertDirection.x, checkTurnCell.columnCell + invertDirection.y);

            if ((secondStep.x > -1 && secondStep.x < row && secondStep.y > -1 && secondStep.y < column) && (CellModel.Instance.allCells[(int)secondStep.x, (int)secondStep.y].GetComponent<Cell>().Status == checkTurnCell.Status))
            {
                Debug.Log("3 в ряд");

                Vector3 line1 = CellModel.Instance.allCells[(int)start.x, (int)start.y].transform.position;//Линия
                Vector3 line2 = CellModel.Instance.allCells[(int)secondStep.x, (int)secondStep.y].transform.position;
                Vector3[] tor = new Vector3[2] { line1, line2 };
                var linia = LineRenderer.Instantiate(CellModel.Instance.simpleLine).GetComponent<LineRenderer>();
                linia.SetPositions(tor);

                StopGame();
                CellModel.Instance.dial.SetActive(true);
                CountWined(checkTurnCell.Status);
                CellModel.Instance.countingOfMoves = 0;
                CellModel.Instance.dontWin = false;
                return;
            }
            else if ((backStep.x > -1 && backStep.x < row && backStep.y > -1 && backStep.y < column) && (CellModel.Instance.allCells[(int)backStep.x, (int)backStep.y].GetComponent<Cell>().Status == checkTurnCell.Status))
            {
                Debug.Log("не в ряд");

                Vector3 line1 = CellModel.Instance.allCells[(int)firstStep.x, (int)firstStep.y].transform.position;//Линия
                Vector3 line2 = CellModel.Instance.allCells[(int)backStep.x, (int)backStep.y].transform.position;
                Vector3[] tor = new Vector3[2] { line1, line2 };
                var linia = LineRenderer.Instantiate(CellModel.Instance.simpleLine).GetComponent<LineRenderer>();
                linia.SetPositions(tor);

                StopGame();
                CellModel.Instance.dial.SetActive(true);
                CountWined(checkTurnCell.Status);
                CellModel.Instance.countingOfMoves = 0;
                CellModel.Instance.dontWin = false;

                return;
            }
        }
    }

    public void StopGame()
    {
        foreach (var item in CellModel.Instance.allCells)
        {
            item.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (checkTurnCell.Status == 2)
        {
            SetTurnText(3);
        }
        else
        {
            SetTurnText(4);
        }
    }
    public void CountWined(int crosszero)
    {
        if (crosszero == 1)
        {
            CellModel.Instance.crosswWinnes += 1;
            WriteToCount(crosszero);
        }
        else if (crosszero == 2)
        {
            CellModel.Instance.zeroWinnes += 1;
            WriteToCount(crosszero);
        }
        else
        {
            CellModel.Instance.crosswWinnes = 0;
            CellModel.Instance.zeroWinnes = 0;
            WriteToCount(crosszero);
        }
    }
    public void WriteToCount(int crosszero)
    {
        if (crosszero == 1)
            CellModel.Instance.textCross.text = $"Cross {CellModel.Instance.crosswWinnes}";
        else if (crosszero == 2)
            CellModel.Instance.textZero.text = $"Zero {CellModel.Instance.zeroWinnes}";
        else
        {
            CellModel.Instance.textCross.text = $"Cross {CellModel.Instance.crosswWinnes}";
            CellModel.Instance.textZero.text = $"Zero {CellModel.Instance.zeroWinnes}";
        }
    }
    public void NewRound()
    {

        foreach (var item in CellModel.Instance.allCells)
        {
            Destroy(item);

        }
        CreateAllCells(row, column);
        SetTurnText(CellModel.Instance.numberNextPlayer);
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Linetag").Length; i++)
        {
            Destroy(GameObject.FindGameObjectWithTag("Linetag"));
        }
        CellModel.Instance.dontWin = true;

        //CountWined(CellModel.Instance.targetCell.GetComponent<Cell>().Status);
    }
    public void RestartGame()
    {
        NewRound();
        CountWined(0);
        CellModel.Instance.numberNextPlayer = 1;
        SetTurnText(CellModel.Instance.numberNextPlayer);
        CellModel.Instance.countingOfMoves = 0;
        CellModel.Instance.dial.SetActive(false);
    }
    public void CountingOfMoves()
    {

        if (CellModel.Instance.numOfMoves == CellModel.Instance.countingOfMoves)
        {
            CellModel.Instance.dontWin = true;
            CellModel.Instance.countingOfMoves = 0;
            StopGame();

            if (CellModel.Instance.dontWin == true)
            {
                CellModel.Instance.textTurns.text = "No winner";
                CellModel.Instance.dial.SetActive(true);

            }
            else CellModel.Instance.dial.SetActive(true);

        }
    }
}

