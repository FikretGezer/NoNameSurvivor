using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public abstract class EnemyBase : MonoBehaviour, IDamageable
    {
        [field:SerializeField] public float Health {get;set;}
        public void TakeDamage()
        {
            Health -= 10f;
            if(Health <= 0)
                Die();
        }
        public void Die()
        {
            gameObject.SetActive(false);
        }
    }
}
