using UnityEngine;
#if UNITY_ANDROID
using UnityStandardAssets.CrossPlatformInput;
#endif


public class Item : MonoBehaviour
{
    private KnightPlayer _knightScript;
    private bool _canUse;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _canUse = true;
            _knightScript = collision.GetComponent<KnightPlayer>();
            _knightScript.PlayerNeerActiveObject = true;
        }
    }

    private void Update()
    {
        if (!_canUse) return;
        if (Input.GetButtonDown("Use"))
        {
            gameObject.SetActive(false);
            _knightScript.Inventory[0] = gameObject;
        }

        //if (CrossPlatformInputManager.GetButtonDown("Use"))
        //{
        //    gameObject.SetActive(false);
        //    _knightScript.Inventory[0] = gameObject;
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _canUse = false;
            _knightScript.PlayerNeerActiveObject = false;
            _knightScript.Inventory[0] = null;
        }
    }
}
