using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FikretGezer
{
    public class ItemSelection : MonoBehaviour
    {
        private bool isCardsSelected;

        public List<GunTypeScriptable> guns = new List<GunTypeScriptable>();
        public int currentCharCount = 0;

        public static ItemSelection Instance;
        private void Awake() {
            if(Instance == null) Instance = this;
        }
        private void Update() {
            if(isCardsSelected)
            {
                currentCharCount = CharacterSpawner.Instance._charactersOnTheScene.Count;
                RoundController.Instance.SpawnNewCharacters();
                isCardsSelected = false;
            }
        }
        public void SelectTheCard(Button item){
            //Select a gun and add a new characters with that gun
            if(/*CharacterSpawner.Instance._charactersOnTheScene.Count < 6 && */CharacterSpawner.Instance._changeableCharacterCount < 6){
                CharacterSpawner.Instance._changeableCharacterCount++;
                guns.Add(item.GetComponent<CardShowing>()._gunType);
                isCardsSelected = true;
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
}