using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;
using System.IO.Enumeration;

namespace CardBattle {
    [CreateAssetMenu(fileName = "New Card")]
    public class CardScriptableObject : ScriptableObject
    {
        public int energyCost;
        public int cardCode;
        public string cardName;
    }
}
