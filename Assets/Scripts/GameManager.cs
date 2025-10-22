using System.Collections.Generic;
using System.Linq;

public class GameManager 
{
    public int[] board;

    public const int None = 0;
    public const int King = 1;
    public const int Pawn = 2;
    public const int Knight = 3;
    public const int Bishop = 4;
    public const int Rook = 5;
    public const int Queen = 6;

    public const int White = 8;
    public const int Black = 16;

    public int turn = White;

    public GameManager()
    {
        board = new int[64];

        board[0] = White | Rook;        board[56] = Black | Rook;
        board[1] = White | Knight;      board[57] = Black | Knight;
        board[2] = White | Bishop;      board[58] = Black | Bishop;
        board[3] = White | Queen;       board[59] = Black | Queen;
        board[4] = White | King;        board[60] = Black | King;
        board[5] = White | Bishop;      board[61] = Black | Bishop;
        board[6] = White | Knight;      board[62] = Black | Knight;
        board[7] = White | Rook;        board[63] = Black | Rook;
        board[8] = White | Pawn;        board[55] = Black | Pawn;
        board[9] = White | Pawn;        board[55] = Black | Pawn;
        board[10] = White | Pawn;       board[55] = Black | Pawn;
        board[11] = White | Pawn;       board[55] = Black | Pawn;
        board[12] = White | Pawn;       board[55] = Black | Pawn;
        board[13] = White | Pawn;       board[55] = Black | Pawn;
        board[14] = White | Pawn;       board[55] = Black | Pawn;
        board[15] = White | Pawn;       board[55] = Black | Pawn;
    }

    
    public bool isLegalMove(int pieceLocation, int targetLocation)
    {
        List<int> legalMoves = giveLegalMoves(pieceLocation);
        return legalMoves.Any(move => move == targetLocation);
    }

    public List<int> giveLegalMoves(int pieceLocation)
    {
        if (board[pieceLocation] == 0) return null;

        List<int> legalMoves = new List<int>();
        int piece = board[pieceLocation];
        switch (piece & 7)
        {
            case 2:
                if ((piece & 24) == White)
                {
                    legalMoves.Add(pieceLocation + 8);
                }
                else
                {
                    legalMoves.Add(pieceLocation - 8);
                }
                break;
            case 4:
                addDiagonalsToList(legalMoves, pieceLocation);
                break;
            case 3:
                if (pieceLocation + 15 <= 63) legalMoves.Add(pieceLocation + 15);
                if (pieceLocation + 6 <= 63) legalMoves.Add(pieceLocation + 6);
                if (pieceLocation + 17 <= 63) legalMoves.Add(pieceLocation + 17);
                if (pieceLocation + 10 <= 63) legalMoves.Add(pieceLocation + 10);

                if (pieceLocation - 15 >= 0) legalMoves.Add(pieceLocation - 15);
                if (pieceLocation - 6 >= 0) legalMoves.Add(pieceLocation - 6);
                if (pieceLocation - 17 >= 0) legalMoves.Add(pieceLocation - 17);
                if (pieceLocation - 10 >= 0) legalMoves.Add(pieceLocation - 10);

                break;
            case 5:
                addCardinalsToList(legalMoves, pieceLocation);
                break;
            case 6:
                addCardinalsToList(legalMoves, pieceLocation);
                addDiagonalsToList(legalMoves, pieceLocation);
                break;
            case 1:
                if (pieceLocation + 7 <= 63) legalMoves.Add(pieceLocation + 7);
                if (pieceLocation + 8 <= 63) legalMoves.Add(pieceLocation + 8);
                if (pieceLocation + 9 <= 63) legalMoves.Add(pieceLocation + 9);
                if (pieceLocation + 1 <= 63) legalMoves.Add(pieceLocation + 1);
                if (pieceLocation - 7 >= 0) legalMoves.Add(pieceLocation - 7);
                if (pieceLocation - 8 >= 0) legalMoves.Add(pieceLocation - 8);
                if (pieceLocation - 9 >= 0) legalMoves.Add(pieceLocation - 9);
                if (pieceLocation - 1 >= 0) legalMoves.Add(pieceLocation - 1);
                break;
        }
        return legalMoves;
    }

    //public void showHighlights(Piece piece)
    //{
    //    visualManager.showHighlight(giveLegalMoves(piece), piece.team);
    //}

    //public void hideHighlights()
    //{
    //    visualManager.hideHighlight();
    //}

    private void addDiagonalsToList(List<int> legalMoves, int position)
    {
        int pointer = position + 7; //northwest
        while (pointer >= 0 && pointer <= 63)
        {
            legalMoves.Add(pointer);
            pointer += 7;
        }
        pointer = position + 9; //northeast
        while (pointer >= 0 && pointer <= 63)
        {
            legalMoves.Add(pointer);
            pointer += 9;
        }
        pointer = position - 7; //southeast
        while (pointer >= 0 && pointer <= 63)
        {
            legalMoves.Add(pointer);
            pointer -= 7;
        }
        pointer = position - 9; //southwest
        while (pointer >= 0 && pointer <= 63)
        {
            legalMoves.Add(pointer);
            pointer -= 9;
        }
    }

    private void addCardinalsToList(List<int> legalMoves, int position)
    {
        int pointer = position + 8;
        while (pointer <= 63) //north
        {
            legalMoves.Add(pointer);
            pointer += 8;
        }
        pointer = position + 1;
        while (pointer <= 63 && pointer % 8 != 0) //north
        {
            legalMoves.Add(pointer);
            pointer += 1;
        }
        pointer = position - 8;
        while (pointer >= 0) //north
        {
            legalMoves.Add(pointer);
            pointer -= 8;
        }
        pointer = position - 1;
        while (pointer >= 0 && pointer % 8 != 7) //north
        {
            legalMoves.Add(pointer);
            pointer -= 1;
        }
    }

    public int flipTurn()
    {
        turn = turn == White ? Black : White;
        return turn;
    }

    public int getTurn()
    {
        return turn;
    }
}
