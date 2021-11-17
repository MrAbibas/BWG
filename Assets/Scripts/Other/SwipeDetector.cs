using System;
using UnityEngine;

public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}
public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}
public class SwipeDetector : MonoBehaviour
{
    public static event Action<SwipeData> OnSwipe = delegate { };
    private Vector2 _fingerDownPosition;
    private Vector2 _fingerUpPosition;
    [SerializeField]
    private bool _detectSwipeOnlyAfterRelease = false;
    [SerializeField]
    private float _minDistanceForSwipe = 20f;
    private void Update()
    {
        SwipeDetectLogic();
    }
    private void SwipeDetectLogic()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _fingerUpPosition = touch.position;
                _fingerDownPosition = touch.position;
            }

            if (!_detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved ||
                touch.phase == TouchPhase.Ended)
            {
                _fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
    }
    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            SwipeDirection direction;
            if (IsVerticalSwipe())
                direction = _fingerDownPosition.y - _fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
            else
                direction = _fingerDownPosition.x - _fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
            SendSwipe(direction);
            _fingerUpPosition = _fingerDownPosition;
        }
    }
    private bool IsVerticalSwipe() => VerticalMovementDistance() > HorizontalMovementDistance();

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > _minDistanceForSwipe || HorizontalMovementDistance() > _minDistanceForSwipe;
    }
    private float VerticalMovementDistance() => Mathf.Abs(_fingerDownPosition.y - _fingerUpPosition.y);
    private float HorizontalMovementDistance() => Mathf.Abs(_fingerDownPosition.x - _fingerUpPosition.x);
    private void SendSwipe(SwipeDirection direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = _fingerDownPosition,
            EndPosition = _fingerUpPosition
        };
        OnSwipe(swipeData);
    }
}