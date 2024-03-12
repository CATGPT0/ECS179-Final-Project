using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private GameObject bag;
    private bool isOpen;
    private GameObject playerSound;

    void Awake()
    {
        playerSound = GameObject.Find("PlayerSoundEffects");
        bag = GameObject.Find("Inventory");
        bag.SetActive(false);
        isOpen = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenBag();
    }

    void TryOpenBag()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            OpenBag();
        }
    }

    public void OpenBag()
    {
        isOpen = !isOpen;
        bag.SetActive(isOpen);
        playerSound.SetActive(!isOpen);
    }
}
