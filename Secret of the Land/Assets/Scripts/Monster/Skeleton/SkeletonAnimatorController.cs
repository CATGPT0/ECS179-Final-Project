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
        anim = GetComponent<Animator>();
        skeletonController = GetComponent<SkeletonController>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("health", skeletonController.Health);
    }

    void OnDestory()
    {
        
    }
}
