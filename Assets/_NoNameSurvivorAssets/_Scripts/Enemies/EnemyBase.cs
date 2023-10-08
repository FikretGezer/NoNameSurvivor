using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public abstract class EnemyBase : MonoBehaviour, IDamageable, IDropableEnemy, IEnemy
    {
        [SerializeField] protected float attackCoolDown;
        [SerializeField] protected float chanceOfHigherXP;
        [SerializeField] protected float chanceOfHigherMoney;
        [SerializeField] protected float speed;

        public EnemyTypes _enemyType;
        public float spawnChances;

        [field:Header("IDamageable Parameters")]
        [field:SerializeField] public float Health {get;set;}

        [field:Header("IEnemy Parameters")]
        [field:SerializeField] public int GivenDamage {get;set;}


        protected bool canGiveDamage;
        private const float TAU = 6.28318530718f;
        private void Awake() {
            canGiveDamage = true;
        }
        public virtual void Update()
        {
            Attack();            
        }
        
        public abstract void Attack();
        public void TakeDamage(float GivingDamage)
        {
            Health -= GivingDamage;
            if(Health <= 0)
                Die();
        }
        public virtual void Die()
        {
            DropMoney(chanceOfHigherMoney);
            DropXP(chanceOfHigherXP);            
            //gameObject.SetActive(false);
        }
        
        public void DropMoney(float chanceOfHigherMoney) // 2 money item
        {
            var money = MoneyPoolManager.Instance.GetPooledObject();    

            Vector3 dropPos = GetItemDropPoint(transform.position);
            money.transform.position = dropPos;

            money.GetComponent<MoneyItem>().moneyAmount = Random.Range(1,3);
            //int dropCount = Random.Range(1, 3);
            // for (int i = 0; i < dropCount; i++)
            // {
                // Vector3 dropPos = GetItemDropPoint(transform.position);
                // MoneyPoolManager.Instance.GetPooledObject().transform.position = dropPos;    
            // }
        }
        public void DropXP(float chanceOfHigherXP) // 2 xp item // chance doens't applied yet but it will
        {
            var xp = XPPoolManager.Instance.GetPooledObject();

            Vector3 dropPos = GetItemDropPoint(transform.position);
            xp.transform.position = dropPos;

            xp.GetComponent<XpItem>().xpAmount = Random.Range(1,3);
            // int dropCount = Random.Range(1, 4);
            // for (int i = 0; i < dropCount; i++)
            // {
            //     Vector3 dropPos = GetItemDropPoint(transform.position);
            //     XPPoolManager.Instance.GetPooledObject().transform.position = dropPos;
            // }
        }
        Vector3 GetItemDropPoint(Vector3 enemyPos)
        {
            float angleRad = Random.Range(0, 1f);
            angleRad *= TAU;
            return enemyPos + new Vector3(Mathf.Cos(angleRad), 0f, Mathf.Sin(angleRad)) * 2f;
        }
    }
}
