using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    private Skeleton skeleton;

    void Awake()
    {
        skeleton = GetComponentInChildren<Skeleton>();
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
