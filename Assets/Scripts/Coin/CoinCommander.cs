using UnityEngine;

public class CoinCommander : MonoBehaviour
{
    [SerializeField] private CoinSpawner _spawner;
    [SerializeField] private SoundController _soundController;

    private void OnEnable()
    {
        _spawner.CoinRelease += PlayCoinSound;
    }

    private void OnDisable()
    {
        _spawner.CoinRelease -= PlayCoinSound;
    }

    private void PlayCoinSound()
    {
        _soundController.PlaySound();
    }
}
