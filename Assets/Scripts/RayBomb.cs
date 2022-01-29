using UnityEngine;


public class RayBomb : MonoBehaviour
{
    public RaycastHit2D[] Hit;

    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _mask;

    private Vector3 _startRay;

    void FixedUpdate()
    {
        _startRay = new Vector3(transform.position.x - _distance / 2, transform.position.y, 0);
        Hit = Physics2D.RaycastAll(_startRay, Vector2.right, _distance, _mask);
        Debug.DrawRay(_startRay, Vector2.right * _distance, Color.red);
    }
}
