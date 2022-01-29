using UnityEngine;


public class ExplosionParticleSystem : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}
