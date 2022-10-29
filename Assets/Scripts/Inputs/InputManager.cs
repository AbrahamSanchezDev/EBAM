using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UTS
{
    /// <summary>
    /// Controls the inputs and should be registered to its events
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        public static SwipeDirectionEvent OnSwipe = new SwipeDirectionEvent();
        public static SwipeDirectionRealTimeEvent OnSwipeRealTime = new SwipeDirectionRealTimeEvent();
        public static UnityEvent OnClickDown = new UnityEvent();
        public static UnityEvent OnClickHold = new UnityEvent();
        public static UnityEvent OnClickUp = new UnityEvent();

        #region Swipes

        private Vector2 _fingerUpPosition;
        private Vector2 _fingerDownPosition;
        private float _swipe_thereshold = 20f;

        #endregion

        //Called every frame Check for inputs
        protected void Update()
        {
            if (OnUi()) return;
            DetectSwipe();
        }

        private bool OnUi()
        {
            if (EventSystem.current) return EventSystem.current.IsPointerOverGameObject();

            return false;
        }

        //Check for swipe
        private void DetectSwipe()
        {
            //Check for Pc inputs for the start of the click
            if (Input.GetMouseButtonDown(0))
            {
                OnStartPosition(Input.mousePosition);
                return;
            }

            //Check for Pc inputs  for end of the click
            if (Input.GetMouseButtonUp(0))
            {
                OnEndPosition(Input.mousePosition);
                return;
            }

            //Check for Pc inputs for duration of the click
            if (Input.GetMouseButton(0))
            {
                OnHoldSwipe(Input.mousePosition);
                return;
            }

            //Check mobile inputs
            if (Input.touchCount == 0) return;
            var firstTouch = Input.touches[0];
            CheckTouch(firstTouch);
        }

        //Called when a swipe is happening
        private void OnHoldSwipe(Vector2 currentPosition)
        {
            OnClickHold.Invoke();
            OnSwipeRealTime.Invoke(_fingerDownPosition, currentPosition);
        }

        //Check the touch to see if should start or end
        private void CheckTouch(Touch firstTouch)
        {
            switch (firstTouch.phase)
            {
                case TouchPhase.Began:
                    OnStartPosition(firstTouch.position);
                    break;
                case TouchPhase.Ended:
                    OnEndPosition(firstTouch.position);
                    break;
                case TouchPhase.Moved:
                    OnHoldSwipe(firstTouch.position);
                    break;
            }
        }

        //Set the starting swipe position
        private void OnStartPosition(Vector2 position)
        {
            SetStartPos(position);
            SetEndPos(position);
            OnClickDown.Invoke();
        }

        //Set the ending swipe position
        private void OnEndPosition(Vector2 position)
        {
            SetEndPos(position);
            CheckSwipe();
            OnClickUp.Invoke();
        }

        //Set end Position
        private void SetStartPos(Vector2 position)
        {
            _fingerUpPosition = position;
        }

        //Set starting Position
        private void SetEndPos(Vector2 position)
        {
            _fingerDownPosition = position;
        }

        //Check for the swipe direction
        private void CheckSwipe()
        {
            var yMove = VerticalMove();
            var xMove = HorizontalMove();
            //Check for vertical swipe
            if (yMove > _swipe_thereshold && yMove > xMove)
            {
                //Up swipe
                if (_fingerUpPosition.y - _fingerDownPosition.y > 0)
                    OnSwipe.Invoke(SwipeDirection.Up);
                //Down swipe
                else if (_fingerUpPosition.y - _fingerDownPosition.y < 0)
                    OnSwipe.Invoke(SwipeDirection.Down);
                _fingerDownPosition = _fingerUpPosition;
            }
            //Check for horizontal swipe
            else if (xMove > _swipe_thereshold && xMove > yMove)
            {
                //Right swipe
                if (_fingerUpPosition.x - _fingerDownPosition.x > 0)
                    OnSwipe.Invoke(SwipeDirection.Right);
                //Left swipe
                else if (_fingerUpPosition.x - _fingerDownPosition.x < 0)
                    OnSwipe.Invoke(SwipeDirection.Left);
                _fingerDownPosition = _fingerUpPosition;
            }
        }

        //Absolute vertical position of the finger position on finish touch
        private float VerticalMove()
        {
            return Mathf.Abs(_fingerUpPosition.y - _fingerDownPosition.y);
        }

        //Absolute horizontal position of the finger position on finish touch
        private float HorizontalMove()
        {
            return Mathf.Abs(_fingerUpPosition.x - _fingerDownPosition.x);
        }
    }
}