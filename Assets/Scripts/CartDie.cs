using UnityEngine;


public class CartDie : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cart"))
            Destroy(collision);
    }
}
