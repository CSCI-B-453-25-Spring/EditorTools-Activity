using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class TreePlacer : MonoBehaviour
{
    public GameObject treePrefab;
    public int numberOfTrees = 5000;
    public Vector2 areaSize = new Vector2(100, 100);

    [ContextMenu("Place my trees")]
    public void PlaceTrees()
    {
        if (treePrefab != null)
        {
            for (int i = 0; i < numberOfTrees; i++)
            {
                Vector3 randomPos = new Vector3(Random.Range(-areaSize.x / 2f, areaSize.x / 2f), 0, Random.Range(-areaSize.y / 2f, areaSize.y / 2f));

                //GameObject newTree = Instantiate(treePrefab, randomPos, Quaternion.identity);
                GameObject newTree = (GameObject)PrefabUtility.InstantiatePrefab(treePrefab);
                newTree.transform.position = randomPos;
                newTree.transform.parent = this.transform;
            }
        }
    }
}
