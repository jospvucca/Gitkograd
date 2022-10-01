using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] _enemyPrefab;
    private float _difficulty = 1.0f;
    private IEnumerator _coroutine;

    [SerializeField]
    private int _boundLength = 10;

    private void Start()
    {
        _coroutine = SpawnEnemyController();
        StartCoroutine(_coroutine);
    }

    IEnumerator SpawnEnemyController()
    {
        while(true) //use gameisplaying var from gamemanager
        {
            yield return new WaitForSeconds(_difficulty);
            SpawnEnemy(_enemyPrefab[Random.Range(0, _enemyPrefab.Length)]);
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Transform spawnTransform = gameObject.transform;
        spawnTransform.position += new Vector3((float)Random.Range(-_boundLength, _boundLength), 0.0f, 0.0f);
        enemy.transform.position = spawnTransform.position;
        enemy.transform.rotation = spawnTransform.rotation;
        enemy.transform.localScale = new Vector3(5.0f, 5.0f, 1.0f);

        Instantiate(enemy);
        //Debug.Log("Enemy Spawned: " + enemy.name + " at position: " + enemy.transform.position.ToString());
    }
}
