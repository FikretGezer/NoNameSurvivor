using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FikretGezer
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] private TMP_Text text_MaxHp;
        [SerializeField] private TMP_Text text_Damage;
        [SerializeField] private TMP_Text text_Speed;
        [SerializeField] private TMP_Text text_Luck;
        [SerializeField] private TMP_Text text_AttackSpeed;
        [SerializeField] private TMP_Text text_Armor;

        [HideInInspector] public int maxHp;
        [HideInInspector] public int damage;
        [HideInInspector] public int speed;
        [HideInInspector] public int luck;
        [HideInInspector] public int attackSpeed;
        [HideInInspector] public int armor;

        [HideInInspector] public int prev_damage;
        [HideInInspector] public int prev_attackSpeed;

        public static Stats Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            maxHp = 10;
            speed = 5;
            SetValue(nameof(maxHp), 0);
            SetValue(nameof(speed), 0);
        }
        public void SetValue(string valueToSet, int value)
        {
            switch (valueToSet)
            {
                case nameof(maxHp):
                    if(maxHp + value >= 0)
                        maxHp += value;
                    else
                        maxHp = 0;

                    text_MaxHp.text = maxHp.ToString();
                    break;

                case nameof(damage):
                    if (damage + value >= 0)
                        damage += value;
                    else
                        damage = 0;
                    text_Damage.text = damage.ToString();
                    break;

                case nameof(speed):
                    if (speed + value >= 20)
                        speed = 20;
                    else if (speed + value < 5)
                        speed = 5;
                    else
                        speed += value;
                    text_Speed.text = speed.ToString();
                    break;

                case nameof(attackSpeed):
                    if (attackSpeed + value > 50)
                        attackSpeed = 50;
                    else if (attackSpeed + value < 0)
                        attackSpeed = 0;
                    else
                        attackSpeed += value;

                    text_AttackSpeed.text = attackSpeed.ToString();
                    break;

                case nameof(luck):
                    luck += value;
                    text_Luck.text = luck.ToString();
                    break;

                case nameof(armor):
                    armor += value;
                    text_Armor.text = armor.ToString();
                    break;

                default: break;
            }
        }
    }
}
