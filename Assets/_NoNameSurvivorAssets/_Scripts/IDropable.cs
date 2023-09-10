using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public interface IDropableEnemy
    {
        public GameObject moneyItem {get; set;}
        public GameObject xpItem {get; set;}
        public void DropMoney(float chanceOfHigherMoney);
        public void DropXP(float chanceOfHigherXP);
    }
}
