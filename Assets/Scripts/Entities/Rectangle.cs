using System.Collections.Generic;
using UnityEngine;

namespace Kartsan.PlayFlock.Entities
{
    /// <summary>
    ///  Класс Rectangle
    ///  Наследуется от DraggableElement, т.к. должен иметь возможность перетаскиваться.
    ///  Реализующий этот класс объект на сцене представляет из себя
    ///  объект с SpriteRenderer.
    /// </summary>
    public class Rectangle : DraggableElement
    {
        // Статический счетчик элементов, по нему так же присваевается id
        private static int _elementsCounter = 0;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Dictionary<int, Link> _linkDictionary;
        private int _id;

        public int GetID => _id;
        public Vector2 GetRectangleSize => _spriteRenderer.size;
        public Vector3 GetRectanglePosition => elementTransform.position;
        public Color GetRectangleColor => _spriteRenderer.color;

        public void SetRectangleColor(Color color) => _spriteRenderer.color = color;

        /// <summary>
        ///  Метод SetLink
        ///  Добавляет в список связей link, ключом является id связанного прямоугольника
        /// </summary>
        public void SetLink(int otherRectangleID, Link link) => _linkDictionary.Add(otherRectangleID, link);

        public bool FindMatchesInLinkedID(int id) => _linkDictionary.ContainsKey(id);

        public Link GetLinkByID(int id) => _linkDictionary[id];

        public void RemoveLink(int id) => _linkDictionary.Remove(id);

        private new void Awake()
        {
            base.Awake();
            _elementsCounter++;
            _id = _elementsCounter;
            _linkDictionary = new Dictionary<int, Link>();
        }
    }
}