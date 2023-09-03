using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public abstract class EnemyBase : MonoBehaviour, IDamageable
    {
        [field:SerializeField] public float Health {get;set;}
        [field:SerializeField] public float Speed => 0.5f;
        public virtual void Update() {
            var dir = CharacterSpawner.Instance._position - transform.position;
            transform.Translate(dir * Speed * Time.deltaTime);
        }
        public void TakeDamage()
        {
            Health -= 10f;
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
