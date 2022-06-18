using System;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Weapons
{
    public class SpitBase : MyMonoBehaviour
    {
        /// <summary>
        /// Initial settings for the bullet
        /// </summary>
        public SpitSettings InitSettings;

        /// <summary>
        /// Sets if the bullet is destroyed after a collision.
        /// </summary>
        [HideInInspector]
        public bool IsDestroyed = false;

        void Awake()
        {
            if (InitSettings == null)
            {
                throw new NullReferenceException("BulletSetting cannot be null");
            }
        }
    }
}
