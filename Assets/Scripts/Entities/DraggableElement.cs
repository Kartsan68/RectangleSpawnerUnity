using UnityEngine;

namespace Kartsan.PlayFlock.Entities
{
    /// <summary>
    ///  Класс DraggableElement
    ///  Компонент позволяет перетаскивать объект к которому он прикреплен
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class DraggableElement : MonoBehaviour
    {
        private float _deltaX;
        private float _deltaY;
        private bool _isDrag;

        protected Transform elementTransform;

        protected void Awake() => elementTransform = transform;

        protected void Update()
        {
            if (_isDrag)
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                elementTransform.position = new Vector2(touchPos.x - _deltaX, touchPos.y - _deltaY);
            }
        }

        protected void OnMouseDown()
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _deltaX = touchPos.x - transform.position.x;
            _deltaY = touchPos.y - transform.position.y;
            _isDrag = true;
        }

        protected void OnMouseUp() => _isDrag = false;
    }
}