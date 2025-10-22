using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class VisualManager : MonoBehaviour
{
    public GameObject highlightPrefab;
    public GameManager gameManager = new GameManager();
    public List<GameObject> highlights;

    public void highlightLegalMoves(Piece piece)
    {
        //get legal moves
        List<int> coords = gameManager.giveLegalMoves(vecToIntCoord(piece.transform.position));
        List<Vector3> positions = new List<Vector3>();
        for(int i = 0; i < coords.Count;i++)
        {
            positions.Add(intCoordToVec(coords[i]));
        }

        //call showHighlights
        showHighlight(positions);
    }

    private void showHighlight(List<Vector3> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            GameObject instantiatedObject = Instantiate(highlightPrefab);
            highlights.Add(instantiatedObject);

            instantiatedObject.transform.position = new Vector3(positions[i].x, positions[i].y);

            if (positions[i].z == 1)
            {
                instantiatedObject.GetComponent<SpriteRenderer>().color = Color.softRed;
            }
        }
    }

    public bool isLegalMove(Piece piece, Vector3 targetLocation)
    {
        return gameManager.isLegalMove(vecToIntCoord(piece.transform.position), vecToIntCoord(targetLocation));
    }

    public void hideHighlight()
    {
        for (int i = 0; i < highlights.Count; i++)
        {
            GameObject objectToDestroy = highlights[i];
            Destroy(objectToDestroy);
        }
        highlights.Clear();
    }

    private int vecToIntCoord(Vector3 vec)
    {
        return (int)vec.y * 8 + (int)vec.x;
    }

    private Vector3 intCoordToVec(int coord)
    {
        return new Vector3(coord % 8, Mathf.Floor(coord / 8));
    }
}
