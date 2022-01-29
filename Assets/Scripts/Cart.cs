using UnityEngine;
using System.Collections;


public class Cart : MonoBehaviour
{
    #region Fields

    public float SpeedInWater = 0;

    [SerializeField] private float _speedLow = 4;
    [SerializeField] private float _speedHigh = 10;
    [SerializeField] private GameObject _player;

    private Vector3 _startPosition;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _startPosition = gameObject.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.CompareTag("Water"))
        {
            SpeedInWater = _speedLow;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            float _curSpeedX = SpeedInWater * Time.deltaTime;
            transform.position += Vector3.right * _curSpeedX;         
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
        if (collision.gameObject.CompareTag("Water"))
        {
            float _curSpeedX = SpeedInWater * Time.deltaTime;
            transform.position += Vector3.right * _curSpeedX;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trigger For Cart"))
        {
            SpeedInWater = _speedHigh;
        }
        if (collision.gameObject.CompareTag("Trigger Die Cart"))
        {
            Destroy(gameObject);
        }
    }
    
    public void Respawn()
    {
        gameObject.transform.position = _startPosition;
    }

    #endregion
    
}
