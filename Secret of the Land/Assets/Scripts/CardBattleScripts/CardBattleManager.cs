using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;
using System;

namespace CardBattle {

    public class CardBattleManager : MonoBehaviour
    {
        public Action EffectDelegate;
        public void ProcessCardEffect()
        {
            if (EffectDelegate == null)
            {
                throw new Exception("Delegate EffectDelegate in CardBattleManger is null");
            }
            EffectDelegate();
        }
    }
}
