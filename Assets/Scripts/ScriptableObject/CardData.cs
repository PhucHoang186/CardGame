using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu()]
    public class CardData : ScriptableObject
    {
        [field: SerializeField]
        public Sprite CardIcon { get; private set; }

        [field: SerializeField]
        public string CardName { get; private set; }

        [field: SerializeField]
        public string CardDescription { get; private set; }
    }
}
