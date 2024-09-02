using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSet : MonoBehaviour
{
    private Resolution res;
    private void BackgroundStretch()
    {


        Debug.Log("setting background .............");
        res.height = Screen.height;
        res.width = Screen.width;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;
        transform.localScale = new Vector3(1, 1, 1);
        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;
        transform.localScale = new Vector2(Screen.width / width, Screen.height / height);
    }

    void Start()
    {
        BackgroundStretch();
    }
    void Update()
    {
        if (res.height != Screen.height || res.width != Screen.width)
        {
            BackgroundStretch();
        }
    }
}
