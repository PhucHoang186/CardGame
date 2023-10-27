using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CardDescriptionUI : MonoBehaviour
    {
        [SerializeField] Image cardIcon;
        [SerializeField] TMP_Text cardName;
        [SerializeField] TMP_Text cardDescription;

        public void UpdateCardInfo(Sprite cardIcon, string cardName, string cardDescription)
        {
            this.cardIcon.sprite = cardIcon;
            this.cardName.text = cardName;
            this.cardDescription.text = cardDescription;
        }
    }
}