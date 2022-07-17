using InFlammis.Victoria.Assets.Scripts.Managers;
using System;
using UnityEngine;
using System.Linq;

namespace InFlammis.Victoria.Assets.Scripts.Layout
{
    public class Sector : MonoBehaviour
    {
        public enum SectorState
        {
            // All NAA and AA are inactive
            Inactive,

            // AA are active, NAA are inactive
            Awaken,

            //
            Active
        }

        private SectorState State;

        [SerializeField] private StaticObjectsSO _staticObjects;

        private ActivationArea northAa;
        private ActivationArea southAa;

        private NeighbourActivationArea northNaa;
        private NeighbourActivationArea southNaa;

        [SerializeField]private Color activeColor;
        [SerializeField]private Color inactiveColor;
        [SerializeField]private Color awakenColor;

        private Collider2D sectorCollider;

        private bool playerInS1 = false;
        private bool playerInNaa = false;
        private bool playerInAa = false;

        public bool PlayerInS1
        {
            get => playerInS1;
            set
            {
                playerInS1 = value;
                SetState();
            }
        }
        public bool PlayerInNaa
        {
            get => playerInNaa;
            set
            {
                playerInNaa = value;
                SetState();
            }
        }
        public bool PlayerInAa
        {
            get => playerInAa;
            set
            {
                playerInAa = value;
                SetState();
            }
        }

        public void NeighbourActivation(Sector neighbour)
        {
            PlayerInNaa = true;
            SetState();
        }
        public void NeighbourDeactivation(Sector neighbour)
        {
            PlayerInNaa = false;
            SetState();
        }

        private void Awake()
        {
            sectorCollider = GetComponent<Collider2D>();
            northAa = gameObject.GetComponentsInChildren<ActivationArea>(true).SingleOrDefault(x => x.tag == "NorthAA").GetComponent<ActivationArea>();
            southAa = gameObject.GetComponentsInChildren<ActivationArea>(true).SingleOrDefault(x => x.tag == "SouthAA").GetComponent<ActivationArea>();

            northNaa = gameObject.GetComponentsInChildren<NeighbourActivationArea>(true).SingleOrDefault(x => x.tag == "NorthNAA").GetComponent<NeighbourActivationArea>();
            southNaa = gameObject.GetComponentsInChildren<NeighbourActivationArea>(true).SingleOrDefault(x => x.tag == "SouthNAA").GetComponent<NeighbourActivationArea>();
        }

        private void Start()
        {
            northAa.ActivationEvent += ActivationEvent;
            southAa.ActivationEvent += ActivationEvent;

            northAa.DeactivationEvent += DeactivationEvent;
            southAa.DeactivationEvent += DeactivationEvent;

            var playerTransform = _staticObjects.Messenger.RequestForPlayerTransform(this, null);

            playerInS1 = sectorCollider.OverlapPoint(playerTransform.position);
            SetState();
        }

        private void ActivationEvent(ActivationArea aa)
        {
            PlayerInAa = true;
            SetState();
        }
        private void DeactivationEvent(ActivationArea aa)
        {
            PlayerInAa = false;
            SetState();
        }

        private void SetState()
        {
            if (PlayerInS1 || PlayerInAa)
                SetActive();
            else if (!PlayerInS1 && !PlayerInAa && !PlayerInNaa)
                SetInactive();
            else if (!PlayerInS1 && !PlayerInAa && PlayerInNaa)
                SetAwaken();
        }

        private void SetActive()
        {
            State = SectorState.Active;
            northAa.SetActive(true);
            southAa.SetActive(true);
            northNaa.SetActive(true);
            southNaa.SetActive(true);
            sectorCollider.enabled = true;
            ToggleStainColliderState(true);
        }

        private void SetInactive()
        {
            State = SectorState.Inactive;
            northAa.SetActive(false);
            southAa.SetActive(false);
            northNaa.SetActive(false);
            southNaa.SetActive(false);
            sectorCollider.enabled = false;
            ToggleStainColliderState(false);
        }

        private void SetAwaken()
        {
            State = SectorState.Awaken;
            northAa.SetActive(true);
            southAa.SetActive(true);
            northNaa.SetActive(false);
            southNaa.SetActive(false);
            sectorCollider.enabled = true;
            ToggleStainColliderState(false);
        }

        private void SetState(SectorState state)
        {
            switch (state)
            {
                case SectorState.Inactive:
                    SetInactive();
                    break;
                case SectorState.Awaken:
                    SetAwaken();
                    break;
                case SectorState.Active:
                    SetActive();
                    break;
                default:
                    throw new Exception("Unmanaged state");
            };
        }

        private void ToggleStainColliderState(bool enabled)
        {

        }
        private void DisableColliders()
        {
            Debug.Log($"Disabling colliders for {gameObject.name}");
        }

        private void EnableColliders()
        {
            Debug.Log($"Enabling colliders for {gameObject.name}");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                return;
            }
            PlayerInS1 = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                return;
            }
            PlayerInS1 = false;
        }

        private void OnDrawGizmos()
        {
            var rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

            Gizmos.matrix = rotationMatrix;
            Gizmos.color = State switch
            {
                SectorState.Active => activeColor,
                SectorState.Awaken => awakenColor,
                _ => inactiveColor
            };

            Gizmos.DrawCube(Vector3.zero, transform.localScale);
        }
    }
}
