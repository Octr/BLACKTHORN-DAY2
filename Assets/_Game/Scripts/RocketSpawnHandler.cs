using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RocketSpawnHandler : MonoBehaviour
{
    [SerializeField] private float _coolDownTime = 3f;

    private InputReader _input;
    private RocketSpawner _rocketSpawner;
    private DecalProjector _reticleProjector;
    private Animator _animator;

    private bool _canFire = true;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
        _animator = GetComponent<Animator>();
        _rocketSpawner = GetComponentInChildren<RocketSpawner>();
        _reticleProjector = GetComponentInChildren<DecalProjector>();
    }

    private void Update()
    {
        if(_input.FireInput && _canFire == true) //Prevent rockets from spawning during cooldown with _canFire
        {
            _rocketSpawner.Spawn();
            _animator.SetTrigger("Fire");
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        _canFire = false;
        yield return new WaitForSeconds(_coolDownTime);
        _canFire = true;
    }
}
