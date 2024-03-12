using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnEngine
{
    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, int level = 1)
    {
        switch (prefab.tag)
        {
            case "Player":
                return SpawnPlayer(prefab, position, rotation, level);
            case "Skeleton":
                return SpawnSkeleton(prefab, position, rotation, level);
            case "Slime":
                return SpawnSlime(prefab, position, rotation, level);
            default:
                return null;
        }
    }

    private static GameObject SpawnSlime(GameObject prefab, Vector3 position, Quaternion rotation, int level)
    {
        GameObject slime = GameObject.Instantiate(prefab, position, rotation);
        slime.GetComponent<SlimeFSM>().properties = new SlimeProperties(level, EntityType.Type.Slime);
        return slime;
    }

    public static GameObject SpawnPlayer(GameObject prefab, Vector3 position, Quaternion rotation, int level)
    {
        GameObject player = GameObject.Instantiate(prefab, position, rotation);
        player.GetComponent<Player>().properties = new PlayerProperties(level, EntityType.Type.Player);
        return player;
    }

    public static GameObject SpawnSkeleton(GameObject prefab, Vector3 position, Quaternion rotation, int level)
    {
        GameObject skeleton = GameObject.Instantiate(prefab, position, rotation);
        skeleton.GetComponent<SkeletonFSM>().properties = new SkeletonProperties(level, EntityType.Type.Skeleton);
        return skeleton;
    }
}
