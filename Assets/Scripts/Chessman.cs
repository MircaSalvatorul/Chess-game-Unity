using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    //Reference
    public GameObject controller;
    public GameObject movePlate;

    //Position
    private int xBoard = -1;
    private int yBoard = -1;

    //Variable for player color
    public string player;

    //Reference for sprites
    public Sprite blackQueen, blackKnight, blackBishop, blackKing, blackRook, blackPawn;
    public Sprite whiteQueen, whiteKnight, whiteBishop, whiteKing, whiteRook, whitePawn;


    //Activate is called when the object is created
    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //take instance location and adjust transform
        SetCoords();

        switch (this.name)
        {
            case "blackQueen":
                this.GetComponent<SpriteRenderer>().sprite = blackQueen;
                player = "black";
                break;
            case "blackKnight":
                this.GetComponent<SpriteRenderer>().sprite = blackKnight;
                player = "black";
                break;
            case "blackBishop":
                this.GetComponent<SpriteRenderer>().sprite = blackBishop;
                player = "black";
                break;
            case "blackKing":
                this.GetComponent<SpriteRenderer>().sprite = blackKing;
                player = "black";
                break;
            case "blackRook":
                this.GetComponent<SpriteRenderer>().sprite = blackRook;
                player = "black";
                break;
            case "blackPawn":
                this.GetComponent<SpriteRenderer>().sprite = blackPawn;
                player = "black";
                break;
            case "whiteQueen":
                this.GetComponent<SpriteRenderer>().sprite = whiteQueen;
                player = "white";
                break;
            case "whiteKnight":
                this.GetComponent<SpriteRenderer>().sprite = whiteKnight;
                break;
            case "whiteBishop":
                this.GetComponent<SpriteRenderer>().sprite = whiteBishop;
                player = "white";
                break;
            case "whiteKing":
                this.GetComponent<SpriteRenderer>().sprite = whiteKing;
                player = "white";
                break;
            case "whiteRook":
                this.GetComponent<SpriteRenderer>().sprite = whiteRook;
                player = "white";
                break;
            case "whitePawn":
                this.GetComponent<SpriteRenderer>().sprite = whitePawn;
                player = "white";
                break;
        }
    }

    public void SetCoords()
    {
        // Dimensiunea unui pătrat al tablei
        float squareSize = 1.0f; 

        // Offset pentru centrul tablei
        float boardOffsetX = -3.5f; 
        float boardOffsetY = -3.5f;

        float x = xBoard * squareSize + boardOffsetX;
        float y = yBoard * squareSize + boardOffsetY;

        this.transform.position = new Vector3(x, y, -1.0f);
    }


    public int GetXboard()
    {
        return xBoard;
    }

    public int GetYboard() {
        return yBoard;
    }

    public void SetXboard(int x)
    {
        xBoard = x;
    }

    public void SetYboard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        DestroyMovePlates();

        InitiatesMovePlates();
    } 

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiatesMovePlates()
    {
        switch (this.name)
        {
            case "blackQueen":
            case "whiteQueen":
                //one function for each direction
                //which means a total of 8
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "blackKnight":
            case "whiteKnight":
                LMovePlate();
                break;
            case "blackBishop":
            case "whiteBishop":
                LineMovePlate(1, 1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, -1);
                break;
            case "blackKing":
            case "whiteKing":
                SurroundMovePlate();
                break;
            case "blackRook":
            case "whiteRook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "blackPawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "whitePawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        GameScript sc = controller.GetComponent<GameScript>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x,y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }


        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>().player != player)
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
        //again, 8 directions... really gotta find a better
        //way than 8 calls. this just feels lazy
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    public void PointMovePlate(int x, int y)
    {
        GameScript sc = controller.GetComponent<GameScript>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x, int y)
    {
        GameScript sc = controller.GetComponent<GameScript>();

        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;
        
        x += -3.5f;
        y += -3.5f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x += -3.5f;
        y += -3.5f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}
