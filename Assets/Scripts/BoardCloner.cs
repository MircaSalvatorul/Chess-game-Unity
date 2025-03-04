using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCloner : MonoBehaviour
{
    public float boardOffsetX = 9.0f; // Distanta dintre table
    public GameObject boardPrefab; // Prefab pentru tabla
    private List<GameObject> clonedBoards = new List<GameObject>();

    public void CloneBoard(GameObject[,] originalBoard)
    {
        // Creează o copie a tablei (dacă avem un prefab setat)
        GameObject clonedBoard = null;
        if (boardPrefab != null)
        {
            clonedBoard = Instantiate(boardPrefab, new Vector3((clonedBoards.Count + 1) * boardOffsetX, 0, 0), Quaternion.identity);
            clonedBoard.name = "ClonedBoard_" + clonedBoards.Count;
        }
        else
        {
            Debug.LogError("Board Prefab is missing! Asigură-te că ai asignat un prefab în Inspector.");
            return;
        }

        // Clonează toate piesele
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (originalBoard[x, y] != null)
                {
                    GameObject pieceClone = Instantiate(
                        originalBoard[x, y],
                        new Vector3(originalBoard[x, y].transform.position.x + ((clonedBoards.Count + 1) * boardOffsetX),
                                    originalBoard[x, y].transform.position.y,
                                    originalBoard[x, y].transform.position.z),
                        Quaternion.identity);

                    pieceClone.transform.parent = clonedBoard.transform; // Grupează piesele în obiectul tablei
                }
            }
        }

        // Adaugă noua tablă în listă pentru referință
        clonedBoards.Add(clonedBoard);
    }

    public void CloneBoardFromGame(GameScript gameScript)
    {
        CloneBoard(gameScript.GetBoardState());
    }
}
