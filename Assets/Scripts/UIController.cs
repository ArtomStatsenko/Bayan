using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    #region Fields

    public Text Message;

    [SerializeField] private Text _healthUI;
    [SerializeField] private Text _bombsUI;
    [SerializeField] private GameObject _knightPlayer;

    private KnightPlayer _knightScript;
    private int _health;
    private int _bombs;

    #endregion

    #region UnityMethods

    void Start()
    {
        Message.enabled = false;       
        _knightScript = _knightPlayer.GetComponent<KnightPlayer>();
    }

    void Update()
    {
        _health = _knightScript.Health;
        _bombs = _knightScript.Bombs;

        _healthUI.text = "Здоровье: " + _health.ToString();
        _bombsUI.text = "Бомбы: " + _bombs.ToString();

        if (_knightScript.PlayerNeerActiveObject)
            Message.enabled = true;
        else Message.enabled = false;
    }

    #endregion
}
