using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private bool _isJump;
    private bool _isAttack;
    private bool _isSkill;
    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(KeyCode.Space))
            _isJump = true;

        if (Input.GetKeyDown(KeyCode.F))
            _isAttack = true;

        if(Input.GetKeyDown(KeyCode.V))
            _isSkill = true;
    }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);
    public bool GetIsAttack() => GetBoolAsTrigger(ref _isAttack);
    public bool GetIsSkill() => GetBoolAsTrigger(ref _isSkill);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
