using UnityEngine;

namespace FikretGezer
{
    public class XpItem : MonoBehaviour
    {
        [SerializeField] private Transform _groundObject;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private float _radius = 0.1f;
        [HideInInspector] public int xpAmount = 1;

        private Rigidbody _rb;
        private void Awake() {
            _rb = GetComponent<Rigidbody>();               
        }
        private void OnEnable() {
            _rb.velocity = Vector3.zero;    
            _rb.constraints = RigidbodyConstraints.None;        
        }
        private void Update() {
            bool isOnTheFloor = Physics.CheckSphere(_groundObject.position, _radius, _layer);     

            if(isOnTheFloor)
            {
                _rb.constraints = RigidbodyConstraints.FreezePositionY;
            }
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

                gameObject.SetActive(false);
            }            
        }
    }
}
