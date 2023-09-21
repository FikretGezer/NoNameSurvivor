using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class RunnerEnemy : EnemyBase, IEnemy
    {
        public int GivenDamage { get; set;}
        
        public bool isAttacking;
        public bool canAttack;
        private float defaultSpeed;
        public float dist;

        public void Attack()
        {
            
        }

        private void Start() {
            defaultSpeed = speed;
            canAttack = true;
        }
        private Vector3 direction;
        private void Update()
        {  

            if(canAttack && !isAttacking && (dist > 10f && dist < 12f))
            {
                //Get the last seen position of player or direction
                //Move fast to that direction
                StartCoroutine(AttackTimer(3f)); 
                //Go into cooldown before next attack
                //After animation finished, keep following player again                
            }
            else//is dist close to the characters
            {
                direction = CharacterSpawner.Instance._position - transform.position;
                dist = direction.magnitude;
                transform.Translate(direction * speed * Time.deltaTime);
            }

            if(isAttacking)
            {
                transform.Translate(direction * speed * Time.deltaTime);
            }
        }
        private IEnumerator AttackTimer(float attackTime)
        {   
            isAttacking = true;
            speed = 1f;
            yield return new WaitForSeconds(attackTime);
            StartCoroutine(CooldownForAttack(2f));
        }
        private IEnumerator CooldownForAttack(float coolDown)
        {   
            isAttacking = false;
            canAttack = false;
            speed = defaultSpeed;

            yield return new WaitForSeconds(coolDown);
            canAttack = true;
        }
    }
}
