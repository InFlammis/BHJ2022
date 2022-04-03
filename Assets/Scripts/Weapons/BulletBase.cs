using System;

namespace BulletHellJam2022.Assets.Scripts.Weapons
{
    /// <summary>
    /// Base class for a Bullet
    /// </summary>
    public abstract class BulletBase : MyMonoBehaviour
    {
        /// <summary>
        /// Weapon to which the Bullet applies
        /// </summary>
        public WeaponType WeaponType;

        /// <summary>
        /// Initial settings for the bullet
        /// </summary>
        public BulletSettings InitSettings;
        
        /// <summary>
        /// Sets if the bullet is destroyed after a collision.
        /// </summary>
        public bool IsDestroyed = false;

        void Awake()
        {
            if (InitSettings == null)
            {
                throw new NullReferenceException("BulletSetting cannot be null");
            }

            if (InitSettings.WeaponType != WeaponType)
            {
                throw new Exception(
                    $"Bullet Settings of type {InitSettings.WeaponType.ToString()} not compatible with Bullet of type {WeaponType}");
            }
        }
    }
}
