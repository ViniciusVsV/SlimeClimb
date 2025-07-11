using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlesPrefab;
    private ParticleSystem particles;

    public void PlayParticles(Vector3 position, Vector3 scale)
    {
        particles = Instantiate(particlesPrefab, position, Quaternion.identity);
        particles.transform.localScale = scale;

        particles.Play();

        Destroy(particles.gameObject, 2f);
    }
}