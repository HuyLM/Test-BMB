using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillToScreen : MonoBehaviour
{
    public bool enable;
    public SpriteRenderer sprite;
    public Camera camera;
    public bool FitX;
    public bool FitY;
    public bool ratio;

    private void Awake()
    {
        if(enable)
        {
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (camera == null) camera = Camera.main;
        StartCoroutine(ResizeSpriteToScreen());
        }
    }

    [ContextMenu("Resize")]
    public IEnumerator ResizeSpriteToScreen()
    {
        yield return new WaitForSeconds(0.05f);
        if (sprite == null) yield break;

        sprite.transform.localScale = new Vector3(1, 1, 1);

        var width = sprite.sprite.bounds.size.x;
        var height = sprite.sprite.bounds.size.y;

        var worldScreenHeight = camera.orthographicSize * 2.0f;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        float x = (FitX ? worldScreenWidth / width : sprite.transform.localScale.x) * 1.1f;
        float y = (FitY ? worldScreenHeight / height : sprite.transform.localScale.y) * 1.1f;

        float r = 1;
        float px = x;
        float py = y;

        if (FitX)
        {
            r = x / sprite.transform.localScale.x;
            py = ratio ? y * sprite.transform.localScale.y * r : y;
        }

        if (FitY)
        {
            r = y / sprite.transform.localScale.y;
            px = ratio ? sprite.transform.localScale.x * r : x;
        }

        if (FitX && FitY)
        {
            px = x;
            py = y;
        }

        sprite.transform.localScale = new Vector2(px, py);
    }
}
