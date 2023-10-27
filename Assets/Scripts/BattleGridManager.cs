using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Grid
{

    public class BattleGridManager : MonoBehaviour
    {
        [SerializeField] Vector2 startPosition;
        [SerializeField] int width;
        [SerializeField] int height;
        [SerializeField] BattleNode nodePrefab;


        // temporary
        [SerializeField] EntityCard playerCard;
        [SerializeField] EntityCard enemyCard;


        private List<BattleNode> nodes = new();

        private void Start()
        {
            InitGrid();
            InitEntitesCard();
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
                    nodes.Add(newNode);
                }
            }
        }

        // temporary
        private void InitEntitesCard()
        {
            var player =  Instantiate(playerCard, transform);
            player.PlaceToNode(nodes[0]);
            var enemy =  Instantiate(enemyCard, transform);
            enemy.PlaceToNode(nodes[^1]);
        }
    }
}
