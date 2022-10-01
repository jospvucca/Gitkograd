using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    public GameObject _coinPrefab;
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
        while (true)
        {
            yield return new WaitForSeconds(10);
            SpawnEnemy(_coinPrefab);
        }
    }

    void SpawnEnemy(GameObject coin)
    {
        Transform spawnTransform = gameObject.transform;
        spawnTransform.position += new Vector3((float)Random.Range(-_boundLength, _boundLength), 1.0f, 0.0f);
        coin.transform.position = spawnTransform.position;
        coin.transform.rotation = Quaternion.identity;
        coin.transform.localScale = new Vector3(2.0f, 2.0f, 0.1f);

        Instantiate(coin);
    }
}
