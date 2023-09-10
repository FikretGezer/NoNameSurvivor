using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class EasyEnemy : EnemyBase
    {
        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag == "Player")
            {
                if(canGiveDamage)
                {
                    Debug.Log("Player here");
                    other.GetComponent<IDamageable>().TakeDamage(GivingDamage);
                    StartCoroutine(CooldownForGivingDamage(attackCoolDown));
                    canGiveDamage = false;
                }
            }
        }
        private IEnumerator CooldownForGivingDamage(float coolDown)
        {   
            yield return new WaitForSeconds(coolDown);
            canGiveDamage = true;
        }
    }
}
