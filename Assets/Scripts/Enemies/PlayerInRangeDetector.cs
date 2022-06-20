using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Enemies
{
    public class PlayerInRangeDetector : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] string playerInRangeTrigger;
        [SerializeField] string bulletInRangeTrigger;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                Debug.Log("Player in range");
                animator.SetBool(playerInRangeTrigger, true);
            }
            else if (collision.gameObject.tag == "Bullet")
            {
                Debug.Log("Bullet in range");
                animator.SetBool(bulletInRangeTrigger, true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Player no longer in range");
                animator.SetBool(playerInRangeTrigger, false);
            }
            else if (collision.gameObject.tag == "Bullet")
            {
                Debug.Log("Bullet no longer in range");
                animator.SetBool(bulletInRangeTrigger, false);
            }
        }
    }
}
