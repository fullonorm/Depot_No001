using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JYH_PlayEffect : MonoBehaviour
{
    public ParticleSystem particlePrefab;
    public bool playPraticle = false;
    public Transform tf;

    private void Update()
    {
        if(playPraticle)
        {
            PlayParticle();
            playPraticle = false;
        }
    }

    public void PlayParticle()
    {
        ParticleSystem particleSystem = Instantiate(particlePrefab, tf.position, tf.rotation) as ParticleSystem;
        particleSystem.Play();
    }
}
