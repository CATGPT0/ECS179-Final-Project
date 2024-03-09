using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class BootyManager : MonoBehaviour
{
    protected SkeletonFSM skeleton;
    protected PlayerController playerController;

    [SerializeField]
    private GameObject[] booties;
    public GameObject[] Booties
    {
        get { return booties; }
        set { booties = value; }
    }

    void Awake()
    {
        skeleton = FindFirstObjectByType<SkeletonFSM>();
        playerController = FindFirstObjectByType<PlayerController>();
    }

    protected void OnDestroy()
    {
        // int monsterLevel = skeleton.properties.Level;
        // int playerLevel = playerController.LevelManager.Level;
        // int xp = BootyEngine.CalculateXP(monsterLevel, playerLevel, skeleton.properties.ThisType);
        // playerController.LevelManager.ReceiveXP(xp);
    }
}
