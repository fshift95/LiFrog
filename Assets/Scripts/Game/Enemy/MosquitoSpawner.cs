using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MosquitoSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private float _minimumSpawnTime;
    [SerializeField] private float _maximumSpawnTime;
    [SerializeField] private int _maximumSpawnCount;
    private float _timeUntilSpawn;
    // Start is called before the first frame update
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;
        if (_timeUntilSpawn <= 0)
        {
            if (GameObject.FindGameObjectsWithTag("mosquito").Length < _maximumSpawnCount)
            {
                Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
            }

        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
