using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class EventManager : MonoBehaviour
    {
        public Dictionary<GameObject, List<Action>> deathEvent = new Dictionary<GameObject, List<Action>>();
        public void DestroyObject(GameObject obj)
        {
            Destroy(obj);
        }

        public static void SpawnObject(GameObject prefab, Vector3 position)
        {
            var obj = Instantiate(prefab);
            obj.transform.position = position;
        }
    }
}
