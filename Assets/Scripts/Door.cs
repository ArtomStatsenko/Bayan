using UnityEngine;
#if UNITY_ANDROID
using UnityStandardAssets.CrossPlatformInput;
#endif


public class Door : MonoBehaviour
{
    #region Fields

    public bool IsOpen = false;
    public AudioClip DoorOpenSound;

    [SerializeField] private Sprite _door_opened;
    [SerializeField] private Sprite _door_closed;

    private float _offset = 1.2f;
    private Vector3 _firstPos;
    private KnightPlayer _knightScript;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private AudioSource _audio;
    private bool _canUse;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _firstPos = transform.position;
        _audio = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (IsOpen)
        {
            transform.position = new Vector3(_firstPos.x + _offset, _firstPos.y, 0);
            _spriteRenderer.sprite = _door_opened;
            _boxCollider.enabled = false;
        }
        else
        {
            transform.position = new Vector3(_firstPos.x, _firstPos.y, 0);
            _spriteRenderer.sprite = _door_closed;
            _boxCollider.enabled = true;
        }
        if (_canUse)
        {
            if (Input.GetButtonDown("Use") && _knightScript.Inventory[0] != null)
            {
                _knightScript.Inventory[0] = null;
                IsOpen = true;
                _audio.PlayOneShot(DoorOpenSound);
            }

            //if (CrossPlatformInputManager.GetButtonDown("Use") && _knightScript.Inventory[0] != null)
            //{
            //    _knightScript.Inventory[0] = null;
            //    IsOpen = true;
            //    _audio.PlayOneShot(DoorOpenSound);
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _canUse = true;
            _knightScript = collision.GetComponent<KnightPlayer>();
            if (_knightScript.Inventory[0] != null)
            {
                _knightScript.PlayerNeerActiveObject = true;
            }
        }
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _canUse = false;
            _knightScript.PlayerNeerActiveObject = false;
        }
    }

    #endregion
}
