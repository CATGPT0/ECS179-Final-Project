using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.Events;

public class Skeleton : Entity
{
    protected override void Init()
    {
        base.Init();
        entityType = EntityType.Type.Skeleton;
    }
    void Awake()
    {
        Init();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
