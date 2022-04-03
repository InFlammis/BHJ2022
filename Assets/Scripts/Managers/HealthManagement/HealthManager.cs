using BulletHellJam2022.Assets.Scripts.MessageBroker;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.HealthManagement
{
    public class HealthManager :
        MyMonoBehaviour,
        IHealthManager
    {
        [SerializeField] private HealthManagerSettingsSO _settings;
        [SerializeField] private StaticObjectsSO _staticObjects;

        /// <inheritdoc/>
        public int MaxHealth { get; set; }

        /// <inheritdoc/>
        private int _health;

        /// <inheritdoc/>
        public int Health
        {
            get => _health;
            set
            {
                _health = value;

                PublishHealthLevelChanged(this, Target, _health, MaxHealth);

                if (_health <= 0)
                {
                    PublishHasDied(this, Target);
                }
            }
        }
        
        /// <inheritdoc/>
        public bool IsInvulnerable { get; set; }
        
        /// <inheritdoc/>
        public bool IsDead { get; protected set; }

        public string Target { get; set; }

        void Awake()
        {
            this.MaxHealth = _settings.MaxHealth;
            this.Health = _settings.InitHealth;
            this.IsInvulnerable = _settings.IsInvulnerable;
            //this.Target = _settings.Target;
        }

        /// <inheritdoc/>
        public void Heal(int byValue)
        {
            var newHealthValue =  Health + byValue;
            if (newHealthValue > MaxHealth)
            {
                newHealthValue = MaxHealth;
            }

            Health = newHealthValue;

        }

        /// <inheritdoc/>
        public void Heal()
        {
            Heal(MaxHealth);
            IsDead = false;
        }

        /// <inheritdoc/>
        public void Damage(int byValue)
        {
            if (IsInvulnerable) return;

            var newHealthValue = Health - byValue;
            if (newHealthValue < 0)
            {
                newHealthValue = 0;
            }

            Health = newHealthValue;
        }

        /// <inheritdoc/>
        public void Kill()
        {
            Damage(Health);
        }

        public void PublishHasDied(object publisher, string target)
        {
            (_staticObjects.Messenger as IHealthManagerEventsPublisher).PublishHasDied(this, target);
        }

        public void PublishHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
            (_staticObjects.Messenger as IHealthManagerEventsPublisher).PublishHealthLevelChanged(this, target, healthLevel, maxHealthLevel);
        }
    }
}
