using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Managers.StainsManagement
{
    public class StainsManager : MonoBehaviour
    {
        [SerializeField] private StaticObjectsSO _staticObjects;

        [SerializeField] private StainsManagerSettings _initSettings;

        private void Awake()
        {
            SubscribeToStainsManagerEvents();
        }

        public virtual void SubscribeToStainsManagerEvents()
        {
            var messenger = (_staticObjects.Messenger as ISpitEventsMessenger);

            messenger.HasDied.AddListener(Spit_HasDied);
        }

        private void Spit_HasDied(object publisher, string target)
        {
            // WIP
            var publisherGo = (GameObject)publisher;
            var newGo = new GameObject("Stain");
            newGo.layer = LayerMask.NameToLayer("Background");
            //newGo.tag = "";
            newGo.transform.parent = this.transform;
            newGo.transform.position = publisherGo.transform.position;
            newGo.transform.rotation = publisherGo.transform.rotation;

            var collider = newGo.AddComponent<CircleCollider2D>();
            collider.radius = 0.5f;
            collider.isTrigger = true;

            //-- WIP
        }
    }
}
