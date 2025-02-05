using UnityEngine;

public class bulletEffect : MonoBehaviour
{
    public ParticleSystem trailEffect;
    void Start()
    {
               if (trailEffect == null)
        {
            Debug.LogError("Trail Effect (Particle System) is not assigned in the Inspector!");
            return;
        }

               trailEffect.Play();
        Debug.Log("Particle system started!XXXXXXXXXXXXXXXXX");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (trailEffect != null)
        {
                       trailEffect.transform.parent = null;            trailEffect.Stop();            Debug.Log("Particle system stopped on collision!");

                       Destroy(trailEffect.gameObject, trailEffect.main.duration);
        }

              
    }
}
