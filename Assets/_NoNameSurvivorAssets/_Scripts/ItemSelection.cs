using UnityEngine;
using UnityEngine.UI;

namespace FikretGezer
{
    public class ItemSelection : MonoBehaviour
    {
        public void SelectTheCard(Button item){
            item.gameObject.SetActive(false);
        }
        public void ScaleUpWhenHovering(Button _button){
            _button.transform.localScale = new Vector2(1.1f, 1.1f);
        }
        public void ScaleUpWhenExitHovering(Button _button){
            _button.transform.localScale = new Vector2(1f, 1f);
        }
    }
}
