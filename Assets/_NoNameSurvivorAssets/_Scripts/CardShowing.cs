using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FikretGezer
{
    public class CardShowing : MonoBehaviour
    {
        public ItemType _gunOrItem;

        public GunTypeScriptable _gunType;
        public ItemScriptable _itemType;

        private Image _cardImage;

        [Header("Gun Parameters")]
        [SerializeField] private TMP_Text _textName;
        [SerializeField] private TMP_Text _textDamage;
        [SerializeField] private TMP_Text _textCoolDown;
        private void Awake()
        {
            _cardImage = GetComponent<Image>();
            if (_gunOrItem == ItemType.gun)
            {
                _cardImage.sprite = _gunType.gunImage;
                _textName.text = $"{_gunType.gunName}s";
                _textDamage.text = $"Damage: {_gunType.damageAmount}s";
                _textCoolDown.text = $"CoolDown: {_gunType.coolDown}s";
            }
            else if (_gunOrItem == ItemType.item)
            {

            }
        }
    }
}
