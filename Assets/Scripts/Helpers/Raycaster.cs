using UnityEngine;

namespace Kartsan.PlayFlock.Helpers
{
    /// <summary>
    ///  Класс Raycaster
    ///  Имеет два статических метода, которые возвращают RaycastHit2D
    ///  по необходимой пользователю координате.
    /// </summary>
    public class Raycaster
    {
        public static RaycastHit2D GetBoxCastHitByPosition(Vector3 clickWorldPointPosition, Vector2 boxSize) => Physics2D.BoxCast(clickWorldPointPosition, boxSize, 0, Vector2.zero);

        public static RaycastHit2D GetRaycastHitByPosition(Vector3 clickWorldPointPosition) => Physics2D.Raycast(clickWorldPointPosition, Vector2.zero);
    }
}