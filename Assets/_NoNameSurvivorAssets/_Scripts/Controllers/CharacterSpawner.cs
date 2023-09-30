using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [HideInInspector] public List<GameObject> _charactersOnTheScene = new List<GameObject>();
        public int _changeableCharacterCount;

        private int _currentCharacterCount = 0;
        private bool newCharacterEquipped;
        private bool equipNewGuns;
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
                if(_characterCount < 6 && _changeableCharacterCount > _characterCount) {
                    _characterCount = _changeableCharacterCount;
                    Spawner();
                }
                newCharacterEquipped = false;
            }
        }
        private void OnDisable() {
            newCharacterEquipped = true;
        }
        private void Update() {
            //Equips new guns 
            if(equipNewGuns)
            {
                if(ItemSelection.Instance.guns.Count > 0)
                {
                    for (int i = 0; i < ItemSelection.Instance.guns.Count; i++)
                    {
                        var gun = ItemSelection.Instance.guns[i];
                        _charactersOnTheScene[i].transform.GetChild(2).GetComponent<GunShooter>().SetGunParameters(gun.coolDown, gun.damageAmount, gun.gunMesh, gun.gunColor);
                    }
                    ItemSelection.Instance.guns.Clear();
                    ItemSelection.Instance.gunsThatChanged.Clear();
                    ItemSelection.Instance.gunIndx = 0;
                }
                equipNewGuns = false;
            }
            //Character Position Adjuster
            Vector3 total = Vector3.zero;
            foreach (var _char in _charactersOnTheScene)
            {
                total += _char.transform.position;
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
            _changeableCharacterCount = _currentCharacterCount = _characterCount;
            
            //Add new guns
            equipNewGuns = true;
            //

            for (int i = 0; i < _characterCount; i++)
            {
                _charactersOnTheScene[i].transform.position = _position + ReturnPositionOfSpawnedCharacter(i);
            }
        }
        private Vector3 ReturnPositionOfSpawnedCharacter(int i)
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
                Gizmos.DrawSphere(_position + ReturnPositionOfSpawnedCharacter(i), 0.3f);
            }
        }
        public void DisableCharacters()
        {            
            foreach (var _char in _charactersOnTheScene)
            {
                _char.SetActive(false);
            }
        }
    }
}
