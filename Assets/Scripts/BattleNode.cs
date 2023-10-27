using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class BattleNode : MonoBehaviour
    {

        [SerializeField] GameObject mouseEnterIcon;
        private int xCordinate;
        private int yCordinate;
        public Vector2 Cordinate => new(xCordinate, yCordinate);
        public bool IsEmpty { get; set; }

        public void SetNodeCordinate(int x, int y)
        {
            xCordinate = x;
            yCordinate = y;
        }

        private void OnMouseEnter()
        {
            if (!IsEmpty)
                return;
            mouseEnterIcon.SetActive(true);
        }

        private void OnMouseExit()
        {
            mouseEnterIcon.SetActive(false);
        }
    }
}