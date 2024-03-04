using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnEngine
{
    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return GameObject.Instantiate(prefab, position, rotation);
    }
}
