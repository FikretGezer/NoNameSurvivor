using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FikretGezer
{
    public class CardShowing : MonoBehaviour
    {
        public ItemType _gunOrItem { get; set; }

        [field: SerializeField] public GunTypeScriptable Gun { get; set; }
        [field: SerializeField] public ItemScriptable Item { get; set; }
        [field: SerializeField] public int Cost { get; set; }
        //public CardTypeScriptable _card;

        [Header("Childs")]
        [SerializeField] private GameObject gunTexts;
        [SerializeField] private GameObject itemTexts;

        [Header("General Parameters")]
        [SerializeField] private Image _cardImage;
        [SerializeField] private TMP_Text _itemCost;

        [Header("Gun Parameters")]
        [SerializeField] private TMP_Text _textName;
        [SerializeField] private TMP_Text _textDamage;
        [SerializeField] private TMP_Text _textCoolDown;

        [Header("Item Properties")]
        [SerializeField] private TMP_Text _itemName;
        [SerializeField] private TMP_Text _itemHp;
        [SerializeField] private TMP_Text _itemDamage;
        [SerializeField] private TMP_Text _itemSpeed;
        [SerializeField] private TMP_Text _itemAttackSpeed;

        private void OnEnable()
        {
            AssignACard();
        }
        public void SetGunProperties(Sprite gunImage, string gunName, int damageAmount, float cooldown, int gunCost)
        {
            //_itemName.gameObject.SetActive(false);
            //_itemHp.gameObject.SetActive(false);
            //_itemDamage.gameObject.SetActive(false);
            //_itemSpeed.gameObject.SetActive(false);
            //_itemAttackSpeed.gameObject.SetActive(false);

            itemTexts.SetActive(false);
            gunTexts.SetActive(true);

            _cardImage.sprite = gunImage;
            _textName.text = $"{gunName}";
            _textDamage.text = $"{damageAmount}";
            _textCoolDown.text = $"{cooldown}s";
            _itemCost.text = $"{gunCost}";
            Cost = gunCost;
        }
        public void SetItemProperties(Sprite itemImage, string itemName, int itemHp, int itemDamage, int itemSpeed, int itemAttackSpeed, int itemCost)
        {
            //_textName.gameObject.SetActive(false);
            //_textDamage.gameObject.SetActive(false);
            //_textCoolDown.gameObject.SetActive(false);

            gunTexts.SetActive(false);
            itemTexts.SetActive(true);

            _cardImage.sprite = itemImage;
            _itemName.text = $"{itemName}";
            _itemHp.text = $"{itemHp}";
            _itemDamage.text = $"{itemDamage}";
            _itemSpeed.text = $"{itemSpeed}";
            _itemAttackSpeed.text = $"{itemAttackSpeed}";
            _itemCost.text = $"{itemCost}";
            Cost = itemCost;
        }
        
        public void AssignACard()
        {
            int rnd = Random.Range(0, 2);
            if (rnd == 0)//if rnd returns guns
            {
                GunTypeScriptable currentGun;
                rnd = Random.Range(0, ItemsHolder.Instance.gunsScr.Count);
                currentGun = ItemsHolder.Instance.gunsScr[rnd];
                while (ItemsHolder.Instance.selectedOnes.Contains(currentGun))
                {
                    rnd = Random.Range(0, ItemsHolder.Instance.gunsScr.Count);
                    currentGun = ItemsHolder.Instance.gunsScr[rnd];
                }
                ItemsHolder.Instance.selectedOnes.Add(currentGun);
                _gunOrItem = currentGun.cardType;
                Gun = currentGun;

                if(Gun != null)
                    SetGunProperties(Gun.gunImage, Gun.gunName, Gun.damageAmount, Gun.cooldown, Gun.gunCost);
            }
            else
            {
                ItemScriptable currentItem;
                rnd = Random.Range(0, ItemsHolder.Instance.itemsScr.Count);
                currentItem = ItemsHolder.Instance.itemsScr[rnd];
                while (ItemsHolder.Instance.selectedOnes.Contains(currentItem))
                {
                    rnd = Random.Range(0, ItemsHolder.Instance.itemsScr.Count);
                    currentItem = ItemsHolder.Instance.itemsScr[rnd];
                }
                ItemsHolder.Instance.selectedOnes.Add(currentItem);

                _gunOrItem = currentItem.cardType;
                Item = currentItem;

                if(Item != null)                
                    SetItemProperties(Item.itemImage, Item.itemName, Item.itemHP, Item.itemDamage, Item.itemSpeed, Item.itemAttackSpeed, Item.itemCost);                                  
            }
        }
    }
}
