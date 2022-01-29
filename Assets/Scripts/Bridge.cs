using UnityEngine;


public class Bridge : MonoBehaviour
{
    #region Fields
    public float RotationSpeed = 90;
    public bool IsRoped = true;
    public Vector3 DefPOS;
    public AudioClip BridgeFall;

    private bool _isGrounded = false;  
    private AudioSource _audio;
    #endregion


    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_isGrounded) return;
        if (!IsRoped)
        {
            Quaternion defRot = Quaternion.Euler(DefPOS);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, defRot, Time.deltaTime * RotationSpeed);
        }
        if(transform.rotation.z == DefPOS.z) _isGrounded = true;    
    }

    public void PlaySound()
    {
        _audio.PlayOneShot(BridgeFall);
    }
}

