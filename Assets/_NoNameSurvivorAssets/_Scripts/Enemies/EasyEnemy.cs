using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class EasyEnemy : EnemyBase, IEnemy
    {
        
        [field:Header("IEnemy Parameters")]
        [field:SerializeField] public int GivenDamage {get;set;}

        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag == "Player")
            {
                if(canGiveDamage)
                {
                    Debug.Log("Player here");
                    other.GetComponent<IDamageable>().TakeDamage(GivenDamage);
                    StartCoroutine(CooldownForGivingDamage(attackCoolDown));
                    canGiveDamage = false;
                }
            }
        }
        private void Update()
        {
            var dir = CharacterSpawner.Instance._position - transform.position;
            transform.Translate(dir * speed * Time.deltaTime);
        }
        public void Attack() //Fist, Different guns, bomb,
        {
            speed = 1;
        }        
        private IEnumerator CooldownForGivingDamage(float coolDown)
        {   
            yield return new WaitForSeconds(coolDown);
            canGiveDamage = true;
        }
    }
}
