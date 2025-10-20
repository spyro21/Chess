using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class VisualManager : MonoBehaviour
{
    public GameObject highlightPrefab;
    public List<GameObject> highlights;

    public void showHighlight(List<Vector3> positions, GameManager.Team team)
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

    public void hideHighlight()
    {
        for (int i = 0; i < highlights.Count; i++) {
            GameObject objectToDestroy = highlights[i];
            Destroy(objectToDestroy);
        }
        highlights.Clear();
    }
}
