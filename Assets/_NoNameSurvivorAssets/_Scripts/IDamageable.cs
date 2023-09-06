using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public interface IDamageable
    {
        public float Health { get; set; }
        public void TakeDamage(float GivingDamage);
        public void Die();
    }
}
