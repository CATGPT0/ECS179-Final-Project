using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class SlimeAttack : MonoBehaviour
{
    private BattleEngine battleEngine;
    [SerializeField]
    private Player player;
    [SerializeField]
    private SlimeFSM fsm;
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
                Debug.Log("SlimeAttack: OnTriggerEnter2D");
                battleEngine.DealDamage(ref fsm.properties, 
                                        ref player.properties);
                
            }
        }
}
