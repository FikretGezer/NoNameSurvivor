using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public abstract class ShootingBase : MonoBehaviour
    {
        [SerializeField] protected GunTypeScriptable gunType;
        [SerializeField] protected Transform bulletPoint;
        [SerializeField] protected Transform target;
        [SerializeField] protected float bulletCoolDown = 1f;
        [SerializeField] protected float GivingDamage = 1f;
        [field:SerializeField] protected float bulletSpeed;
        private bool didShoot;
        private float coolDown;
        private float damageAmount;
        private float critique;
        private float range;
        private float pierce;
        private void Start() {
            SetGunParameters(gunType.coolDown, gunType.damageAmount, gunType.critique, gunType.range, gunType.pierce);
        }
        public virtual void Update()
        {
            ChooseTarget();
            if(target != null)
            {
                if(!didShoot)
                {
                    StartCoroutine(Timer(coolDown));
                    didShoot = true;
                }
            }
        }
        private void ChooseTarget()
        {
            if(target == null)
            {
                target = transform.parent.GetComponent<PlayerController>()._target;
            }
            else
            {
                if(!target.gameObject.activeInHierarchy)
                    target = null;
            }
        }
        private void SetGunParameters(float coolDown, float damageAmount, float critique, float range, float pierce)
        {
            this.coolDown = coolDown;
            this.damageAmount = damageAmount;
            this.critique = critique;
            this.range = range;
            this.pierce = pierce;
        }
        private void Shoot()
        {
            ChooseTarget();
            if(target != null)
            {
                var dir = (target.position - transform.parent.position).normalized;
                var bullet = BulletPoolManager.Instance.GetPooledObject();
                if(bullet != null)
                {
                    //bullet.SetActive(true);
                    bullet.transform.position = bulletPoint.position;
                    bullet.GetComponent<Bullet>().ShootThis(Fire);
                    bullet.GetComponent<Bullet>().SetDamage(damageAmount);
                }

                void Fire()
                {
                    bullet.transform.Translate(dir * bulletSpeed * Time.deltaTime);
                }
            }
        }
        IEnumerator Timer(float coolDown)
        {
            var elapsedTime = 0f;
            Shoot();
            while(elapsedTime < coolDown)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            didShoot = false;
        }
        private void OnDrawGizmos() {
            if(target != null)
            {
                var dir = target.position - transform.parent.position;
                Gizmos.DrawLine(transform.parent.position,transform.parent.position + dir);
            }
        }
    }
}
