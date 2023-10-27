using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Entity
{
    public class EntityCard : MonoBehaviour
    {
        private BattleNode currentPlacedNode;

        public void PlaceToNode(BattleNode newNodeToPlace)
        {
            if (currentPlacedNode != null)
                currentPlacedNode.IsEmpty = true;
            currentPlacedNode = newNodeToPlace;
            newNodeToPlace.IsEmpty = false;
            transform.position = newNodeToPlace.transform.position;
        }
    }
}