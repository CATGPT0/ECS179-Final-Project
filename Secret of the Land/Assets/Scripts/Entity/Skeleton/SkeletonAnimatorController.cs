using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class SkeletonAnimatorController : MonoBehaviour
{
    private Animator anim;
    private SkeletonController skeletonController;
    void Awake()
    {
        anim = GetComponentInParent<Animator>();
        skeletonController = GetComponentInParent<SkeletonController>();
    }

    void Start()
    {
        
    }
    void Update()
    {
        anim.SetInteger("health", skeletonController.Skeleton.Health);
    }

    void OnDestory()
    {
        
    }
}
