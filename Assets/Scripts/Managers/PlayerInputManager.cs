using System;
using UnityEngine;

/* 
 * Управление:
 * - Прямоугольник создается при клике левой кнопкой мыши и достаточном для его появления пространстве
 * - Прямоугольник удаляется при двойном клике левой нопкой мыши.
 * - Связь создается при клике правой кнопкой мыши по желаемому элементу и повторном клике по элементу с которым требуется связь
 * - Связь удаляется при клике правой кнопкой мыши по двум уже имеющим связь прямоугольникам 
 */

namespace Kartsan.PlayFlock.Managers
{
    /// <summary>
    ///  Класс PlayerInputManager
    ///  Обрабатывает ввод пользователя отправляя соответствующие Action.
    /// </summary>
    public class PlayerInputManager : MonoBehaviour
    {
        public static Action<Vector3> OnLeftSingleClick;
        public static Action<Vector3> OnLeftDoubleClick;
        public static Action<Vector3> OnRightSingleClick;

        [SerializeField] private Camera _camera;

        private float _lastLeftClickTime;

        private const float DOUBLE_CLICK_DETECTION_TIME = 0.2f;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) LeftMouseButtonClick();
            else if (Input.GetMouseButtonDown(1)) RightMouseButtonClick();
        }

        private void LeftMouseButtonClick()
        {
            float timeSinceLastClick = Time.time - _lastLeftClickTime;
            Vector3 clickWorldPointPosition = GetClickWorldPointPosition();

            if (timeSinceLastClick <= DOUBLE_CLICK_DETECTION_TIME) OnLeftDoubleClick?.Invoke(clickWorldPointPosition);
            else
            {
                OnLeftSingleClick?.Invoke(clickWorldPointPosition);
                _lastLeftClickTime = Time.time;
            }
        }

        private void RightMouseButtonClick()
        {
            Vector3 clickWorldPointPosition = GetClickWorldPointPosition();
            OnRightSingleClick?.Invoke(clickWorldPointPosition);
        }

        private Vector3 GetClickWorldPointPosition()
        {
            Vector3 clickWorldPointPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            clickWorldPointPosition.z = 0;

            return clickWorldPointPosition;
        }
    }
}