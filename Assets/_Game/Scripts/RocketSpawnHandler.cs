using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RocketSpawnHandler : Singleton<RocketSpawnHandler>
{
    public float CoolDownTime = 1f;

    private InputReader _input;
    private RocketSpawner _rocketSpawner;
    private DecalProjector _reticleProjector;
    private Animator _animator;

    private bool _canFire = true;

    protected override void Awake()
    {
        base.Awake();
        _input = GetComponent<InputReader>();
        _animator = GetComponent<Animator>();
        _rocketSpawner = GetComponentInChildren<RocketSpawner>();
        _reticleProjector = GetComponentInChildren<DecalProjector>();
    }

    private void Update()
    {
        // I hardcoded the mouse input here because of a bug
        if(Input.GetMouseButton(0) && _canFire == true) //Prevent rockets from spawning during cooldown with _canFire
        {
            _rocketSpawner.Spawn();
            _animator.SetTrigger("Fire");
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        _canFire = false;
        yield return new WaitForSeconds(CoolDownTime);
        _canFire = true;
    }
}
