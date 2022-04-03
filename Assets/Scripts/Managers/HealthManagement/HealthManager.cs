using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.HealthManagement
{
    public class HealthManager :
        MyMonoBehaviour,
        IHealthManager
    {
        [SerializeField] private HealthManagerSettingsSO _settings;
        [SerializeField] private StaticObjectsSO _staticObjects;

        public int MaxHealth { get; set; }

        private int _health;

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
        
        public bool IsInvulnerable { get; set; }
        
        public bool IsDead { get; protected set; }

        public string Target { get; set; }

        void Awake()
        {
            this.MaxHealth = _settings.MaxHealth;
            this.Health = _settings.InitHealth;
            this.IsInvulnerable = _settings.IsInvulnerable;
        }

        public void Heal(int byValue)
        {
            var newHealthValue =  Health + byValue;
            if (newHealthValue > MaxHealth)
            {
                newHealthValue = MaxHealth;
            }

            Health = newHealthValue;

        }

        public void Heal()
        {
            Heal(MaxHealth);
            IsDead = false;
        }

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
