using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Sprite cursorSprite;
    void Awake()
    {
        
    }
    void Start()
    {
        Texture2D cursorTexture = SpriteToTexture2D(cursorSprite);
        Vector2 hotspot = Vector2.zero;
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Texture2D SpriteToTexture2D(Sprite sprite)
    {
        Texture2D texture = sprite.texture;
        if (!texture.isReadable)
        {
            Debug.LogError("Texture2D.GetPixels: texture data is not readable, please enable 'Read/Write Enabled' in import settings.");
            return null;
        }

        if (sprite.rect.width != texture.width || sprite.rect.height != texture.height)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = texture.GetPixels((int)sprite.textureRect.x, 
                (int)sprite.textureRect.y, 
                (int)sprite.textureRect.width, 
                (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return texture;
    }
}
