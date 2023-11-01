using System;
using System.Collections;
using Coffee.UIExtensions;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardMoverUI : MonoBehaviour
{
    [SerializeField] Image cardIcon;
    [SerializeField] TMP_Text cardName;
    [SerializeField] TMP_Text cardDescription;
    [SerializeField] GameObject display;
    [SerializeField] Canvas canvas;

    [Space]
    [SerializeField] UIParticle playCardVfx;
    [SerializeField] float playCardDuration = 0.5f;
    private bool moveable = true;

    public void OnStartMove(Sprite icon, string cardName, string cardDescription)
    {
        if (!moveable)
            return;
        cardIcon.sprite = icon;
        this.cardName.text = cardName;
        // this.cardDescription.text = cardDescription;
        ToggleCard(true);
    }

    public void OnStopMove()
    {
        ToggleCard(false);
    }

    private void ToggleCard(bool isActive)
    {
        display.SetActive(isActive);
        ResetVfx();
    }

    public void PlayCardAnimation(Action onFinishAnimation = null)
    {
        Sequence s = DOTween.Sequence();
        Tween moveToCenter = transform.DOMove(new Vector2(Screen.width / 2f, Screen.height / 2f), playCardDuration).SetEase(Ease.OutFlash);
        Tween scaleBigger = display.transform.DOScale(display.transform.localScale * 1.2f, playCardDuration).SetEase(Ease.OutFlash);
        s.Join(moveToCenter).Join(scaleBigger);
        s.Play().OnComplete(
            () =>
            {
                OnStopMove();
                PlayCardVfx();
                onFinishAnimation?.Invoke();
            });
    }

    public void PlayCardVfx()
    {
        if (playCardVfx == null)
            return;
        playCardVfx.gameObject.SetActive(true);
        playCardVfx.Stop();
        playCardVfx.Play();
    }

    private void ResetVfx()
    {
        display.transform.localScale = Vector3.one;
        playCardVfx.gameObject.SetActive(false);
        playCardVfx.Stop();
    }

    private void Update()
    {
        if (!display.activeSelf || !moveable)
            return;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out Vector2 position);
        transform.position = canvas.transform.TransformPoint(position);
    }
}
