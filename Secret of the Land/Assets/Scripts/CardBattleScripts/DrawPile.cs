using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class DrawPile : MonoBehaviour
{
    public CardGameManager cardGameManager;

    private void OnMouseOver()
    {
        
    }

    private void OnMouseDown()
    {
        cardGameManager.DrawACard();
    }
}
