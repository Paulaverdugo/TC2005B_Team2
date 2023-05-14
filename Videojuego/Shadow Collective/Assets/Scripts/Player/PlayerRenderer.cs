using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerRenderer : MonoBehaviour
{
    // Init cursor texture
    public Texture2D cursorTexture;

    // Init sprite renderer
    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // Set cursor texture
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FaceDirection(Vector2 pointerinput)
    {
        var direction = (Vector3)pointerinput - transform.position;
        var result = Vector3.Cross(Vector2.up, direction);
        if (result.z > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (result.z < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
