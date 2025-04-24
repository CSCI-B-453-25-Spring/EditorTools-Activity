#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class TreePlacerWindow : EditorWindow
{
    GameObject treePrefab;
    int numberOfTrees;
    Vector2 areaSize = new Vector2(1000, 1000);

    [MenuItem("Our Custom Tool Thing/Tree Placer")]
    public static void ShowWindow()
    {
        GetWindow<TreePlacerWindow>("Tree Placer");
    }

    private void OnGUI()
    {
        treePrefab = (GameObject)EditorGUILayout.ObjectField("Tree Prefab", treePrefab, typeof(GameObject), false);
        numberOfTrees = EditorGUILayout.IntSlider("Number of Trees", numberOfTrees, 0, 1000000);
        areaSize = EditorGUILayout.Vector2Field("Area Size", areaSize);

        if (GUILayout.Button("Place Trees"))
        {
            PlaceTrees();
        }
    }

    void PlaceTrees()
    {
        if (treePrefab != null)
        {
            for (int i = 0; i < numberOfTrees; i++)
            {
                Vector3 randomPos = new Vector3(Random.Range(-areaSize.x / 2f, areaSize.x / 2f), 0, Random.Range(-areaSize.y / 2f, areaSize.y / 2f));

                //GameObject newTree = Instantiate(treePrefab, randomPos, Quaternion.identity);
                GameObject newTree = (GameObject)PrefabUtility.InstantiatePrefab(treePrefab);
                newTree.transform.position = randomPos;
            }
        }
    }
}
#endif