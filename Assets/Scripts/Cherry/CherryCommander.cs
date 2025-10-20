using UnityEngine;

public class CherryCommander : MonoBehaviour
{
    [SerializeField] private CherrySpawner _spawner;
    [SerializeField] private SoundController _soundController;

    private void OnEnable()
    {
        _spawner.CherryRelease += PlayCherrySound;
    }

    private void OnDisable()
    {
        _spawner.CherryRelease -= PlayCherrySound;
    }

    private void PlayCherrySound()
    {
        _soundController.PlaySound();
    }
}
