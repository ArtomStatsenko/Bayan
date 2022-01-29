using UnityEngine;


public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _enemyBoss;

    private Transform _enemyPosition;

    private void Start()
    {
        _enemyPosition = gameObject.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _enemyPosition = gameObject.transform;
            Invoke("SpawnEnemy", 0.5f);
            Invoke("SpawnEnemy", 1f);
            Invoke("SpawnBoss", 1.5f);
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(_enemy, _enemyPosition);
    }
    private void SpawnBoss()
    {
        Instantiate(_enemyBoss, _enemyPosition);
    }

}
