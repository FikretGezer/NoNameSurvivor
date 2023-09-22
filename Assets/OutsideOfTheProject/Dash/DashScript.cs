using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class DashScript : MonoBehaviour
    {
        public float speed = 1f;
        public float dashingPower = 1f;
        public float dashingTime = 0.2f;
        public float dashingCooldown = 1f;

        private bool canDash = true;
        private bool isDashing;
        private Vector2 direction;
        private void Update()
        {
            if(isDashing)
                return;

            Move(KeyCode.A, Vector2.left);
            Move(KeyCode.D, Vector2.right);
            Move(KeyCode.W, Vector2.up);
            Move(KeyCode.S, Vector2.down);

            if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }
        }
        private void Move(KeyCode k, Vector2 dir)
        {   
            if(Input.GetKey(k))
            {
                var pos = (Vector2)transform.position + dir * speed * Time.deltaTime;
                transform.position = pos;
                direction = dir;
            }
        }
        IEnumerator Dash()
        {
            canDash = false;
            isDashing = true;

            var pos = (Vector2)transform.position + direction * dashingPower;
            //transform.position = pos;
            StartCoroutine(Animate(pos));

            yield return new WaitForSeconds(dashingTime);
            isDashing = false;

            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;            
        }
        IEnumerator Animate(Vector2 pos)
        {
            var elapsedTime = 0f;
            while (elapsedTime <= dashingTime)
            {
                var t = elapsedTime / dashingTime;
                transform.position = Vector3.Lerp(transform.position, pos, t);                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
