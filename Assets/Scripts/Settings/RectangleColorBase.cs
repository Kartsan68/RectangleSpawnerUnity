using UnityEngine;

namespace Kartsan.PlayFlock.Settings
{
    [CreateAssetMenu(fileName = "RectangleColorBase", menuName = "SpawnedRect/RectangleColorBase")]
    public class RectangleColorBase : ScriptableObject
    {
        [SerializeField] private Color[] _colors;

        public Color GetRandomColor => _colors[Random.Range(0, _colors.Length)];
    }
}