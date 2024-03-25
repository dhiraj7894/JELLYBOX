using Jelly.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActions : MonoBehaviour
{
    public static InputAction _moveAction;
    public static InputAction _dashAction;
    public static InputAction _jumpAction;


    public static InputAction _attack;
    public static InputAction _heavyAttack;

    public static InputAction _specialAttackA;
    public static InputAction _specialAttackB;

    public static InputAction _shieldAction;

    public static InputAction _pressF;
    public static InputAction _submit;


    public PlayerInput playerInput;

    private void Start()
    {
        _moveAction = playerInput.actions["Move"];
        _jumpAction = playerInput.actions["Jump"];
        _dashAction = playerInput.actions["Dash"];

        _attack = playerInput.actions["Attack"];
        _heavyAttack = playerInput.actions["HeavyAttack"];

        _specialAttackA = playerInput.actions["MiniSAttack"];
        _specialAttackB = playerInput.actions["UltimateAttack"];

        _shieldAction = playerInput.actions["Shield"];
        _pressF = playerInput.actions["PressF"];
        _submit = playerInput.actions["Submit"];
    }

    private void Update()
    {
        if (_pressF.triggered)
        {
            EventManager.Instance.PressedFButton();
        }
    }
}
