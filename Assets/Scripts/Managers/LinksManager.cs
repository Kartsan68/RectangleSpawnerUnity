using Kartsan.PlayFlock.Data;
using Kartsan.PlayFlock.Entities;
using Kartsan.PlayFlock.Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace Kartsan.PlayFlock.Managers
{
    /// <summary>
    ///  Класс LinksManager
    ///  Управляет созданием, удалением связей между прямоугольниками на сцене.
    ///  Так же хранит список всех имеющихся связей обновляя их вершины на каждом кадре.
    ///  Наследник InputDependentManager, обрабатывает события пользовательского ввода
    /// </summary>
    public class LinksManager : InputDependentManager
    {
        [SerializeField] private Link _link;
        [SerializeField] private Transform _linksParent;

        private List<Link> _links;
        private Rectangle _firstChangedRectangle;
        private Rectangle _secondChangedRectangle;
        private int _countOfChangedRectangles;

        protected override void LeftSingleClick(Vector3 clickPosition) => ResetChangedRectangles();

        protected override void RightSingleClick(Vector3 clickPosition)
        {
            RaycastHit2D hit = Raycaster.GetRaycastHitByPosition(clickPosition);

            if (hit)
            {
                var clickedGameObject = hit.collider.gameObject;

                if (clickedGameObject.CompareTag(TagBase.RECTANGLE_TAG)) ChangeRectangle(clickedGameObject.GetComponent<Rectangle>());
            }
        }

        private void Awake()
        {
            _countOfChangedRectangles = 0;
            _links = new List<Link>();
        }

        private void Update() => UpdateLinks();

        private new void OnEnable()
        {
            base.OnEnable();
            RectangleManager.OnDestroyRectangle += DestroyRectangleReaction;
        }

        private new void OnDisable()
        {
            base.OnDisable();
            RectangleManager.OnDestroyRectangle -= DestroyRectangleReaction;
        }

        private void ChangeRectangle(Rectangle rectangle)
        {
            switch (_countOfChangedRectangles)
            {
                case 0:
                    _firstChangedRectangle = rectangle;
                    _countOfChangedRectangles++;
                    break;
                case 1:
                    if (_firstChangedRectangle.GetID != rectangle.GetID)
                    {
                        _secondChangedRectangle = rectangle;
                        CheckAllLinks();
                    }
                    break;
            }
        }

        /// <summary>
        ///  Метод ResetChangedRectangles
        ///  После вызова данного метода, считается, что пользователь не выбрал ни одной фигуры
        /// </summary>
        private void ResetChangedRectangles()
        {
            _countOfChangedRectangles = 0;
            _firstChangedRectangle = null;
            _secondChangedRectangle = null;
        }

        /// <summary>
        ///  Метод CheckAllLinks
        ///  Проверка существования связи между двумя выбранными прямоугольниками.
        ///  Если связь уже существует - она уничтожается.
        ///  Если связи еще не существует - она создается.
        /// </summary>
        private void CheckAllLinks()
        {
            if (_firstChangedRectangle.FindMatchesInLinkedID(_secondChangedRectangle.GetID))
            {
                DestroyLink(_firstChangedRectangle.GetLinkByID(_secondChangedRectangle.GetID));
                _firstChangedRectangle.RemoveLink(_secondChangedRectangle.GetID);
                _secondChangedRectangle.RemoveLink(_firstChangedRectangle.GetID);
            }
            else CreateLink();

            ResetChangedRectangles();
        }

        private void CreateLink()
        {
            var link = Instantiate(_link, _linksParent);
            link.LinkRectangles(_firstChangedRectangle, _secondChangedRectangle);

            _firstChangedRectangle.SetLink(_secondChangedRectangle.GetID, link);
            _secondChangedRectangle.SetLink(_firstChangedRectangle.GetID, link);

            _links.Add(link);
        }

        private void DestroyLink(Link link)
        {
            _links.Remove(link);
            Destroy(link.gameObject);
        }

        /// <summary>
        ///  Метод UpdateLinks
        ///  Обновить все вершины существующих связей
        /// </summary>
        private void UpdateLinks()
        {
            foreach (var link in _links) link.UpdatePointPositions();
        }

        /// <summary>
        ///  Метод DestroyRectangleReaction
        ///  Получает прямоугольник, который будет уничтожен,
        ///  после чего связи этого прямоугольника с другими - уничтожаются.
        /// </summary>
        private void DestroyRectangleReaction(Rectangle rectangle)
        {
            List<Link> destroyQueue = new List<Link>();

            foreach (var link in _links)
                if (link.ContainRectangleWithID(rectangle.GetID))
                {
                    link.RemoveLinkInfoInLinkedRectangle();
                    destroyQueue.Add(link);
                }

            foreach (var link in destroyQueue) DestroyLink(link);
        }
    }
}