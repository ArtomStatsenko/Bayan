using UnityEngine;


public class CameraBehaviour : MonoBehaviour
{
    #region Fields
    [SerializeField] private float _leftBorder; //0.5f
    [SerializeField] private float _rightBorder; //270.0f
    [SerializeField] private float _topBorder;  //4.0f
    [SerializeField] private float _bottomBorder; //-1.0f
    [SerializeField] private float _speed;
    [SerializeField] private Transform _knightPlayer;

    private float _cameraPositionZ = -10f;
    private Vector3 _offset;
    private Vector2 _cameraCurrentPosition;
    #endregion


    #region UnityMethods
    void Start()
    {
        _offset = transform.position - _knightPlayer.position;
    }

    void LateUpdate()
    {
        float moveSpeed = Mathf.Clamp(Time.deltaTime * _speed, 0, 1);
        _cameraCurrentPosition.x = Mathf.Lerp(transform.position.x, _knightPlayer.position.x, moveSpeed);
        _cameraCurrentPosition.y = Mathf.Lerp(transform.position.y, _knightPlayer.position.y, moveSpeed);

        Move();
    }
    #endregion


    #region Methods
    private void Move()
    {
        transform.position = new Vector3(_cameraCurrentPosition.x, _cameraCurrentPosition.y, _cameraPositionZ);

        //границы игровой области для камеры
        if (transform.position.x <= _leftBorder)
            transform.position = new Vector3(_leftBorder, transform.position.y, _cameraPositionZ);
        if (transform.position.x >= _rightBorder)
            transform.position = new Vector3(_rightBorder, transform.position.y, _cameraPositionZ);
        if (transform.position.y <= _bottomBorder)
            transform.position = new Vector3(transform.position.x, _bottomBorder, _cameraPositionZ);
        if (transform.position.y >= _topBorder)
            transform.position = new Vector3(transform.position.x, _topBorder, _cameraPositionZ);
    }
    #endregion
}