using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public interface IDropableEnemy
    {
        public void DropMoney(float chanceOfHigherMoney);
        public void DropXP(float chanceOfHigherXP);
    }
}
