using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using TMPro;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Managers.HudManagement
{
    public class HudManager : MyMonoBehaviour, IHudManager
    {
        [SerializeField] private TextMeshProUGUI HiScore;
        [SerializeField] private TextMeshProUGUI Score;
        [SerializeField] private TextMeshProUGUI Multiplier;
        [SerializeField] private TextMeshProUGUI Health;
        [SerializeField] private TextMeshProUGUI Central;

        [SerializeField] private Gradient HealthGradient;

        [SerializeField] private StaticObjectsSO _staticObjects;

        void Awake()
        {
            _staticObjects.Messenger.SetCentralMessage.AddListener(this.SetCentralText);
            (_staticObjects.Messenger as IPlayerEventsMessenger).HealthLevelChanged.AddListener(this.SetHealth);
            _staticObjects.Messenger.ScoreChanged.AddListener(this.SetScore);
            _staticObjects.Messenger.MultiplierChanged.AddListener(this.SetMultiplier);
            _staticObjects.Messenger.HiScoreChanged.AddListener(this.SetHiScore);
        }

        public void SetHiScore(int value)
        {
            HiScore.text = value.ToString();
        }

        public void SetScore(int value)
        {
            Score.text = value.ToString();
        }

        public void SetMultiplier(int value)
        {
            Multiplier.text = value.ToString();
        }

        public void SetHealth(int value, int maxValue)
        {
            Health.text = value.ToString();
            Health.color = HealthGradient.Evaluate(value / (float)maxValue);
        }

        public void SetCentralText(string text)
        {
            Central.text = text;
        }

        public void SetHiScore(object publisher, string target, int value)
        {
            HiScore.text = value.ToString();
        }

        public void SetScore(object publisher, string target, int value)
        {
            Score.text = value.ToString();
        }

        public void SetMultiplier(object publisher, string target, int value)
        {
            Multiplier.text = value.ToString();
        }

        public void SetHealth(object publisher, string target, int value, int maxValue)
        {
            Health.text = value.ToString();
            Health.color = HealthGradient.Evaluate(value / (float)maxValue);
        }

        public void SetCentralText(object publisher, string target, string text)
        {
            Central.text = text;
        }
    }
}
