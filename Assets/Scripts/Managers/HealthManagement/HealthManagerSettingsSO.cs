﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.HealthManagement
{
    [CreateAssetMenu(fileName = "New HealthManager settings", menuName = "Game/Init Settings/HealthManager InitSettings")]

    public class HealthManagerSettingsSO : ScriptableObject
    {
        /// <summary>
        /// Initial health
        /// </summary>
        public int InitHealth;

        /// <summary>
        /// Max health
        /// </summary>
        public int MaxHealth;

        /// <summary>
        /// Target to whom send events
        /// </summary>
        public string Target;

        public bool IsInvulnerable;

        /// <summary>
        /// Is the enemy invulnerable at start.
        /// </summary>
        public bool IsInvulnerableAtStart;

        /// <summary>
        /// How many seconds the enemy is invulnerable at start
        /// </summary>
        public float InvulnerableAtStartForSeconds;

    }
}
