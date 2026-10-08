using UnityEngine;

public class AttackState : State
{
    public AttackState(PlayerController player, float duration = float.MaxValue) : base(player) { }

    public override void OnEnter()
    {
        _player.Invoke(nameof(ShowAttackColor), _player.PreAttackDuration);
        _player.Invoke(nameof(ResetColor), _player.AttackDuration);
    }

    private void ResetColor()
    {
        _player.SpriteRenderer.color = Color.navajoWhite;
    }

    private void ShowAttackColor()
    {
        _player.SpriteRenderer.color = Color.orange;
    }
}