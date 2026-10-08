using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[Flags] public enum States { Moving = 1, Jumping = 1 << 1}

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    
    private State _currentState;
    private States _states;

    [Header("Move")]
    [SerializeField] private float _moveSpeed;

    private Vector2 _moveInput;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCut;
    
    private bool _jumpInput;

    [Header("Attack")] 
    [SerializeField] private float _preAttackDuration;
    [SerializeField] private float _attackDuration;
    [SerializeField] private float _wholeAttackDuration;
    
    private bool _attackInput;

    // References
    public Rigidbody2D Rb => _rb;
    public  SpriteRenderer SpriteRenderer => _spriteRenderer;
    
    // Move
    public Vector2 MoveInput => _moveInput;
    public float MoveSpeed => _moveSpeed;
    
    // Jump
    public float JumpForce => _jumpForce;
    public bool IsGrounded { get; private set; }
    public Action<bool> OnChangeGroundState;
    
    // Attack
    public float PreAttackDuration => _preAttackDuration;
    public float AttackDuration => _attackDuration;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentState = new MoveState(this);
    }

    private void OnEnable()
    {
        OnChangeGroundState += value => IsGrounded = value;
    }

    private void OnDisable()
    {
        OnChangeGroundState -= value => IsGrounded = value;
    }

    private void Update()
    {
        _currentState.OnUpdate();
        // Debug.Log("is grounded: " + IsGrounded);
        
        // Transitions
        if (_currentState is JumpState && IsGrounded) // Jump to move
            SwitchState(new MoveState(this));
        if (_currentState is MoveState && IsGrounded && _jumpInput) // Move to jump
            SwitchState(new JumpState(this));
        if (_currentState is MoveState && IsGrounded && _attackInput) // Move to attack
            SwitchState(new AttackState(this, _wholeAttackDuration));
    }

    void SwitchState(State state)
    {
        _currentState?.OnExit();
        _currentState = state;
        _currentState.OnEnter();
        Debug.Log("entering state: " + state);
    }

    #region Player Input    
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _jumpInput = context.performed;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        _attackInput = context.performed;
    }
    
    #endregion
}