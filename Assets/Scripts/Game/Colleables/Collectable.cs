using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    private ICollectableBahavior _collectableBahavior;

    private void Awake()
    {
        _collectableBahavior = GetComponent<ICollectableBahavior>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        var player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            _collectableBahavior.OnCollected(player.gameObject);
            Destroy(gameObject);
        }
    }
}
