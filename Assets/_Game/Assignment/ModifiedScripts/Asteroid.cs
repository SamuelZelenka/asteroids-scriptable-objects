using DefaultNamespace.ScriptableEvents;
using UnityEngine;
using Variables;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D), typeof(AsteroidDestroyer))]
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private ScriptableEventInt _onAsteroidDestroyed;

        [Header("Config:")]
        [SerializeField] private float _minForce;
        [SerializeField] private float _maxForce;
        [SerializeField] private float _minSize;
        [SerializeField] private float _maxSize;
        [SerializeField] private float _minTorque;
        [SerializeField] private float _maxTorque;

        [Header("References:")]
        [SerializeField] private Transform _shape;

        private Rigidbody2D _rigidbody;
        private Vector3 _direction;

        public float minSize { get => _minSize; }
        public float size { get; private set; }

        public int instanceId { get; private set; }

        private void Awake() 
        {
            float randomSize = Random.Range(_minSize, _maxSize);

            _rigidbody = GetComponent<Rigidbody2D>();

            instanceId = gameObject.GetInstanceID();


            SetDirection();
            AddForce();
            AddTorque();
            SetSize(randomSize);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (string.Equals(other.tag, "Laser"))
            {
               Destroy(other.gameObject);
               HitByLaser();
            }
        }

        private void HitByLaser()
        {
            _onAsteroidDestroyed.Raise(GetInstanceID());
        }

        private void SetDirection()
        {
            var size = new Vector2(3f, 3f);
            var target = new Vector3
            (
                Random.Range(-size.x, size.x),
                Random.Range(-size.y, size.y)
            );

            _direction = (target - transform.position).normalized;
        }

        private void AddForce()
        {
            var force = Random.Range(_minForce, _maxForce);
            _rigidbody.AddForce( _direction * force, ForceMode2D.Impulse);
        }

        private void AddTorque()
        {
            var torque = Random.Range(_minTorque, _maxTorque);
            var roll = Random.Range(0, 2);

            if (roll == 0)
                torque = -torque;
            
            _rigidbody.AddTorque(torque, ForceMode2D.Impulse);
        }

        public void SetSize(float size)
        {
            this.size = size;
            _shape.localScale = new Vector3(this.size, this.size, 0f);
        }
    }
}
