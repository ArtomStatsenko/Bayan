using UnityEngine;


public class HelpMessage : MonoBehaviour
{
    [SerializeField] private GameObject _item;
    
    void Update()
    {
        if (_item == null)
            Destroy(gameObject);
    }
}
