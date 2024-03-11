using System.Collections;
using System.Collections.Generic;
using Controller;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private BattleEngine battleEngine;
    [SerializeField]
    private Player player;
    [SerializeField]
    private SkeletonFSM fsm;
    void Awake()
    {
        battleEngine = FindFirstObjectByType<BattleEngine>();
        player = FindFirstObjectByType<PlayerController>().Player;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                battleEngine.DealDamage(ref fsm.properties, 
                                        ref player.properties);
                
            }
        }
}
