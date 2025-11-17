using UnityEngine;
using UnityEngine.UI;

public class UIFollower : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Image _smoothSlider;
    [SerializeField] private int _height;
    [SerializeField] private int _width;

    private void Update()
    {
        _smoothSlider.transform.position = new Vector2(_gameObject.transform.position.x + _width, _gameObject.transform.position.y + _height);
    }
}
