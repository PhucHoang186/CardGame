using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{

    public enum CardActionType
    {
        Play,
        Cancel,
    }

    public class DetectCardActionArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
    {
        [SerializeField] CardActionType cardActionType;
        [SerializeField] GameObject highlightImage;
        private bool dropIn;
        public bool IsPlayCard => cardActionType == CardActionType.Play && dropIn;



        public void OnPointerEnter(PointerEventData eventData)
        {
            ToggleHighlight(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ToggleHighlight(false);
            dropIn = false;
        }

        public void OnDrop(PointerEventData eventData)
        {
            dropIn = true;
        }

        public void ToggleHighlight(bool isActive)
        {
            highlightImage.SetActive(isActive);
        }


    }
}
