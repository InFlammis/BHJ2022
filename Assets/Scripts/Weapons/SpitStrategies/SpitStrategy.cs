using System;
using System.Collections;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Weapons
{
    public abstract class SpitStrategy : MyMonoBehaviour
    {
        public SpitStrategySettings InitSettings;
        public abstract event Action<Vector3, Quaternion> SpitEvent;
        protected abstract IEnumerator Spit();

        protected Coroutine _spitCoroutine;


        public void StartSpitting()
        {
            _spitCoroutine = StartCoroutine(Spit());
        }

        public void StopSpitting()
        {
            if (_spitCoroutine != null)
            {
                StopCoroutine(_spitCoroutine);
            }

            _spitCoroutine = null;
        }

        public bool IsSpitting()
        {
            return _spitCoroutine != null;
        }
    }
}