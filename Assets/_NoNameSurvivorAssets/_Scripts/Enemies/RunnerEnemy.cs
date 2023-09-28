using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class RunnerEnemy : EnemyBase, IEnemy
    {
        [Header("Dash Parameters")]
        [SerializeField] private float dashingPower = 1f;
        [SerializeField] private float dashingTime = 0.5f;
        [SerializeField] private float dashingCooldown = 2f;


        private float dist;
        private bool canDash;
        private bool isDashing;        

        private Vector3 direction;

        private void OnEnable() {
            StartCoroutine(nameof(DashCooldown));
        }
        public override void Update()
        {  
            if(FindObjectOfType<PlayerController>())
                base.Update();
        }
        public override void Attack()
        {
            if(isDashing)
            {
                //Play flying kick animation
                transform.Translate(direction * dashingPower * Time.deltaTime);
                return;
            }
            
            direction = CharacterSpawner.Instance._position - transform.position;
            dist = direction.magnitude;
            transform.Translate(direction * speed * Time.deltaTime);

            if(canDash && dist < 7f)
            { 
                StartCoroutine(nameof(Dash));
            }
        }
        IEnumerator Dash()
        {
            canDash = false;
            isDashing = true;            

            yield return new WaitForSeconds(dashingTime);
            isDashing = false;

            StartCoroutine(nameof(DashCooldown));
        }
        IEnumerator DashCooldown()
        {
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;            
        }
    }
}
