using System;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Weapons
{
    public class SpitBase : MyMonoBehaviour
    {
        /// <summary>
        /// Initial settings for the bullet
        /// </summary>
        [HideInInspector]
        public SpitSettings InitSettings;

        /// <summary>
        /// Sets if the bullet is destroyed after a collision.
        /// </summary>
        [HideInInspector]
        public bool IsDestroyed = false;

        protected void SetInitSettings()
        {
            if (InitSettings == null)
            {
                throw new NullReferenceException("BulletSetting cannot be null");
            }

            this.gameObject.layer = LayerMask.NameToLayer(InitSettings.Layer);
            this.gameObject.tag = InitSettings.Tag;
        }
    }
}
