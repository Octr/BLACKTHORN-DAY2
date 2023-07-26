using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [Tooltip("The max distance the rocket can travel before selfdestruct occurs. Stops the game from overloading on rocket spawns")]
    [SerializeField] private float _maxDistance = 50f;
    [Tooltip("The layer in which rockets will collide and explode on. Default is 'Default'.")]
    [SerializeField] private LayerMask _collisionLayer;

    [Header("Explosion Effect")]
    [SerializeField] private GameObject _explosionVFXPrefab;
    [Tooltip("_particleGameObjectHolder starts off empty but is found during game play when the any object with this script is created. Very jank, do not copy")]
    [SerializeField] private GameObject _particleGameObjectHolder;

    private CinemachineImpulseSource _explosionImpulseSource;

    private Vector3 _startingPosition;
    private float _currentDistance;

    private void Awake()
    {
        _explosionImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Find the object responsible for holding all generated particles from rockets. For organisation of heirarchy
        _particleGameObjectHolder = GameObject.Find("PARTICLE OBJECTS");

        _startingPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _currentDistance = CalculateCurrentDistance(this.transform.position);

        if(_currentDistance >= _maxDistance)
        {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Someone make sure that I'm checking collision layers properly. Seems to work at the moment
        if (_collisionLayer.value >= (1 << collision.gameObject.layer))
        {
            Explode();
        }
    }

    private float CalculateCurrentDistance(Vector3 currentPosition)
    {
        return Mathf.Abs(Vector3.Distance(_startingPosition, currentPosition));
    }

    private void Explode()
    {
        GameObject explosionParticlesGameObject = Instantiate(_explosionVFXPrefab, this.transform.position, Quaternion.identity, _particleGameObjectHolder.transform);
        _explosionImpulseSource.GenerateImpulse();
        Destroy(this.gameObject);
    }
}
