using System;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Layout.Sectors
{
    public class ActivationArea : MonoBehaviour
    {
        public event Action<ActivationArea> ActivationEvent;
        public event Action<ActivationArea> DeactivationEvent;

        [SerializeField] Color color;

        private Collider2D _collider;

        public bool IsActive => _collider.enabled;

        public void SetActive(bool isActive)
        {
            _collider.enabled = isActive;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                return;
            }

            ActivationEvent?.Invoke(this);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                return;
            }

            DeactivationEvent?.Invoke(this);
        }

        private void OnDrawGizmos()
        {
            if (_collider == null || !IsActive)
            {
                return;
            }

            var rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

            Gizmos.matrix = rotationMatrix;
            Gizmos.color = color;
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
        }
    }
}
