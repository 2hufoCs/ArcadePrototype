using UnityEngine;

public class JumpState : State
{
    public JumpState(PlayerController player) : base(player) { }

    public override void OnEnter()
    {
        _rb.AddForce(Vector2.up * _player.JumpForce, ForceMode2D.Impulse);
    }

    public override void OnUpdate()
    {
        _rb.linearVelocity = new Vector2(_player.MoveInput.x * _player.MoveSpeed, _rb.linearVelocity.y);
    }
    public override void OnExit() {}
}