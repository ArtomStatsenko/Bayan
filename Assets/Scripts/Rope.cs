using UnityEngine;


public class Rope : MonoBehaviour
{
    [SerializeField] private GameObject _bridge;
    [SerializeField] private int _health = 1;

    public void Hurt(int damage)
    {
        _health -= damage;
        if (_health <= 0) Die();
    }

    private void Die()
    {       
        Destroy(gameObject);
        _bridge.GetComponent<Bridge>().PlaySound();
        _bridge.GetComponent<Bridge>().IsRoped = false;
    }
}
