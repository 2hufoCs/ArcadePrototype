using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundCheck : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private LayerMask _groundLayer;
    
    private List<Collider2D> _groundCollisions = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ground")) return;
            
        _groundCollisions.Add(other);
        _player.OnChangeGroundState(true);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_groundCollisions.Contains(other)) return;
            
        _groundCollisions.Remove(other);
        if (_groundCollisions.Count == 0)
            _player.OnChangeGroundState(false);
    }
}
