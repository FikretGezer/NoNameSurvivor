using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public abstract class EnemyBase : MonoBehaviour, IDamageable, IDropableEnemy
    {
        [field:Header("IDamageable Parameters")]
        [field:SerializeField] public float Health {get;set;}
        [field:SerializeField] public float Speed {get; set;}
        [field:SerializeField] public float GivingDamage {get; set;}

        [field:Header("IDamageable Parameters")]
        [field:SerializeField] public GameObject moneyItem {get; set;}
        [field:SerializeField] public GameObject xpItem {get; set;}

        [SerializeField] protected float attackCoolDown;
        [SerializeField] protected float chanceOfHigherXP;
        [SerializeField] protected float chanceOfHigherMoney;

        protected bool canGiveDamage;
        private const float TAU = 6.28318530718f;
        private void Awake() {
            canGiveDamage = true;
        }
        public virtual void Update() {
            var dir = CharacterSpawner.Instance._position - transform.position;
            transform.Translate(dir * Speed * Time.deltaTime);
        }
        public void TakeDamage(float GivingDamage)
        {
            Health -= GivingDamage;
            if(Health <= 0)
                Die();
        }
        public void Die()
        {
            DropMoney(chanceOfHigherMoney);
            DropXP(chanceOfHigherXP);
            EnemySpawnController.Instance.selectedEnemies.Remove(gameObject);
            gameObject.SetActive(false);
        }
        
        public void DropMoney(float chanceOfHigherMoney) // 2 money item
        {
            int dropCount = Random.Range(1, 3);
            for (int i = 0; i < dropCount; i++)
            {
                Vector3 dropPos = GetItemDropPoint(transform.position);
                Instantiate(moneyItem, dropPos, Quaternion.identity);                
            }
        }
        public void DropXP(float chanceOfHigherXP) // 2 xp item
        {
            int dropCount = Random.Range(1, 4);
            for (int i = 0; i < dropCount; i++)
            {
                Vector3 dropPos = GetItemDropPoint(transform.position);
                Instantiate(xpItem, dropPos, Quaternion.identity);                
            }

        }
        Vector3 GetItemDropPoint(Vector3 enemyPos)
        {
            float angleRad = Random.Range(0, 1f);
            angleRad *= TAU;
            return enemyPos + new Vector3(Mathf.Cos(angleRad), 0f, Mathf.Sin(angleRad)) * 2f;
        }
    }
}
