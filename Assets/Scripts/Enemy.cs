using UnityEngine;


public class Enemy : MonoBehaviour
{
    #region Fields
    public float Direction;
    public bool IsSeePlayer = false;
    public AudioClip BoltEnemy;
    public AudioClip Hmmm;
    public AudioClip EnemyDetectPlayer;

    [SerializeField] private float _speed;
    [SerializeField] private float _speedWhenSeePlayer;
    [SerializeField] private float _impactForce;
    [SerializeField] private int _health; 
    [SerializeField] private GameObject _key;
    [SerializeField] private GameObject _legs;

    private float _horizontalSpeed;   
    private Transform _startKey;
    private AudioSource _audio;
    private Rigidbody2D _rigidbody;
    private AnimatorController _animatorController;
    #endregion


    #region UnityMethods
    private void Start()
    {
        InvokeRepeating("GetRandomDirection", 0f, 2f);
        _rigidbody = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        _animatorController = _legs.GetComponent<AnimatorController>();
    }

    private void Update()
    {
        Move();
        RotateToDirection();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collider For Enemy"))
        {
            Direction = -Direction;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Direction = -Direction;
        }
    }
    #endregion


    #region Methods
    private void Move()
    {
        _horizontalSpeed = Direction;
        transform.position += Vector3.right * _horizontalSpeed * _speed * Time.deltaTime;
        if (Direction == 0)
            _animatorController.Idle();
        else _animatorController.Walk();
    }

    private void RotateToDirection()
    {
        if (_horizontalSpeed < 0) transform.rotation = new Quaternion(0, 180, 0, 0);
        else if (_horizontalSpeed > 0) transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void Explode(int damage, Vector2 bombPosition)
    {
        Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 relativePosition = enemyPosition - bombPosition;
        _rigidbody.AddForce(relativePosition * _impactForce, ForceMode2D.Impulse);
        _health -= damage;
        if (_health <= 0) Die();
    }

    public void Hurt(int damage)
    {
        _health -= damage;
        _audio.PlayOneShot(BoltEnemy);
        if (_health <= 0) Die();
    }

    private void Die()
    {
        if (_key != null) DropKey();
        Destroy(gameObject);
    }

    private void DropKey()
    {
        _startKey = gameObject.transform;
        Instantiate(_key, _startKey.position, new Quaternion(0, 0, 0, 0));
    }

    private void GetRandomDirection()
    {
        Direction = Mathf.Round(Random.Range(-1f, 1f));
    }
    
    public void SeePlayer(GameObject player)
    {
        //CancelInvoke("GetRandomDirection");
        IsSeePlayer = true;
        Direction = player.transform.position.x - transform.position.x;
        if (Direction < -1) Direction = -1;
        else if (Direction > 1) Direction = 1;
        else Direction = 0;
        _speed = _speedWhenSeePlayer;
        //_audio.PlayOneShot(EnemyDetectPlayer);
    }

    public void LostPlayer()
    {
        //InvokeRepeating("GetRandomDirection", 0f, 2f);
        //Direction = 0;
        IsSeePlayer = false;
        _speed = 5;
        // _audio.PlayOneShot(Hmmm);
    }
    #endregion
}
