using System;
using UnityEngine;


public class Piece : MonoBehaviour
{
    public enum Team
    {
        White,
        Black
    }

    public enum PieceType
    {
        King,
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen
    }

    public VisualManager visualManager;

    public PieceType pieceType;
    public bool showMoves = false;
    public Team team;

    private bool isMoving = false;
    private Vector3 offset;
    public Vector3 gamePosition;

    

    private void Start()
    {
        gamePosition = transform.position;
    }


    private void OnMouseUp()
    {
        isMoving = false;
        Vector3 dropOffPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dropOffPos.x = Mathf.Floor(dropOffPos.x);
        dropOffPos.y = Mathf.Floor(dropOffPos.y);
        dropOffPos.z = 0;

        //check if valid move with GameManager
        if(visualManager.isLegalMove(this, dropOffPos))
        {
            transform.position = dropOffPos;
            gamePosition = dropOffPos;
            visualManager.hideHighlight();
        }
        else
        {
            transform.position = gamePosition;
        }
    }

    private void OnMouseDown()
    {
        isMoving = true;
        gamePosition = transform.position;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseEnter()
    {
        showMoves = true;
        visualManager.highlightLegalMoves(this);
    }

    private void OnMouseExit()
    {
        visualManager.hideHighlight();
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; //keep in 2D
            transform.position = offset + mousePosition;
        }
    }
}
