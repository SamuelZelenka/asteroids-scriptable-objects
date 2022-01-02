using DefaultNamespace.ScriptableEvents;
using UnityEngine;
using Variables;

namespace Asteroids
{    
    [RequireComponent(typeof(Asteroid))]
    public class AsteroidDestroyer : MonoBehaviour
    {
        [SerializeField] private ScriptableEventInt _onAsteroidDestroyed;
        [SerializeField] private IntVariable _spawnCount;
        [SerializeField] private FloatVariable _reductionRate;
        [SerializeField] private Asteroid _asteroidPrefab;

        private Asteroid _thisAsteroid;

        private void Awake()
        {
            _thisAsteroid = GetComponent<Asteroid>();
            _onAsteroidDestroyed.Register(OnAsteroidHitByLaser);
        }
        private void OnDisable()
        {
            _onAsteroidDestroyed.Unregister(OnAsteroidHitByLaser);
        }

        public void OnAsteroidHitByLaser(int asteroidId)
        {
            if (asteroidId == _thisAsteroid.GetInstanceID())
            {
                _onAsteroidDestroyed.Raise();
                if (!CanSpawnSmallerAsteroids())
                {
                    Destroy(gameObject);
                    return;
                }
                InstantiateSmallerAsteroids(_spawnCount.Value);
            }
        }
        private bool CanSpawnSmallerAsteroids() => _thisAsteroid.size > _thisAsteroid.minSize * ( 1f / _reductionRate.Value );

        private void InstantiateSmallerAsteroids(int count)
        {
            if (count > 0)
            {
                float newAsteroidSize = _thisAsteroid.size * _reductionRate.Value;

                _thisAsteroid.SetSize(newAsteroidSize);
                for (int i = 1; i < count; i++)
                {
                    Asteroid newAsteroid = Instantiate(_asteroidPrefab, transform.position, Quaternion.identity);
                    newAsteroid.SetSize(newAsteroidSize);
                }
            }
        }
    }
}
