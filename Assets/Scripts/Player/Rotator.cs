using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float _rotationRight;
    private float _rotationLeft = 180f;

    private void Start()
    {
        _rotationRight = transform.rotation.y;
    }

    public void Rotate(float direction) 
    {
        float rotationY = _rotationRight;

        if (direction < 0)
            rotationY = _rotationLeft;
        else if(direction > 0)
            rotationY = _rotationRight;

        transform.rotation = Quaternion.Euler(transform.rotation.x, rotationY, transform.rotation.z);
    }
}
