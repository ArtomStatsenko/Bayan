using UnityEngine;


public class Bolt : MonoBehaviour
{
    #region Fields
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _damage;

    private int _direction;
    private Rigidbody2D _rigidbody;
    #endregion


    #region UnityMethods
    void Start()
    {
        if (transform.rotation.y == 0) _direction = 1;
        else if (transform.rotation.y == 1) _direction = -1;

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddForce(_direction * Vector2.right * _speed, ForceMode2D.Impulse);

        Destroy(gameObject, _lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Hurt(_damage);
        }
        if (collision.gameObject.CompareTag("Rope"))
        {
            var rope = collision.gameObject.GetComponent<Rope>();
            rope.Hurt(_damage);
        }
        if (!collision.gameObject.CompareTag("Player")) Destroy(gameObject);
    }
    #endregion
}
