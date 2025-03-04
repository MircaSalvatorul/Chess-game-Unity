using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCloner : MonoBehaviour
{
    public float boardOffsetX = 9.0f; // Distanța dintre table
    public Dictionary<string, GameObject> piecePrefabs; // Dicționar cu prefabs pentru piese
    private List<GameObject> clonedBoards = new List<GameObject>();

    void Start()
    {
        LoadPiecePrefabs();
    }

    private void LoadPiecePrefabs()
    {
        piecePrefabs = new Dictionary<string, GameObject>();

        // Încarcă prefab-urile din Resources (asigură-te că ai un folder "Resources" în Unity)
        piecePrefabs["whitePawn"] = Resources.Load<GameObject>("Prefabs/whitePawn");
        piecePrefabs["blackPawn"] = Resources.Load<GameObject>("Prefabs/blackPawn");
        piecePrefabs["whiteKnight"] = Resources.Load<GameObject>("Prefabs/whiteKnight");
        piecePrefabs["blackKnight"] = Resources.Load<GameObject>("Prefabs/blackKnight");
        piecePrefabs["whiteBishop"] = Resources.Load<GameObject>("Prefabs/whiteBishop");
        piecePrefabs["blackBishop"] = Resources.Load<GameObject>("Prefabs/blackBishop");
        piecePrefabs["whiteRook"] = Resources.Load<GameObject>("Prefabs/whiteRook");
        piecePrefabs["blackRook"] = Resources.Load<GameObject>("Prefabs/blackRook");
        piecePrefabs["whiteQueen"] = Resources.Load<GameObject>("Prefabs/whiteQueen");
        piecePrefabs["blackQueen"] = Resources.Load<GameObject>("Prefabs/blackQueen");
        piecePrefabs["whiteKing"] = Resources.Load<GameObject>("Prefabs/whiteKing");
        piecePrefabs["blackKing"] = Resources.Load<GameObject>("Prefabs/blackKing");
    }

    public void CloneBoard(GameObject[,] originalBoard)
    {
        if (piecePrefabs == null || piecePrefabs.Count == 0)
        {
            Debug.LogError("Prefab-urile pentru piese nu au fost încărcate!");
            return;
        }

        // Creează un nou obiect gol care va conține copia tablei
        GameObject clonedBoard = new GameObject("ClonedBoard");
        clonedBoard.transform.position = new Vector3(clonedBoards.Count * boardOffsetX, 0, 0);

        // Clonează toate piesele folosind Prefab-uri
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (originalBoard[x, y] != null)
                {
                    Chessman originalPiece = originalBoard[x, y].GetComponent<Chessman>();
                    if (originalPiece != null && piecePrefabs.ContainsKey(originalPiece.name))
                    {
                        // Instanțiem o piesă nouă din Prefab
                        GameObject pieceClone = Instantiate(
                            piecePrefabs[originalPiece.name],
                            new Vector3(originalPiece.transform.position.x + (clonedBoards.Count + 1) * boardOffsetX,
                                        originalPiece.transform.position.y,
                                        originalPiece.transform.position.z),
                            Quaternion.identity);

                        pieceClone.transform.parent = clonedBoard.transform; // Grupează piesele în obiectul tablei
                    }
                }
            }
        }

        // Adaugă noua tablă în listă pentru referință
        clonedBoards.Add(clonedBoard);
    }
    public void CloneBoardFromGame(GameScript gameScript)
    {
        if (gameScript == null)
        {
            Debug.LogError("GameScript este NULL în CloneBoardFromGame!");
            return;
        }

        Debug.Log("Se clonează tabla...");
        CloneBoard(gameScript.GetBoardState());
    }
}
