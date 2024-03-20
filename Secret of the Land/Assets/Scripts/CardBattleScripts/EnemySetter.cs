using CardBattle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetter : MonoBehaviour
{
    public GameObject cardBattle;
    public GameObject enemyGameObject;
    private Enemy enemy;
    private NPCBehavior nPCBehavior;
    void Awake()
    {
        cardBattle = GameObject.Find("CardBattle");
        nPCBehavior = cardBattle.GetComponent<NPCCardEntryController>().npcBehavior;
        enemy = enemyGameObject.GetComponent<Enemy>();
        enemy.HP = nPCBehavior.Health;
        enemy.maxAttackDamage = nPCBehavior.MaxDamage;
        enemy.minAttackDamage = nPCBehavior.MinDamage;
        enemy.maxShield = nPCBehavior.MaxDefense;
        enemy.minShield = nPCBehavior.MinDefense;
        enemy.enemyActionsPattern = nPCBehavior.ActionsLoop;
        enemy.GetComponent<SpriteRenderer>().sprite = nPCBehavior.sprite;
    }

    public void GameStateSetter(bool state)
    {
        nPCBehavior.isWin = state;
    }

}
