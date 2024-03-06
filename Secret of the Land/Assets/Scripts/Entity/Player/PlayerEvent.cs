using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerEvent : EntityEvent
{
    [SerializeField]
    protected GameEvent levelUpEvent;
    public GameEvent LevelUpEvent
    {
        get { return levelUpEvent; }
        set { levelUpEvent = value; }
    }
    [SerializeField]
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnDestroy()
    {
        base.OnDestroy();
    }
}
