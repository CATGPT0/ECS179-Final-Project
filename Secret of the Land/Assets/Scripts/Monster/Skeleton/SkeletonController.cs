using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class SkeletonController : EntityController
{
    // Start is called before the first frame update
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
        CheckDeath();
    }
}
}
