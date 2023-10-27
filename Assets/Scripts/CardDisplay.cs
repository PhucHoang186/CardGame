using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [field: SerializeField] public Image CardIcon { get; set; }
        [field: SerializeField] public TMP_Text CardName { get; set; }
        [field: SerializeField] public TMP_Text CardDescription { get; set; }
        [SerializeField] GameObject display;

        [SerializeField] float checkThreshold = 0.001f;
        // mouse hover params
        [Space]
        [SerializeField] float onMouseEnterScale = 1.2f;
        [SerializeField] float scaleHoverSpeed = 10f;
        // mouse drag params
        [Space]
        [SerializeField] float scaleWhenDragSpeed = 10f;
        private HorizontalLayoutGroup layoutGroup;
        private Canvas canvas;

        private float desScale = 1f;
        private float scaleSpeed;
        private float currentScale = 1f;
        private bool dragging;
        // callbacks
        private Action<CardDisplay> onMouseEnterCb;
        private Action<CardDisplay> onMouseExitCb;
        private Action<CardDisplay> onMouseClickCb;
        private Action<CardDisplay> onMouseHoldCb;
        private Action<CardDisplay> onMouseReleaseCb;

        private void Start()
        {
            canvas = GetComponent<Canvas>();
            layoutGroup = GetComponentInParent<HorizontalLayoutGroup>();
        }

        public void InitAction(Action<CardDisplay> onMouseEnter, Action<CardDisplay> onMouseExit, Action<CardDisplay> onMouseClick
        , Action<CardDisplay> onMouseHold, Action<CardDisplay> onMouseRelease)
        {
            onMouseEnterCb = onMouseEnter;
            onMouseExitCb = onMouseExit;
            onMouseClickCb = onMouseClick;
            onMouseHoldCb = onMouseHold;
            onMouseReleaseCb = onMouseRelease;
        }

        public void UpdateCardInfo(Sprite cardIcon, string cardName, string cardDescription)
        {
            this.CardIcon.sprite = cardIcon;
            this.CardName.text = cardName;
            // this.cardDescription.text = cardDescription;
        }

        public void OnSelectCard()
        {
            UpdateDrag(true);
            StartScaleCard(0f, scaleWhenDragSpeed);
            ResetHorizonalLayout();
        }


        public void CancelCard()
        {
            UpdateDrag(false);
            StartScaleCard(1f, scaleWhenDragSpeed * 3);
            ResetHorizonalLayout();
        }

        private void UpdateDrag(bool isActive)
        {
            dragging = isActive;
            display.SetActive(!isActive);
        }

        private void ResetHorizonalLayout()
        {
            StartCoroutine(CorResetHorizonalLayout());
        }

        private IEnumerator CorResetHorizonalLayout()
        {
            layoutGroup.enabled = false;
            // yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(0.1f);
            layoutGroup.enabled = true;
        }

        private void StartScaleCard(float desScale, float scaleSpeed)
        {
            this.desScale = desScale;
            this.scaleSpeed = scaleSpeed;
        }

        public void PLayCard()
        {
            Destroy(this.gameObject);
        }

        void Update()
        {
            ScaleCard();
        }

        private void ScaleCard()
        {
            currentScale = Mathf.Lerp(currentScale, desScale, Time.deltaTime * scaleSpeed);
            if (Mathf.Abs(currentScale - desScale) <= checkThreshold)
            {
                currentScale = desScale;
                transform.localScale = Vector3.one * currentScale;
                return;
            }
            transform.localScale = Vector3.one * currentScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            StartScaleCard(onMouseEnterScale, scaleHoverSpeed);
            canvas.sortingOrder = 2;
            onMouseEnterCb?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            canvas.sortingOrder = 1;
            if (!dragging)
                StartScaleCard(1f, scaleHoverSpeed);
            onMouseExitCb?.Invoke(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (dragging)
                return;
            onMouseClickCb?.Invoke(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            onMouseHoldCb?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onMouseReleaseCb?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
        }
    }
}
