using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FikretGezer
{
    public abstract class EnemyBase : MonoBehaviour, IDamageable
    {
        [field:SerializeField] public float Health {get;set;}
        [field:SerializeField] public float Speed {get; set;}
        [field:SerializeField] public float GivingDamage {get; set;}
        protected bool canGiveDamage;
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
            EnemySpawnController.Instance.selectedEnemies.Remove(gameObject);
            gameObject.SetActive(false);
        }
        
    }
}
