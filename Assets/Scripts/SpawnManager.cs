using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerUps;

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    public void startSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {


    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);
        while (_stopSpawning!=true)
        {
            GameObject newEnemy= Instantiate(_enemyPrefab, new Vector3(Random.Range(-8f,8f),6,0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5);
        }
        Destroy(_enemyContainer.gameObject);
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3);
        while (_stopSpawning!=true)
        {
            Instantiate(powerUps[Random.Range(0,3)], new Vector3(Random.Range(-8f,8f),6,0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f,8f));
        }
        Destroy(_enemyContainer.gameObject);
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
