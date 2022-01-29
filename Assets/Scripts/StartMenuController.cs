using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartMenuController : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private GameObject _selectLevelPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Dropdown _dropDownQuality;

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSelectLevelMenu()
    {
        HideButtons();
        _selectLevelPanel.SetActive(true);
    }

    public void OpenSettingsMenu()
    {
        HideButtons();
        _settingsPanel.SetActive(true);
    }

    public void Return()
    {
        ShowButtons();
        _selectLevelPanel.SetActive(false);
        _settingsPanel.SetActive(false);
    }

    public void ChangeQuality()
    {
        int qualityIndex = _dropDownQuality.GetComponent<Dropdown>().value;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    private void HideButtons()
    {
        foreach (var button in _buttons)
            button.SetActive(false);
    }

    private void ShowButtons()
    {
        foreach (var button in _buttons)
            button.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            Return();
    }

    private void Start()
    {
        _selectLevelPanel.SetActive(false);
        _settingsPanel.SetActive(false);
        _dropDownQuality.GetComponent<Dropdown>().value = 5;
    }
}
