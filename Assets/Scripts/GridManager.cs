using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Grid
{

    public class GridManager : MonoBehaviour
    {
        [SerializeField] Vector2 startPosition;
        [SerializeField] int width;
        [SerializeField] int height;
        [SerializeField] BattleNode nodePrefab;

        [field: SerializeField]
        public Dictionary<Vector2Int, BattleNode> Nodes { get; set; } = new();

        public void Init()
        {
            InitGrid();
        }

        private void InitGrid()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var newNode = Instantiate(nodePrefab, transform);
                    newNode.transform.position = startPosition + new Vector2(i, j);
                    newNode.SetNodeCordinate(i, j);
                    newNode.IsEmpty = true;
                    Nodes.Add(new Vector2Int(i, j), newNode);
                    Debug.Log(new Vector2Int(i, j) + " 1");
                }
            }
        }

        public List<BattleNode> GetCardinalNeighborNodes(BattleNode checkNode, int range)
        {
            List<BattleNode> neighbors = new() { checkNode };
            List<BattleNode> checkList = new() { checkNode };
            int step = 0;
            while (step < range)
            {
                List<BattleNode> newNeighbors = new();
                foreach (BattleNode node in checkList)
                {

                    newNeighbors.AddRange(GetNeighborNodes(node));
                }
                if (newNeighbors != null)
                {
                    neighbors.AddRange(newNeighbors);
                    checkList = newNeighbors;
                }
                step++;
            }
            Debug.Log(neighbors.Count);
            foreach (BattleNode node in neighbors)
            {
                node.test();
            }
            return neighbors;
        }

        private List<BattleNode> GetNeighborNodes(BattleNode checkNode)
        {
            List<BattleNode> neighbors = new();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    // if (Mathf.Abs(x) != Mathf.Abs(y) && Nodes.ContainsValue(checkNode))
                    if (Nodes.ContainsValue(checkNode))
                    {
                        var nodeCordinate = checkNode.Cordinate;
                        BattleNode neighbor = Nodes[new Vector2Int(nodeCordinate.x + x, nodeCordinate.y + y)];
                        neighbors.Add(neighbor);
                    }
                }
            }
            return neighbors;
        }
    }
}
