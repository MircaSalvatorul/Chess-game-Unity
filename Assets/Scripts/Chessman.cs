﻿using System.Collections;
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
                break;
            case "blackKnight":
                this.GetComponent<SpriteRenderer>().sprite = blackKnight;
                break;
            case "blackBishop":
                this.GetComponent<SpriteRenderer>().sprite = blackBishop;
                break;
            case "blackKing":
                this.GetComponent<SpriteRenderer>().sprite = blackKing;
                break;
            case "blackRook":
                this.GetComponent<SpriteRenderer>().sprite = blackRook;
                break;
            case "blackPawn":
                this.GetComponent<SpriteRenderer>().sprite = blackPawn;
                break;
            case "whiteQueen":
                this.GetComponent<SpriteRenderer>().sprite = whiteQueen;
                break;
            case "whiteKnight":
                this.GetComponent<SpriteRenderer>().sprite = whiteKnight;
                break;
            case "whiteBishop":
                this.GetComponent<SpriteRenderer>().sprite = whiteBishop;
                break;
            case "whiteKing":
                this.GetComponent<SpriteRenderer>().sprite = whiteKing;
                break;
            case "whiteRook":
                this.GetComponent<SpriteRenderer>().sprite = whiteRook;
                break;
            case "whitePawn":
                this.GetComponent<SpriteRenderer>().sprite = whitePawn;
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
}
