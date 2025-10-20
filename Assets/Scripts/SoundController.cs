using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip _audio;

    private AudioSource _soundSource;

    private void Awake()
    {
        _soundSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _soundSource.PlayOneShot(_audio);
    }
}
