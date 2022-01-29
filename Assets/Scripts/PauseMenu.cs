using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _mobileSingleStickControl;

    private bool _isClosed = true;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ManageMenu();
        }
        //else if (CrossPlatformInputManager.GetButtonDown("Cancel"))
        //{
        //    ManageMenu();
        //}
               
    }

    private void Start()
    {
        Time.timeScale = 1;
        _pauseMenuPanel.SetActive(false);
    }

    private void ManageMenu()
    {
        if (_isClosed)
        {
            Time.timeScale = 0;
            _pauseMenuPanel.SetActive(true);
            _isClosed = false;
            _mobileSingleStickControl.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            _pauseMenuPanel.SetActive(false);
            _isClosed = true;
            _mobileSingleStickControl.SetActive(true);
        }
    }
}
