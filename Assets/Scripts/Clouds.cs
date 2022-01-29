using UnityEngine;


public class Clouds : MonoBehaviour
{
    [SerializeField] private float _speed;

    void Update()
    {
        float _curSpeed = -1 * _speed * Time.deltaTime;
        transform.position += Vector3.right * _curSpeed;
    }
}
