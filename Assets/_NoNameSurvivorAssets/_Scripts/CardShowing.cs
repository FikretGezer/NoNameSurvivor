using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FikretGezer
{
    public class CardShowing : MonoBehaviour
    {
        [SerializeField] private GunTypeScriptable _gunType;
        [SerializeField] private TMP_Text _textCoolDown;
        [SerializeField] private TMP_Text _textDamage;
        [SerializeField] private Image _gunImage;
        private void Awake() {
            _textCoolDown.text = $"{_gunType.coolDown}s";
            _textDamage.text = _gunType.damageAmount.ToString();
            _gunImage.sprite = _gunType.gunImage;
        }
    }
}
