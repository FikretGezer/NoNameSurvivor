using UnityEngine;

namespace FikretGezer
{
    public class XpItem : MonoBehaviour
    {
        [SerializeField] private Transform _groundObject;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private float _radius = 0.1f;
        [SerializeField] private float flySpeed = 12f;
        [SerializeField] private float distanceBetweenPlayer = 2.5f;

        [HideInInspector] public int xpAmount = 1;

        private Rigidbody _rb;
        private bool isFlying;
        private GameObject selectedChar;
        private void Awake() {
            _rb = GetComponent<Rigidbody>();               
        }
        private void OnEnable() {
            _rb.velocity = Vector3.zero;    
            _rb.constraints = RigidbodyConstraints.None;
            isFlying = false;        
        }
        private void Update() {
            bool isOnTheFloor = Physics.CheckSphere(_groundObject.position, _radius, _layer);     

            if(isOnTheFloor)
            {
                _rb.constraints = RigidbodyConstraints.FreezePositionY;
            }
             if(CalculateDistance(transform.position, CharacterSpawner.Instance._position) < distanceBetweenPlayer)
            {
                isFlying = true;
            }
            if(isFlying)
            {
                if(selectedChar == null)
                {
                    selectedChar = CharacterSpawner.Instance._charactersOnTheScene[Random.Range(0, CharacterSpawner.Instance._charactersOnTheScene.Count)];
                    //SelectMagnetedChar();
                }
                Vector3 dir = CalculateDirection(transform.position, selectedChar.transform.position).normalized;
                transform.Translate(dir * flySpeed * Time.deltaTime);
            }
        }
        private void SelectMagnetedChar()
        {
            float minDistance = 1000f;
            int selectedCharIndex = 0;
            if(CharacterSpawner.Instance._charactersOnTheScene.Count > 1)
            {
                foreach (var _char in CharacterSpawner.Instance._charactersOnTheScene)
                {
                    var dist = CalculateDistance(transform.position, _char.transform.position);
                    if(dist < minDistance)
                    {
                        minDistance = dist;
                        selectedCharIndex = CharacterSpawner.Instance._charactersOnTheScene.IndexOf(_char);
                    }
                }
                selectedChar = CharacterSpawner.Instance._charactersOnTheScene[selectedCharIndex];
            }
            else
                selectedChar = CharacterSpawner.Instance._charactersOnTheScene[0];
        }
        private Vector3 CalculateDirection(Vector3 a, Vector3 b)
        {
            return b-a;
        }
        private float CalculateDistance(Vector3 a, Vector3 b)
        {
            return CalculateDirection(a, b).magnitude;
        }
        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                ExperienceManager.Instance.UpdateExperience(xpAmount);

                var xpText = XPTextPoolManager.Instance.GetPooledObject();
                xpText.GetComponent<xpText>().XPAmount = $"x{xpAmount}";
                xpText.GetComponent<xpText>().StartPos = transform.position + Vector3.up;
                xpText.GetComponent<xpText>().CanMove = true;

                selectedChar = null;
                gameObject.SetActive(false);
            }            
        }
    }
}
