using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class BootyManager : MonoBehaviour
{
    protected SkeletonController skeletonController;
    protected PlayerController playerController;
    [SerializeField]
    private int xp;
    public int Xp
    {
        get { return xp; }
        set { xp = value; }
    }

    [SerializeField]
    private GameObject[] booties;
    public GameObject[] Booties
    {
        get { return booties; }
        set { booties = value; }
    }

    void Awake()
    {
        skeletonController = FindFirstObjectByType<SkeletonController>();
        playerController = FindFirstObjectByType<PlayerController>();
    }

    protected void OnDestroy()
    {
        int monsterLevel = skeletonController.LevelManager.Level.Get();
        int playerLevel = playerController.LevelManager.Level.Get();
        int xp = BootyEngine.CalculateXP(monsterLevel, playerLevel, skeletonController.Skeleton.entityType);
        playerController.LevelManager.ReceiveXP(xp);
    }
}
