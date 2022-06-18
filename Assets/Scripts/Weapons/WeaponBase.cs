using System;
using System.Collections;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Weapons
{
    /// <summary>
    /// Base class for a Weapon
    /// </summary>
    public abstract class WeaponBase : MyMonoBehaviour
    {
        /// <summary>
        /// Weapon Type
        /// </summary>
        public WeaponType WeaponType;

        /// <summary>
        /// Initial settings for the weapon
        /// </summary>
        public WeaponSettings InitSettings;

        /// <summary>
        /// Reference to the GameObject used as a bullet
        /// </summary>
        public GameObject Bullet;
        private Coroutine _fireCoroutine;

        /// <summary>
        /// Reference to the WeaponSoundManager instance
        /// </summary>
        private WeaponSoundManager _soundManager;

        /// <summary>
        /// Ammo in the magazine
        /// </summary>
        public int Ammo;
        
        void Awake()
        {
            if (InitSettings == null)
            {
                throw new NullReferenceException($"InitSettings cannot be null for Weapon {this.GetType().Name}");
            }

            if (InitSettings.WeaponType != WeaponType)
            {
                throw new Exception(
                    $"Weapon Settings of type {InitSettings.WeaponType.ToString()} not compatible with Weapon of type {WeaponType}");
            }

            if (Bullet == null)
            {
                throw new NullReferenceException($"Bullet cannot be null for Weapon {this.GetType().Name}");
            }

            var bulletScript = Bullet.GetComponent<BulletBase>();

            if (bulletScript.InitSettings.WeaponType != WeaponType)
            {
                throw new Exception(
                    $"Bullet of type {bulletScript.InitSettings.WeaponType.ToString()} not compatible with Weapon of type {WeaponType}");
            }
            Ammo = InitSettings.Ammo;

            _soundManager = gameObject.GetComponentInChildren<WeaponSoundManager>();

            if(_soundManager == null)
            {
                throw new Exception("SoundManager not found");
            }

        }

        /// <summary>
        /// Start a Firing action spanned across multiple frames
        /// </summary>
        public virtual void StartFiring()
        {
            _fireCoroutine = StartCoroutine(Fire());
        }

        /// <summary>
        /// Stop a firing action spanned across multiple frames
        /// </summary>
        public virtual void StopFiring()
        {
            if (_fireCoroutine != null)
            {
                StopCoroutine(_fireCoroutine);
            }

            _fireCoroutine = null;
        }

        /// <summary>
        /// Coroutine managing the fire action
        /// </summary>
        /// <returns></returns>
        private IEnumerator Fire()
        {
            bool onlyOnce = InitSettings.RateOfFire == 0;

            var delta = 1.0f / InitSettings.RateOfFire;

            while (Ammo > 0)
            {
                var bulletGo = GameObject.Instantiate(this.Bullet, this.transform);
                _soundManager.PlayFireSound();

                bulletGo.transform.parent = null;
                Ammo--;

                if (onlyOnce) yield break;
                yield return new WaitForSeconds(delta);
            }
        }
    }
}
