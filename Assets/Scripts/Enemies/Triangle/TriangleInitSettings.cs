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
        public float DodgeForceMagnitude = 20f;

        [Range(0f, 10f)]
        public float StandStateDuration = 1.5f;

        [Range(0f, 10f)]
        public float SpinStateDuration = 1.5f;
    }
}
