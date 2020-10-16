using UnityEngine;

namespace Kartsan.PlayFlock.Entities
{
    /// <summary>
    ///  Класс Link
    ///  Экземпляр этого класса прдеставляет из себя линию соединяющую 2 прямоугольника.
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class Link : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private Rectangle _firstRectangle;
        private Rectangle _secondRectangle;

        /// <summary>
        /// Метод LinkRectangles
        /// Соединяет 2 прямоугольника и задает цвет их соединению
        /// </summary>
        /// <param name="firstRect">первый выбранный для связывания прямоугольник</param>
        /// <param name="secondRect">второй выбранный для связывания прямоугольник</param>
        public void LinkRectangles(Rectangle firstRect, Rectangle secondRect)
        {
            _firstRectangle = firstRect;
            _secondRectangle = secondRect;

            _lineRenderer.startColor = firstRect.GetRectangleColor;
            _lineRenderer.endColor = secondRect.GetRectangleColor;

            UpdatePointPositions();
        }

        /// <summary>
        /// Метод UpdatePointPositions
        /// Обновляет вершины LineRenderer`а в соответствии с актуальным положением прямоугольников
        /// </summary>
        public void UpdatePointPositions()
        {
            _lineRenderer.SetPosition(0, _firstRectangle.GetRectanglePosition);
            _lineRenderer.SetPosition(1, _secondRectangle.GetRectanglePosition);
        }

        public bool ContainRectangleWithID(int id) => _firstRectangle.GetID == id || _secondRectangle.GetID == id;

        /// <summary>
        /// Метод RemoveLinkInfoInLinkedRectangle
        /// Удаляет информацию о данной связи у прямоугольников, которые в ней содержатся
        /// </summary>
        public void RemoveLinkInfoInLinkedRectangle()
        {
            _firstRectangle.RemoveLink(_secondRectangle.GetID);
            _secondRectangle.RemoveLink(_firstRectangle.GetID);
        }

        private void Awake() => _lineRenderer = GetComponent<LineRenderer>();
    }
}