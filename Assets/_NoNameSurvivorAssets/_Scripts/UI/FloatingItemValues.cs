using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FikretGezer
{
    public class FloatingItemValues : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float coroutineSpeed = 1f;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private AnimationCurve _curve;
        private bool move;
        private Color baseColor;

        public static FloatingItemValues Instance;
        private void Awake() {
            if(Instance == null) Instance = this;
        }
        private void Update()
        {
            if(Input.GetMouseButtonDown(1))
            {
                move = true;
            }
            
            if(move)
                SpawnItemText(_text, _text.rectTransform.position);
            
        }
        public void SpawnItemText(TMP_Text itemText, Vector3 startPos)
        {
            var time = moveSpeed * Time.deltaTime;
            var pos = startPos;
            pos.y = Mathf.MoveTowards(pos.y, 4f, _curve.Evaluate(time));
            itemText.rectTransform.position = pos;
            itemText.color = new Color(itemText.color.r, itemText.color.g, itemText.color.b, Mathf.MoveTowards(itemText.color.a, 0f, time));

            if(pos.y >= 4f)
            {
                //itemText.gameObject.SetActive(false);
                MoneyTextPoolManager.Instance.ReturnToThePool(itemText.gameObject);
                move = false;
            }
        }
        public void StartCoro(TMP_Text itemText, Vector3 startPos)
        {
            StartCoroutine(SpawnText(itemText, startPos));
        }
        public IEnumerator SpawnText(TMP_Text itemText, Vector3 pos)
        {
            float elapsedTime = 0f;
            while(elapsedTime <= 1f)
            {
                elapsedTime += coroutineSpeed * Time.deltaTime;                
                 
                pos.y = Mathf.MoveTowards(pos.y, 4f, _curve.Evaluate(elapsedTime));
                itemText.rectTransform.position = pos;
                itemText.color = new Color(itemText.color.r, itemText.color.g, itemText.color.b, Mathf.MoveTowards(itemText.color.a, 0f, elapsedTime));

                if(pos.y >= 4f)
                {
                    //itemText.gameObject.SetActive(false);
                    MoneyTextPoolManager.Instance.ReturnToThePool(itemText.gameObject);
                    move = false;
                }
                yield return null;
            }
        }
    }
}
