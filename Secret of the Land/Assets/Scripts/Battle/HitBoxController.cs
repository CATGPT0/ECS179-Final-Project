using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class HitBoxController : MonoBehaviour
    {
        [SerializeField] private string otherTag;
        private BattleEngine battleEngine;
        
        private Player player;

        void Awake()
        {
            player = FindFirstObjectByType<Player>();
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
                battleEngine.DealDamage(player, 
                                        other.gameObject.GetComponentInChildren<Entity>(),
                                        AttackType.Physical);
            }
        }
    }
}