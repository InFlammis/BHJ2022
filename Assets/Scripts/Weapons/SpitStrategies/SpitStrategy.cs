using System;
using System.Collections;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Weapons
{
    public abstract class SpitStrategy : MyMonoBehaviour
    {
        public SpitStrategySettings InitSettings;
        public abstract event Action<Vector3, Quaternion> SpitEvent;
        public abstract event Action<float> BeginSpitEvent;
        public abstract event Action<float> EndSpitEvent;

        protected abstract IEnumerator Spit();

        protected abstract void RaiseBeginSpitEvent();
        protected abstract void RaiseEndSpitEvent();

        protected Coroutine _spitCoroutine;


        public void StartSpitting()
        {
            if (IsSpitting)
            {
                return;
            }

            IsSpitting = true;
            _spitCoroutine = StartCoroutine(Spit());
            RaiseBeginSpitEvent();
        }

        public void StopSpitting()
        {
            if (!IsSpitting)
            {
                return;
            }

            if (_spitCoroutine != null)
            {
                StopCoroutine(_spitCoroutine);
            }

            _spitCoroutine = null;
            IsSpitting=false;
            RaiseEndSpitEvent();
        }

        public bool IsSpitting { get; protected set; }

    }
}