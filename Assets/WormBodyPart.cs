using System.Collections.Generic;
using UnityEngine;

public class WormBodyPart : MonoBehaviour
{
    private Transform _leader;

    private Queue<(float Time, Vector3 Position)> _leaderPositions = new Queue<(float Time, Vector3 Position)>();
    public float Delay = .1f;

    public void Follow(Transform leader, int position)
    {
        _leader = leader;
        _leaderPositions.Enqueue((Time.time, _leader.position));

        if (position <= 5)
        {
            Destroy(GetComponent<CircleCollider2D>());
        }
    }

    public void OnLeaderPositionUpdated()
    {
        _leaderPositions.Enqueue((Time.time, _leader.position));

        var peek = _leaderPositions.Peek();

        while(Time.time - Delay >= peek.Time)
        {
            _leaderPositions.Dequeue();
            transform.position = peek.Position;
            peek = _leaderPositions.Peek();
        }
    }
}
