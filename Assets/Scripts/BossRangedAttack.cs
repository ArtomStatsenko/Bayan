using UnityEngine;
using System.Collections;


public class BossRangedAttack : MonoBehaviour
{
    #region Fields
    public float ReloadTime = 3;
    //public AudioClip Stick_shot;
    //public AudioClip Stick_reload;

    [SerializeField] private float _deviation = 100;
    [SerializeField] private GameObject _fireBall;
    [SerializeField] private GameObject _rayEnemy;
    [SerializeField] private Transform _startFireBall;
    [SerializeField] private Transform _target;

    private bool _canShoot = true;
    private Enemy _enemyScript;
    private RayEnemy _rayEnemyScript;
    private AudioSource _audio;
    #endregion


    #region UnityMethods
    private void Start()
    {
        _enemyScript = gameObject.GetComponent<Enemy>();
        _rayEnemyScript = _rayEnemy.GetComponent<RayEnemy>();
    }

    private void Update()
    {
        if (_enemyScript.IsSeePlayer && _canShoot)
        {
            //_audio.PlayOneShot(Stick_shot);
            Vector3 playerPosition = _rayEnemyScript.Hit.transform.position;
            Vector3 fireBallPosition = transform.position;
            Vector3 relativePosition = playerPosition - fireBallPosition;

            float angle = Mathf.Atan2(relativePosition.x, relativePosition.y) * Mathf.Rad2Deg;
            _startFireBall.rotation = Quaternion.Euler(0, 0, _deviation - angle);

            Instantiate(_fireBall, _startFireBall.position, _startFireBall.rotation);
            _canShoot = false;
            //_audio.PlayOneShot(Stick_reload);
            StartCoroutine(CannotShoot());
        }
    }
    #endregion


    IEnumerator CannotShoot()
    {
        yield return new WaitForSeconds(ReloadTime);
        _canShoot = true;
    }  
}
