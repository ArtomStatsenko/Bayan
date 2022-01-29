using UnityEngine;


public class Bomb : MonoBehaviour
{

    #region Fields

    [SerializeField] private float _explodeTime;
    [SerializeField] private int _damage;
    [SerializeField] private GameObject _rayBomb;
    [SerializeField] private GameObject _explosionParticleSystem;

    private Quaternion _particleSystemRotation = Quaternion.Euler(90, 0, 0);

    #endregion


    #region UnityMethods

    void Start()
    {
        Invoke("Explode", _explodeTime);
    }

    #endregion


    #region Methods

    private void Explode()
    {
        Vector2 bombPosition = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D[] enemy =_rayBomb.GetComponent<RayBomb>().Hit;
        foreach (var i in enemy)
        {
            i.collider.gameObject.GetComponent<Enemy>().Explode(_damage, bombPosition);
        }

        Instantiate(_explosionParticleSystem, transform.position, _particleSystemRotation);
        Destroy(gameObject);
    }

    #endregion

}
