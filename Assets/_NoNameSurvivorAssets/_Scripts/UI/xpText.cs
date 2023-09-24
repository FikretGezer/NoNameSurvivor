using UnityEngine;
using TMPro;

namespace FikretGezer
{
    public class xpText : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float moveSpeed = 1f;
        
        public bool CanMove {get; set;}
        public Vector3 StartPos {get; set;}
        public string XPAmount {get; set;}
        
        TMP_Text _xpAmount;
        private void Awake() {
            _xpAmount = GetComponent<TMP_Text>();
        }
        void Update()
        {
            if(CanMove)
            {
                _xpAmount.text = XPAmount;
                SpawnItemText(_xpAmount, StartPos);
            }
        }
        public void SpawnItemText(TMP_Text itemText, Vector3 pos)
        {
            var time = moveSpeed * Time.deltaTime;
            pos.y = Mathf.MoveTowards(pos.y, 4f, _curve.Evaluate(time));
            itemText.rectTransform.position = pos;
            itemText.color = new Color(itemText.color.r, itemText.color.g, itemText.color.b, Mathf.MoveTowards(itemText.color.a, 0f, time));

            if(pos.y >= 4f)
            {
                //itemText.gameObject.SetActive(false);
                MoneyTextPoolManager.Instance.ReturnToThePool(itemText.gameObject);
                CanMove = false;
            }
        }
    }
}
