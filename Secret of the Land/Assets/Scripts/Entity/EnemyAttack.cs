using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("Enemy is hit");
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player is hit");
                battleEngine.DealDamage(ref fsm.properties, 
                                        ref player.properties);
                
            }
        }
}
