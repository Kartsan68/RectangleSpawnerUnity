using Kartsan.PlayFlock.Data;
using Kartsan.PlayFlock.Entities;
using Kartsan.PlayFlock.Helpers;
using Kartsan.PlayFlock.Settings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kartsan.PlayFlock.Managers
{
    /// <summary>
    ///  Класс RectangleManager
    ///  Управляет создание и удалением прямоугольников на сцене.
    ///  Наследник InputDependentManager, обрабатывает события пользовательского ввода
    /// </summary>
    public class RectangleManager : InputDependentManager
    {
        public static Action<Rectangle> OnDestroyRectangle;

        [SerializeField] private RectangleColorBase _rectangleColorBase;
        [SerializeField] private Rectangle _rectanglePrefab;
        [SerializeField] private Transform _rectanglesParent;

        private List<Rectangle> _rectangles;

        protected override void LeftSingleClick(Vector3 clickPosition)
        {
            RaycastHit2D hit = Raycaster.GetBoxCastHitByPosition(clickPosition, _rectanglePrefab.GetRectangleSize);

            if (!hit) SpawnRectangle(clickPosition);
        }

        protected override void LeftDoubleClick(Vector3 clickPosition)
        {
            RaycastHit2D hit = Raycaster.GetRaycastHitByPosition(clickPosition);

            if (hit)
            {
                var clickedGameObject = hit.collider.gameObject;

                if (clickedGameObject.CompareTag(TagBase.RECTANGLE_TAG)) DestroyRectangle(clickedGameObject.GetComponent<Rectangle>());
            }
        }

        private void Awake()
        {
            _rectangles = new List<Rectangle>();
        }

        private void SpawnRectangle(Vector3 clickPosition)
        {
            var rectangle = Instantiate(_rectanglePrefab, clickPosition, Quaternion.identity, _rectanglesParent);
            rectangle.SetRectangleColor(_rectangleColorBase.GetRandomColor);
            _rectangles.Add(rectangle);
        }

        private void DestroyRectangle(Rectangle rectangle)
        {
            _rectangles.Remove(rectangle);
            OnDestroyRectangle?.Invoke(rectangle);
            Destroy(rectangle.gameObject);
        }
    }
}