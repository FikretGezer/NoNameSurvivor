using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class RevolverGuy : EnemyBase
    {
        [Header("Shooting Parameters")]
        [SerializeField] private float movementDelay = 1f;
        [SerializeField] private float shootingDelay = 1f;
        [SerializeField] private float bulletSpeed = 1f;
        [SerializeField] private Transform bulletPoint;

        private Vector3 direction;
        private float distance;
        private bool canShoot;
        private bool isShooting;

        private void OnEnable() {
            canShoot = true;
        }

        public override void Attack()
        {
            if(isShooting)
                return;

            direction = CharacterSpawner.Instance._position - transform.position;
            distance = direction.magnitude;
            transform.Translate(direction * speed * Time.deltaTime);

            if(canShoot && distance < 10f)
            {
                StartCoroutine(nameof(ShootingTimer));
            }
        }
        IEnumerator ShootingTimer()
        {
            canShoot = false;
            isShooting = true;

            Shooting();
            Debug.Log("dede");
            yield return new WaitForSeconds(movementDelay);
            isShooting = false;

            yield return new WaitForSeconds(shootingDelay);
            canShoot = true;
        }
        private void Shooting()
        {
            var bullet = BulletEnemyPoolManager.Instance.GetPooledObject();
            if(bullet != null)
            {
                bullet.transform.position = bulletPoint.position;
                bullet.GetComponent<BulletEnemy>().ShootThis(Fire);
                bullet.GetComponent<BulletEnemy>().SetDamage(GivenDamage);
            }

            void Fire()
            {
                bullet.transform.Translate(direction * bulletSpeed * Time.deltaTime);
            }
        }
    }
}
