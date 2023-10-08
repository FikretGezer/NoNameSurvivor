using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace FikretGezer
{
    public class RevolverGuy : EnemyBase
    {
        [Header("Shooting Parameters")]
        [SerializeField] private float movementDelay = 1f;
        [SerializeField] private float shootingDelay = 1f;
        [SerializeField] private float bulletSpeed = 1f;
        [SerializeField] private Transform bulletPoint;
        [SerializeField] private Transform gun;
        [SerializeField] private GameObject _vfxMuzzle;

        private Vector3 direction;
        private float distance;
        private bool canShoot;
        private bool isShooting;

        private Rigidbody _rb;

        private void OnEnable() {
            canShoot = true;
        }
        private void Start() {
            _rb = GetComponent<Rigidbody>();
        }
        public override void Attack()
        {
            if(isShooting)
                return;
            if(FindObjectOfType<PlayerController>())
            {
                direction = CharacterSpawner.Instance._position - transform.position;
                distance = direction.magnitude;

                //transform.Translate(direction * speed * Time.deltaTime);
                transform.LookAt(CharacterSpawner.Instance._position);


                //Rotate Enemy
                    _rb.velocity = direction * speed * Time.deltaTime;
                    
                //

                if(canShoot && distance < 20f)
                {
                    StartCoroutine(nameof(ShootingTimer));
                }
            }
        }
        public override void Die()
        {
            base.Die();
            EnemySpawnController.Instance.selectedEnemies.Remove(gameObject);
            HardEnemyPoolManager.Instance.ReturnToThePool(gameObject);
        }
        IEnumerator ShootingTimer()
        {
            canShoot = false;
            isShooting = true;
            _rb.velocity = Vector3.zero;

            Shooting();
            yield return new WaitForSeconds(movementDelay);
            isShooting = false;

            yield return new WaitForSeconds(shootingDelay);
            canShoot = true;
        }
        private void Shooting()
        {
            var bullet = BulletEnemyPoolManager.Instance.GetPooledObject();
            _vfxMuzzle.GetComponent<VisualEffect>().Play();
            if (bullet != null)
            {
                bullet.transform.position = bulletPoint.position;
                var dir = (CharacterSpawner.Instance._position - gun.position).normalized;
                bullet.GetComponent<BulletEnemy>().direction = dir;
                bullet.GetComponent<BulletEnemy>().bulletSpeed = bulletSpeed;
                bullet.GetComponent<BulletEnemy>().SetDamage(GivenDamage);
            }

        }
    }
}
