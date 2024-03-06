using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class SkeletonAnimatorController : MonoBehaviour
{
    private Animator anim;
    private Skeleton skeleton;
    void Awake()
    {
        anim = GetComponentInParent<Animator>();
        skeleton = FindFirstObjectByType<Skeleton>();
    }

    void Start()
    {
        
    }
    void Update()
    {
        anim.SetInteger("health", skeleton.Health);
    }

    void OnDestory()
    {
        
    }
}
