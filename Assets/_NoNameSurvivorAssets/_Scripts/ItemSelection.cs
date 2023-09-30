using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FikretGezer
{
    public class ItemSelection : MonoBehaviour
    {
        private bool isCardsSelected;

        public List<GunTypeScriptable> guns = new List<GunTypeScriptable>();
        public List<GunTypeScriptable> gunsThatChanged = new List<GunTypeScriptable>();
        public int currentCharCount = 0;
        public int gunIndx = 0;
        public bool canBuyAGun;
        private bool addFeaturesToActiveGuns;

        public static ItemSelection Instance;
        private void Awake() {
            if(Instance == null) Instance = this;
            canBuyAGun = true;
        }
        private void Update() {
            if(isCardsSelected)
            {
                currentCharCount = CharacterSpawner.Instance._charactersOnTheScene.Count;
                RoundController.Instance.SpawnNewCharacters();
                isCardsSelected = false;
            }
            if(addFeaturesToActiveGuns)
            {
                var activeGuns = FindObjectsOfType<GunShooter>();

                var damage = Stats.Instance.damage - Stats.Instance.prev_damage;
                var attackSpeed = Stats.Instance.attackSpeed - Stats.Instance.prev_attackSpeed;

                Stats.Instance.prev_damage = Stats.Instance.damage;
                Stats.Instance.prev_attackSpeed = Stats.Instance.attackSpeed;

                Debug.Log(activeGuns);
                Debug.Log("asddsa");

                foreach (var gun in activeGuns)
                {
                    gun.FeatureMultiplier(damage, attackSpeed);
                }
                addFeaturesToActiveGuns = false;
            }
        }
        public void SelectTheCard(Button item){
            //Select a gun and add a new characters with that gun
            ItemType t = item.GetComponent<CardShowing>()._gunOrItem;
            if(t == ItemType.gun)
            {
                if(CharacterSpawner.Instance._changeableCharacterCount < 6){
                    CharacterSpawner.Instance._changeableCharacterCount++;

                    var itm = item.GetComponent<CardShowing>()._gunType;
                    guns.Add(itm);

                    //Stats.Instance.SetValue("damage", itm.damageAmount);

                    isCardsSelected = true;
                    item.gameObject.SetActive(false);
                }
                else
                {
                    canBuyAGun = false;
                    //Appears a pop-up message that player cant buy a gun, because there are already 6 guns selected
                }
            }
            //Selects an item, adds item features to stats
            else if(t == ItemType.item)
            {
                var itm = item.GetComponent<CardShowing>()._itemType;
                int maxHp = itm.itemHP;
                int damage = itm.itemDamage;
                int speed = itm.itemSpeed;
                int attackSpeed = itm.itemAttackSpeed;
                int luck = itm.itemLuck;
                int armor = itm.itemArmor;

                Stats.Instance.SetValue("maxHp", maxHp);
                Stats.Instance.SetValue("damage", damage);
                Stats.Instance.SetValue("speed", speed);
                Stats.Instance.SetValue("attackSpeed", attackSpeed);
                Stats.Instance.SetValue("luck", luck);
                Stats.Instance.SetValue("armor", armor);

                if(guns.Count > 0)
                {
                    foreach (var gun in guns)
                    {
                        if (!gunsThatChanged.Contains(gun))
                        {
                            gunsThatChanged.Add(gun);
                        }
                    }
                    foreach (var gun in gunsThatChanged)
                    {
                        gun.damageAmount += damage;
                    }
                    for (int i = gunIndx; i < gunsThatChanged.Count; i++)
                    {
                        gunsThatChanged[0].damageAmount += Stats.Instance.damage - Stats.Instance.prev_damage;
                        gunsThatChanged[0].coolDown -= gunsThatChanged[0].coolDown * (Stats.Instance.attackSpeed - Stats.Instance.prev_attackSpeed) / 100f;

                        Stats.Instance.prev_damage = Stats.Instance.damage;
                        Stats.Instance.prev_attackSpeed = Stats.Instance.attackSpeed;

                        gunIndx++;
                    }
                }
                addFeaturesToActiveGuns = true;
                item.gameObject.SetActive(false);
            }
        }
        
        public void ScaleUpWhenHovering(Button _button){
            _button.transform.localScale = new Vector2(1.1f, 1.1f);
        }
        public void ScaleUpWhenExitHovering(Button _button){
            _button.transform.localScale = new Vector2(1f, 1f);
        }
    }
    public enum ItemType
    {
        gun,
        item
    }
}