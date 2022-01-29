using UnityEngine;


public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip Crossbow_shot;
    [SerializeField] private AudioClip Crossbow_reload;
    [SerializeField] private AudioClip HealthPoint;
    [SerializeField] private AudioClip CheckPoint;
    [SerializeField] private AudioClip BombExplode;

    private AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void HealSound()
    {
        _audio.PlayOneShot(HealthPoint);
    }

    public void ShootSound()
    {
        _audio.PlayOneShot(Crossbow_shot);
        _audio.PlayOneShot(Crossbow_reload);
    }

    public void PlantBombSound()
    {
        _audio.PlayOneShot(BombExplode);
    }

    public void CheckPointSound()
    {
        _audio.PlayOneShot(CheckPoint);
    }

}
