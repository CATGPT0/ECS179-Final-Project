using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class HitBoxController : MonoBehaviour
    {
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
            if (other.gameObject.CompareTag("Skeleton"))
            {
                SkeletonFSM fsm = other.gameObject.GetComponentInChildren<SkeletonFSM>();
                //Debug.Log("Health Left: " + fsm.properties.Health);
                battleEngine.DealDamage(ref player.properties, 
                                        ref fsm.properties);
                
            }
            else if (other.gameObject.CompareTag("Slime"))
            {
                SlimeFSM fsm = other.gameObject.GetComponentInChildren<SlimeFSM>();
                Debug.Log("Health Left: " + fsm.properties.Health);
                battleEngine.DealDamage(ref player.properties, 
                                        ref fsm.properties);
                
            }
        }
    }
}