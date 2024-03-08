using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class HitBoxController : MonoBehaviour
    {
        [SerializeField] private string otherTag;
        private BattleEngine battleEngine;
        
        private Entity player;

        void Awake()
        {
            player = FindFirstObjectByType<Entity>();
            battleEngine = FindFirstObjectByType<BattleEngine>();
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        
        void OnEnable()
        {
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(otherTag))
            {
                FSM fsm = other.gameObject.GetComponentInChildren<FSM>();
                battleEngine.DealDamage(ref player.properties, 
                                        ref fsm.properties,
                                        AttackType.Physical);
            }
        }
    }
}