using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class CharacterSpawner : MonoBehaviour
    {
        private const float TAU = 6.28318530718f;

        [SerializeField] [Range(1, 6)] private int _characterCount = 3;
        [SerializeField] private float _distanceBetweenCharacters = 1f;
        [SerializeField] private GameObject _prefabCharacter;
        [HideInInspector] public Vector3 _position;


        private List<GameObject> _charactersOnTheScene = new List<GameObject>();
        private int _currentCharacterCount = 0;
        private bool newCharacterEquipped = false;
        private GameObject charactersParent;
        public static CharacterSpawner Instance;
        
        private void Awake() {
            if(Instance == null) Instance = this;
            charactersParent = new GameObject();
            charactersParent.name = "Characters' Parent";
        }
        private void Start() {
            _position = Vector3.zero;
            Spawner();
        }
        private void OnEnable() {
            if(newCharacterEquipped)
            {
                _position = Vector3.zero;
                if(_characterCount < 6) {
                    _characterCount++;
                    Spawner();
                }
                newCharacterEquipped = false;
            }
        }
        private void OnDisable() {
            newCharacterEquipped = true;
        }
        private void Update() {
            Vector3 total = Vector3.zero;
            foreach (var item in _charactersOnTheScene)
            {
                total += item.transform.position;
            }
            _position = total / _characterCount;
        }
        private void Spawner()
        {
            for (int i = _currentCharacterCount; i < _characterCount; i++)
            {
                var character = Instantiate(_prefabCharacter);
                character.name = "Character_" + i.ToString();
                _charactersOnTheScene.Add(character);
                character.transform.parent = charactersParent.transform;
            }
            _currentCharacterCount = _characterCount;
            
            for (int i = 0; i < _characterCount; i++)
            {
                _charactersOnTheScene[i].transform.position = _position + ReturnPositionOfACharacter(i);
            }
        }
        private Vector3 ReturnPositionOfACharacter(int i)
        {
            float d = i * TAU / _characterCount;
            float x = Mathf.Cos(d);
            float y = Mathf.Sin(d);
            return new Vector3(x, 0f, y) * _distanceBetweenCharacters + new Vector3(0f, 1f);
        }
        private void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_position, 0.3f);

            Gizmos.color = Color.cyan;
            for (int i = 0; i < _characterCount; i++)
            {
                Gizmos.DrawSphere(_position + ReturnPositionOfACharacter(i), 0.3f);
            }
        }
    }
}
