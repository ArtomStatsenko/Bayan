using UnityEngine;


public class FireBall : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _damage;

    private int direction;

    #region UnityMethods
    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<KnightPlayer>();
            player.Hurt(_damage);
        }
        if (!collision.gameObject.CompareTag("Enemy")) Destroy(gameObject);
    }
    #endregion
}
