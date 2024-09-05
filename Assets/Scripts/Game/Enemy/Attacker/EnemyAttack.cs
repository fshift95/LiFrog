using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] private int _enemyHuntScore;
    [SerializeField] private float _damageAmount;

    System.DateTime time;
    private void Start()
    {
        time = System.DateTime.Now;
    }
    bool cutScore = true;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            var HealthController = collision.gameObject.GetComponent<HealthController>();
            var ScoreController = collision.gameObject.GetComponent<ScoreController>();



            HealthController.TakeDamage(_damageAmount);

            if (cutScore)
            {
                ScoreController.cutScore(_enemyHuntScore);
                cutScore = false;
            }


        }
    }

}
