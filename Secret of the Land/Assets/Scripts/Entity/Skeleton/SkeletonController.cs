using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    private Skeleton skeleton;
    public Skeleton Skeleton
    {
        get { return skeleton; }
        set { skeleton = value; }
    }
    private LevelManager levelManager;
    public LevelManager LevelManager
    {
        get { return levelManager; }
        set { levelManager = value; }
    }
    private SkeletonAnimatorController skeletonAnimatorController;
    public SkeletonAnimatorController SkeletonAnimatorController
    {
        get { return skeletonAnimatorController; }
        set { skeletonAnimatorController = value; }
    }
    private SkeletonEvent skeletonEvent;
    public SkeletonEvent SkeletonEvent
    {
        get { return skeletonEvent; }
        set { skeletonEvent = value; }
    }

    void Awake()
    {
        skeleton = GetComponentInChildren<Skeleton>();
        skeletonAnimatorController = GetComponentInChildren<SkeletonAnimatorController>();
        skeletonEvent = GetComponentInChildren<SkeletonEvent>();
        levelManager = GetComponentInChildren<LevelManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeactivateSelf()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
