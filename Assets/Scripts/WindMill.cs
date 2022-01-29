using UnityEngine;


public class WindMill : MonoBehaviour
{
    [SerializeField] private float _speed;

    void Update()
    {
        transform.Rotate(0, 0, _speed * Time.deltaTime);
    }
}
