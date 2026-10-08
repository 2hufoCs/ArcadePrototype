using UnityEngine;

public class MoveState : State
{
    public MoveState(PlayerController player) : base(player) { }

    public override void OnUpdate()
    {
        _rb.linearVelocity = new Vector2(_player.MoveInput.x * _player.MoveSpeed, _rb.linearVelocity.y);
    }
}