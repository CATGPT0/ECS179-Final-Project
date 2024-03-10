using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class SkeletonEvent : MonoBehaviour
{
    public Action<int> onMonsterDeath = new Action<int>(a => { });
    private PlayerController playerController;
    private SkeletonFSM fsm;
    void Awake()
    {
        
        playerController = FindFirstObjectByType<PlayerController>();
        fsm = GetComponentInParent<SkeletonFSM>();
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        Debug.Log("XP: " + Table.CalculateXP(fsm.properties.Level, playerController.Player.properties.Level, fsm.properties.ThisType));
        onMonsterDeath?.Invoke(Table.CalculateXP(fsm.properties.Level, playerController.Player.properties.Level, fsm.properties.ThisType));
    }
}
