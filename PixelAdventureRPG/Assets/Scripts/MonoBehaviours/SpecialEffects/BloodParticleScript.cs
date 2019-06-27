using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleScript : MonoBehaviour
{
    private ParticleSystem system;

    private void OnEnable()
    {
        if (!system)
            system = GetComponent<ParticleSystem>();
        StartCoroutine(DisableSystem());
    }

    private IEnumerator DisableSystem()
    {
        yield return new WaitForSeconds(system.main.startLifetimeMultiplier);
        this.gameObject.SetActive(false);
    }
}
