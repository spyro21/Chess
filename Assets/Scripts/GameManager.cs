using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public Piece[] pieces;
    public VisualManager visualManager;
    public enum Team
    {
        Black,
        White
    };

    public enum PieceType
    {
        Pawn,
        Bishop,
        Knight,
        Rook,
        Queen,
        King
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool isLegalMove(Piece piece, Vector3 position)
    {
        List<Vector3> legalMoves = giveLegalMoves(piece);
        return legalMoves.Any(move => move.x == position.x && move.y == position.y);
    }

    public List<Vector3> giveLegalMoves(Piece piece)
    {
        List<Vector3> legalMoves = new List<Vector3>();
        Vector3 position = piece.gamePosition;
        Vector3 pointer = position;
        switch (piece.pieceType)
        {
            case PieceType.Pawn:
                if (piece.team == Team.White)
                {
                    legalMoves.Add(new Vector3(position.x, position.y + 1, 0));
                }
                else
                {
                    legalMoves.Add(new Vector3(position.x, position.y - 1, 0));
                }
                break;
            case PieceType.Bishop:
                addDiagonalsToList(legalMoves, position);
                break;
            case PieceType.Knight:
                if (GameManager.isInBoard(position.x + 2, position.y + 1)) legalMoves.Add(new Vector3(position.x + 2, position.y + 1));
                if (GameManager.isInBoard(position.x + 1, position.y + 2)) legalMoves.Add(new Vector3(position.x + 1, position.y + 2));

                if (GameManager.isInBoard(position.x + 2, position.y - 1)) legalMoves.Add(new Vector3(position.x + 2, position.y - 1));
                if (GameManager.isInBoard(position.x + 1, position.y - 2)) legalMoves.Add(new Vector3(position.x + 1, position.y - 2));

                if (GameManager.isInBoard(position.x - 2, position.y - 1)) legalMoves.Add(new Vector3(position.x - 2, position.y - 1));
                if (GameManager.isInBoard(position.x - 1, position.y - 2)) legalMoves.Add(new Vector3(position.x - 1, position.y - 2));

                if (GameManager.isInBoard(position.x - 2, position.y + 1)) legalMoves.Add(new Vector3(position.x - 2, position.y + 1));
                if (GameManager.isInBoard(position.x - 1, position.y + 2)) legalMoves.Add(new Vector3(position.x - 1, position.y + 2));
                break;
            case PieceType.Rook:
                addCardinalsToList(legalMoves, position);
                break;
            case PieceType.Queen:
                addCardinalsToList(legalMoves, position);
                addDiagonalsToList(legalMoves, position);
                break;
            case PieceType.King:
                if (GameManager.isInBoard(position.x, position.y + 1)) legalMoves.Add(new Vector3(position.x, position.y + 1));
                if (GameManager.isInBoard(position.x, position.y - 1)) legalMoves.Add(new Vector3(position.x, position.y - 1));
                if (GameManager.isInBoard(position.x - 1, position.y)) legalMoves.Add(new Vector3(position.x - 1, position.y));
                if (GameManager.isInBoard(position.x + 1, position.y)) legalMoves.Add(new Vector3(position.x + 1, position.y));
                if (GameManager.isInBoard(position.x - 1, position.y + 1)) legalMoves.Add(new Vector3(position.x - 1, position.y + 1));
                if (GameManager.isInBoard(position.x + 1, position.y + 1)) legalMoves.Add(new Vector3(position.x + 1, position.y + 1));
                if (GameManager.isInBoard(position.x - 1, position.y - 1)) legalMoves.Add(new Vector3(position.x - 1, position.y - 1));
                if (GameManager.isInBoard(position.x + 1, position.y - 1)) legalMoves.Add(new Vector3(position.x + 1, position.y - 1));
                break;
        }
        return legalMoves;
    }

    public void showHighlights(Piece piece)
    {
        visualManager.showHighlight(giveLegalMoves(piece), piece.team);
    }

    public void hideHighlights()
    {
        visualManager.hideHighlight();
    }

    private void addDiagonalsToList(List<Vector3> list, Vector3 position)
    {
        Vector3 pointer = position;
        while (GameManager.isInBoard(pointer.x + 1, pointer.y + 1)) //northeast
        {
            pointer.x++; pointer.y++;
            list.Add(pointer);
        }
        pointer = position;
        while (GameManager.isInBoard(pointer.x + 1, pointer.y - 1)) //southeast
        {
            pointer.x++; pointer.y--;
            list.Add(pointer);
        }
        pointer = position;
        while (GameManager.isInBoard(pointer.x - 1, pointer.y - 1)) //southwest
        {
            pointer.x--; pointer.y--;
            list.Add(pointer);
        }
        pointer = position;
        while (GameManager.isInBoard(pointer.x - 1, pointer.y + 1)) //northwest
        {
            pointer.x--; pointer.y++;
            list.Add(pointer);
        }
    }

    private void addCardinalsToList(List<Vector3> list, Vector3 position)
    {
        Vector3 pointer = position;
        while (GameManager.isInBoard(pointer.x, pointer.y + 1)) //north
        {
            pointer.y++;
            list.Add(pointer);
        }
        pointer = position;
        while (GameManager.isInBoard(pointer.x + 1, pointer.y)) //east
        {
            pointer.x++;
            list.Add(pointer);
        }
        pointer = position;
        while (GameManager.isInBoard(pointer.x, pointer.y - 1)) //south
        {
            pointer.y--;
            list.Add(pointer);
        }
        pointer = position;
        while (GameManager.isInBoard(pointer.x - 1, pointer.y)) //west
        {
            pointer.x--;
            list.Add(pointer);
        }
    }

    private static bool isInBoard(float x, float y)
    {
        return x >= 0 && x < 8 && y >= 0 && y < 8;
    }

    public Piece returnPresentPiece(Vector3 pos, Team team) {
        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i].transform.position == pos && pieces[i].team != team)
            {
                return pieces[i];
            }
        }

        return null;
    }
}
