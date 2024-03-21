using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Search;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item item;
    public Inventory inventory;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject.name == "Player")
        {
            AddItem();
            Destroy(gameObject);
        }
    }

    protected virtual void AddItem()
    {
        
    }
}
