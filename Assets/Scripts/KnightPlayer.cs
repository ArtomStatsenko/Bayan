using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
using UnityStandardAssets.CrossPlatformInput;
#endif


public class KnightPlayer : MonoBehaviour
{
    #region Fields

    public float ReloadTime = 1;
    public float ImmortalTime = 1;
    public int Health = 5;
    public int MaxHealth = 5;
    public int Bombs = 3;
    public bool PlayerNeerActiveObject;
    public GameObject[] Inventory = new GameObject[1];
    public Transform GroundCheck;
    public Vector3 CheckPoint;
    public LayerMask WhatIsGround;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private GameObject _cart;
    [SerializeField] private GameObject _bolt;
    [SerializeField] private GameObject _bomb;
    [SerializeField] private GameObject _legs;
    [SerializeField] private GameObject _crossBow;
    [SerializeField] private Transform _startBolt;
    [SerializeField] private Transform _startBomb;

    private float _horizontalSpeed;
    private float _groundRadius = 0.2f;
    private bool _isGrounded = false;
    private bool _canShoot = true;
    private bool _canHurt = true;
    private bool _canJump = false;
    private AnimatorController _animatorController;
    private AudioController _audioController;
    private Rigidbody2D _rigidBody;

    #endregion


    #region UnityMethods

    void Start()
    {
        CheckPoint = transform.position;
        PlayerNeerActiveObject = false;
        _animatorController = _legs.GetComponent<AnimatorController>();
        _audioController = GetComponent<AudioController>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        RotateToDirection();
        Shoot();
        PlantBomb();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && _canHurt)
        {
            Hurt(1);
        }
        if (collision.gameObject.CompareTag("Water"))
        {
            Respawn();
            _cart.GetComponent<Cart>().Respawn();
        }
    }

    #endregion


    #region Methods

    private void Move()
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            //_horizontalSpeed = CrossPlatformInputManager.GetAxis("Horizontal");
        }
        else
        {
            _horizontalSpeed = Input.GetAxis("Horizontal");
        }

        transform.position += Vector3.right * _horizontalSpeed * _speed * Time.deltaTime;

        if (!_canJump)
            if (_horizontalSpeed == 0 && !_canJump)
                _animatorController.Idle();
            else _animatorController.Walk();
    }

    private void RotateToDirection()
    {
        if (_horizontalSpeed < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            _startBolt.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (_horizontalSpeed > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            _startBolt.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    private void Jump()
    {
        //if (CrossPlatformInputManager.GetButtonDown("Jump"))
        //    _canJump = true;
        if (Input.GetButtonDown("Jump"))
            _canJump = true;
        else _canJump = false;

        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, _groundRadius, WhatIsGround);
        if (_isGrounded && _canJump)
        {
            _rigidBody.velocity = new Vector2(0f, _jumpSpeed);
            _animatorController.Jump();
        }
    }

    public void Hurt(int damage)
    {
        if (_canHurt)
        {
            Health -= damage;
            if (Health <= 0) Respawn();
            _canHurt = false;
            StartCoroutine(CannotHurt());
        }
    }

    public void Heal(int healPoint)
    {
        _audioController.HealSound();
        Health += healPoint;
        if (Health >= MaxHealth) Health = MaxHealth;
    }

    public void CheckPointSound()
    {
        _audioController.CheckPointSound();
    }


    private void Respawn()
    {
        transform.position = CheckPoint;
        Health = 5;
    }

    private void MouseFollow()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var angle = Vector2.Angle(Vector2.right, mousePosition - _crossBow.transform.position);
        _crossBow.transform.eulerAngles = new Vector3(0f, 0f, _crossBow.transform.position.y < mousePosition.y ? angle : -angle);
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && _canShoot)
        {
            _audioController.ShootSound();
            Instantiate(_bolt, _startBolt.position, _startBolt.rotation);
            _canShoot = false;
            StartCoroutine(CannotShoot());
        }

        //if (CrossPlatformInputManager.GetButtonDown("Fire") && _canShoot)
        //{
        //    _audioController.ShootSound();
        //    Instantiate(_bolt, _startBolt.position, _startBolt.rotation);
        //    _canShoot = false;
        //    StartCoroutine(CannotShoot());
        //}
    }

    private void PlantBomb()
    {
        if (Input.GetButtonDown("Fire3") && Bombs != 0)
        {
            _audioController.PlantBombSound();
            Instantiate(_bomb, _startBomb.position, _startBomb.rotation);
            Bombs--;
            if (Bombs <= 0) Bombs = 0;
        }

        //if (CrossPlatformInputManager.GetButtonDown("Bomb") && Bombs != 0)
        //{
        //    _audioController.PlantBombSound();
        //    Instantiate(_bomb, _startBomb.position, _startBomb.rotation);
        //    Bombs--;
        //    if (Bombs <= 0) Bombs = 0;
        //}
    }

    #endregion


    IEnumerator CannotShoot()
    {
        yield return new WaitForSeconds(ReloadTime);
        _canShoot = true;
    }

    IEnumerator CannotHurt()
    {
        yield return new WaitForSeconds(ImmortalTime);
        _canHurt = true;
    }
}
