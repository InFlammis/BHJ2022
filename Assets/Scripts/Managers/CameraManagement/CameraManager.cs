using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.CameraManagement
{
    public class CameraManager : MyMonoBehaviour
    {
        [SerializeField] private StaticObjectsSO _staticObjects;

        private void Awake()
        {
            
        }
        private void LateUpdate()
        {
            var playerTransform = _staticObjects.Messenger.RequestForPlayerTransform(this, "Player");
            if(playerTransform == null)
            {
                return;
            }
            this.transform.position = new Vector3((float)playerTransform.position.x, (float)playerTransform.position.y, this.transform.position.z);
            this.transform.rotation = playerTransform.rotation;
        }
    }
}
