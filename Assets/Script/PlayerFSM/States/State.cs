using UnityEngine;

public abstract class State
{
    protected PlayerController _player;
    protected Rigidbody2D _rb;

    protected float _duration;
    protected float _timer;

    public bool IsFinished => _timer >= _duration;

    protected State(PlayerController player, float duration = float.MaxValue)
    {
        _player = player;
        _rb = player.Rb;

        _duration = duration;
    }
    
    public virtual void OnEnter() {}

    public virtual void OnUpdate()
    {
        if (float.MaxValue - _duration > .1f) _timer += Time.deltaTime;
    }
    public virtual void OnFixedUpdate() {}
    public virtual void OnExit() {}
}