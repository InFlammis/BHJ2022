using InFlammis.Victoria.Assets.Scripts.Managers;
using System;
using UnityEngine;
using System.Linq;
using InFlammis.Victoria.Assets.Scripts.Layout.Sectors.StateMachine;

namespace InFlammis.Victoria.Assets.Scripts.Layout.Sectors
{
    public partial class Sector : MonoBehaviour, IEquatable<Sector>
    {
        public event Action<Sector> OnSectorActivated;
        public event Action<Sector> OnSectorDectivated;

        [SerializeField] private StaticObjectsSO _staticObjects;

        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;
        [SerializeField] private Color awakenColor;

        private IStateFactory _stateFactory;

        private State CurrentState;

        [HideInInspector]public Areas areas = new Areas();

        private bool playerInSector = false;
        private bool playerInNaa = false;
        private bool playerInAa = false;

        public bool PlayerInSector
        {
            get => playerInSector;
            set
            {
                playerInSector = value;
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

        private void Awake()
        {
            areas.SectorCollider = GetComponent<Collider2D>();
            areas.NorthAa = gameObject.GetComponentsInChildren<ActivationArea>(true).SingleOrDefault(x => x.tag == "NorthAA").GetComponent<ActivationArea>();
            areas.SouthAa = gameObject.GetComponentsInChildren<ActivationArea>(true).SingleOrDefault(x => x.tag == "SouthAA").GetComponent<ActivationArea>();
            areas.NorthNaa = gameObject.GetComponentsInChildren<NeighbourActivationArea>(true).SingleOrDefault(x => x.tag == "NorthNAA").GetComponent<NeighbourActivationArea>();
            areas.SouthNaa = gameObject.GetComponentsInChildren<NeighbourActivationArea>(true).SingleOrDefault(x => x.tag == "SouthNAA").GetComponent<NeighbourActivationArea>();
            areas.StainColliders = gameObject.GetComponentInChildren<StainCollidersCollection>();

            _stateFactory = new StateFactory(this);
        }

        private void Start()
        {
            areas.NorthAa.ActivationEvent += ActivationEvent;
            areas.SouthAa.ActivationEvent += ActivationEvent;
            areas.NorthAa.DeactivationEvent += DeactivationEvent;
            areas.SouthAa.DeactivationEvent += DeactivationEvent;

            var playerTransform = _staticObjects.Messenger.RequestForPlayerTransform(this, null);

            playerInSector = areas.SectorCollider.OverlapPoint(playerTransform.position);
            SetState();
        }

        public void NeighbourActivation(Sector neighbour)
        {
            PlayerInNaa = true;
        }
        public void NeighbourDeactivation(Sector neighbour)
        {
            PlayerInNaa = false;
        }

        private void ActivationEvent(ActivationArea aa)
        {
            PlayerInAa = true;
        }
        private void DeactivationEvent(ActivationArea aa)
        {
            PlayerInAa = false;
        }

        private void SetState()
        {
            if (PlayerInSector || PlayerInAa)
            {
                if (SetCurrentState(_stateFactory.Active))
                {
                    OnSectorActivated?.Invoke(this);
                }
            }
            else if (!PlayerInSector && !PlayerInAa && !PlayerInNaa)
            { 
                if (SetCurrentState(_stateFactory.Inactive))
                {
                    OnSectorDectivated?.Invoke(this);
                }
            }
            else if (!PlayerInSector && !PlayerInAa && PlayerInNaa)
            { 
                SetCurrentState(_stateFactory.Awaken); 
            }
        }

        private bool SetCurrentState(State state)
        {
            if(state == null)
            {
                return false;
            }
            if(state == CurrentState)
            {
                return false;
            }

            if(CurrentState != null)
            {
                CurrentState.OnExit();
            }

            CurrentState = state;

            CurrentState.OnEnter();

            return true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                return;
            }
            PlayerInSector = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                return;
            }
            PlayerInSector = false;
        }

        private void OnDrawGizmos()
        {
            var rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

            Gizmos.matrix = rotationMatrix;
            Gizmos.color = CurrentState switch
            {
                AwakenState => awakenColor,
                ActiveState => activeColor,
                _ => inactiveColor
            };

            Gizmos.DrawCube(Vector3.zero, transform.localScale);
        }

        public bool Equals(Sector other)
        {
            if(other == null)
            {
                return false;
            }

            return this.GetInstanceID() == other.GetInstanceID();
        }

        public class Areas
        {
            public ActivationArea NorthAa { get; set; }
            public ActivationArea SouthAa { get; set; }
            public NeighbourActivationArea NorthNaa { get; set; }
            public NeighbourActivationArea SouthNaa { get; set; }
            public Collider2D SectorCollider { get; set; }
            public StainCollidersCollection StainColliders { get; set; }
        }
    }
}
