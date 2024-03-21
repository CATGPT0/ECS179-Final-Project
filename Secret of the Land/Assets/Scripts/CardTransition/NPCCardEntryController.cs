using System.Collections;
using System.Collections.Generic;
using Manager;
using Schema.Builtin.Nodes;
// using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;

namespace CardBattle
{
    [System.Serializable]
    public class NPCBehavior
    {
        public int Id;
        public int Health;
        public int MaxDamage;
        public int MinDamage;
        public int MaxDefense;
        public int MinDefense;
        public List<int> ActionsLoop;
        public Sprite sprite;
        public bool isWin;
        public GameObject UI;
        public GameObject GamePlay;
    }   
    public class NPCCardEntryController : MonoBehaviour
    {
        public NPCBehavior npcBehavior;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
