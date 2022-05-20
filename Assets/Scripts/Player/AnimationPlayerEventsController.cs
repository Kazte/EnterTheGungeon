using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerEventsController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _stepParticle;

    
    
    public void StepEffectParticle()
    {
        _stepParticle.Play();
    }
}
