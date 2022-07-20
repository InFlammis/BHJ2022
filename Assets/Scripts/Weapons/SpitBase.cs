using InFlammis.Victoria.Assets.Scripts.Managers;
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

        [HideInInspector]
        public StaticObjectsSO StaticObjects;


        /// <summary>
        /// Sets if the bullet is destroyed after a collision.
        /// </summary>
        [HideInInspector]
        public bool IsDestroyed = false;

        protected void InitCheck()
        {
            if (InitSettings == null)
            {
                throw new NullReferenceException("BulletSetting cannot be null");
            }
            if (StaticObjects == null)
            {
                throw new NullReferenceException("StaticObjects cannot be null");
            }
        }

        protected void SetInitSettings()
        {
            this.gameObject.layer = LayerMask.NameToLayer(InitSettings.Layer);
            this.gameObject.tag = InitSettings.Tag;
        }
    }
}
