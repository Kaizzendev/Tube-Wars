using UnityEngine;
using UnityEditor;

namespace Node
{
    public class NodeEditorTool
    {
        [MenuItem("Tools/Connect Selected Nodes %#q")]
        public static void ConnectSelectedNodes()
        {
            var selected = Selection.gameObjects;

            if (selected.Length < 2)
            {
                Debug.LogWarning("Please select at least 2 nodes");
                return;
            }

            Node[] nodes = new Node[selected.Length];

            for (int i = 0; i < selected.Length; i++)
            {
                nodes[i] = selected[i].GetComponent<Node>();

                if (nodes[i] == null)
                {
                    Debug.LogWarning("Every Object must have a Node");
                    return;
                }
            }

            for (int i = 0; i < nodes.Length; i++)
            {
                for (int j = i + 1; j < nodes.Length; j++)
                {
                    Node a = nodes[i];
                    Node b = nodes[j];

                    if (!a.neighbours.Contains(b))
                    {
                        a.neighbours.Add(b);
                    }
                    else
                    {
                        a.neighbours.Remove(b);
                    }

                    if (!b.neighbours.Contains(a))
                    {
                        b.neighbours.Add(a);
                    }
                    else
                    {
                        b.neighbours.Remove(a);
                    }
                }
            }
        }
    }
}