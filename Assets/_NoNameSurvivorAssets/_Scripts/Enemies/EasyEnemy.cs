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
                    //Play punch animation
                    other.GetComponent<IDamageable>().TakeDamage(GivenDamage);
                    StartCoroutine(AttackCooldown(attackCoolDown));
                    CameraMovement.Instance.StartShake();
                    canGiveDamage = false;
                }
            }
        }
        public override void Update()
        {
            base.Update();
        }
        public override void Attack() //Fist, Different guns, bomb,
        {
            var dir = CharacterSpawner.Instance._position - transform.position;
            transform.Translate(dir * speed * Time.deltaTime);
        }        
        private IEnumerator AttackCooldown(float cooldown)
        {   
            yield return new WaitForSeconds(cooldown);
            canGiveDamage = true;
        }
    }
}
