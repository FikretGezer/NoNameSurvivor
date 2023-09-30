using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class GunShooter : MonoBehaviour
    {
        // public override void Update()
        // {
        //     base.Update();
        // }
        [SerializeField] private GunTypeScriptable gunType;
        [SerializeField] private Transform bulletPoint;
        [SerializeField] private Transform target;
        [SerializeField] private float bulletCoolDown = 1f;
        [SerializeField] private float GivingDamage = 1f;
        [SerializeField] private float coolDown;
        [SerializeField] private int damageAmount;
        [field:SerializeField] private float bulletSpeed;
        private bool didShoot;
        private void Start() {
            SetGunParameters(gunType.cooldown, gunType.damageAmount, gunType.gunMesh, gunType.gunColor);
        }
        private void Update()
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
        public void FeatureMultiplier(int damage, int attackSpeed)
        {
            coolDown -= (coolDown * attackSpeed / 100f);
            damageAmount += damage;
        }
        public void SetGunParameters(float clDown, int damage, Mesh gunMesh, Color gunColor)
        {
            coolDown = clDown;
            damageAmount = damage;
            //GetComponent<MeshFilter>().mesh = gunMesh;
            GetComponent<Renderer>().material.color = gunColor;           
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
