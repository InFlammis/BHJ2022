using System;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Triangle
{
    [CreateAssetMenu(fileName = "New Triangle Init Settings", menuName = "Game/Init Settings/Triangle Settings")]
    public class TriangleInitSettings : EnemySettings
    {
        private void Awake()
        {
            base.EnemyType = EnemyType.Triangle;
        }

        [Range(0f, 100f)]
        public float _dodgeForceMagnitude = 20f;
    }
}
