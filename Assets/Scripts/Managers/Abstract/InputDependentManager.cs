using UnityEngine;

namespace Kartsan.PlayFlock.Managers
{
    /// <summary>
    ///  Абстрактный класс InputDependentManager
    ///  Подписывается на действия PlayerInputManager`a при активации
    ///  и отписывается от них при деактивации GameObject`a.
    /// </summary>
    public abstract class InputDependentManager : MonoBehaviour
    {
        protected void OnEnable()
        {
            PlayerInputManager.OnLeftSingleClick += LeftSingleClick;
            PlayerInputManager.OnLeftDoubleClick += LeftDoubleClick;
            PlayerInputManager.OnRightSingleClick += RightSingleClick;
        }

        protected void OnDisable()
        {
            PlayerInputManager.OnLeftSingleClick -= LeftSingleClick;
            PlayerInputManager.OnLeftDoubleClick -= LeftDoubleClick;
            PlayerInputManager.OnRightSingleClick -= RightSingleClick;
        }

        protected virtual void LeftSingleClick(Vector3 clickPosition) { }

        protected virtual void LeftDoubleClick(Vector3 clickPosition) { }

        protected virtual void RightSingleClick(Vector3 clickPosition) { }
    }
}