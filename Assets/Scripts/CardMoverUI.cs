using System;
using System.Collections;
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
    [SerializeField] GameObject playCardVfx;
    [SerializeField] float playCardDuration = 0.5f;
    private bool moveable = true;

    public void OnStartMove(Sprite icon, string cardName, string cardDescription)
    {
        if(!moveable)
            return;
        ToggleCard(true);
        cardIcon.sprite = icon;
        this.cardName.text = cardName;
        // this.cardDescription.text = cardDescription;
    }

    public void OnStopMove()
    {
        ToggleCard(false);
    }

    private void ToggleCard(bool isActive)
    {
        display.SetActive(isActive);
    }

    public void PlayCardAnimation(Action onFinishAnimation = null)
    {
        transform.DOMove(new Vector2(Screen.width / 2f, Screen.height / 2f), playCardDuration).SetEase(Ease.OutFlash).OnComplete(
            () =>
            {
                PlayCardVfx();
                OnStopMove();
                onFinishAnimation?.Invoke();
            });
    }

    public void PlayCardVfx()
    {
        StartCoroutine(CorPlayCardVfx());
    }

    private IEnumerator CorPlayCardVfx()
    {
        if (playCardVfx == null)
            yield break;
        playCardVfx.SetActive(true);
        yield return new WaitForSeconds(2f);
        playCardVfx.SetActive(false);
    }

    private void Update()
    {
        if (!display.activeSelf || !moveable)
            return;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out Vector2 position);
        transform.position = canvas.transform.TransformPoint(position);
    }
}
