using UnityEngine;


public class RayEnemy : MonoBehaviour
{
    public RaycastHit2D Hit;

    #region Fields
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private GameObject _enemy;

    private float _direction = 1;
    private Enemy _enemyScript;
    #endregion

    #region UnityMethods
    private void Start()
    {
        _enemyScript = _enemy.GetComponent<Enemy>();
    }

    void FixedUpdate()
    {
        if (_enemyScript.Direction == 1) _direction = 1;
        else if (_enemyScript.Direction == -1) _direction = -1;

        Hit = Physics2D.Raycast(transform.position, _direction * Vector2.right, _distance, _mask);
        if (Hit)
        {
            _enemyScript.SeePlayer(Hit.collider.gameObject);

        }
        else
        {
            _enemyScript.LostPlayer();
        }
        Debug.DrawRay(transform.position, _direction * Vector2.right * _distance, Color.red);
    }
    #endregion
}
