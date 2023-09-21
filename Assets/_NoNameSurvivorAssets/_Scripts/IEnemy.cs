using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public interface IEnemy
    {
        public int GivenDamage {get;set;} //Every attack gives different damages
        public void Attack();//Every enemy has different attack
    }
}
