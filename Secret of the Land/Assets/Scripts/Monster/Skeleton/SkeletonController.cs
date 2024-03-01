using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using Engine;

namespace Controller
{
    public class SkeletonController : EntityController
    {
        void Awake()
        {
            Init();
            eventManager.deathEvent[this.gameObject].Add(SceneEngine.LoadGameOverScene);
        }
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void Test()
        {
            Debug.Log("Skeleton is dead");
        }
    }
}
