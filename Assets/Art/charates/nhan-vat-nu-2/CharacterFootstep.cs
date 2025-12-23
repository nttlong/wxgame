using UnityEngine;

public class CharacterFootstep : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Surface Clips")]
    public AudioClip[] grassClips;
    public AudioClip[] woodClips;
    public AudioClip[] stoneClips;

    [Header("Raycast")]
    public Transform footRayOrigin;
    public float rayDistance = 1.2f;

    public void Footstep()
    {
        if (Physics.Raycast(footRayOrigin.position, Vector3.down, out RaycastHit hit, rayDistance))
        {
            string tag = hit.collider.tag;
            AudioClip clip = null;

            switch (tag)
            {
                case "Grass":
                    clip = grassClips[Random.Range(0, grassClips.Length)];
                    break;

                case "Wood":
                    clip = woodClips[Random.Range(0, woodClips.Length)];
                    break;

                case "Stone":
                    clip = stoneClips[Random.Range(0, stoneClips.Length)];
                    break;
            }

            if (clip != null)
                audioSource.PlayOneShot(clip);
        }
    }
}
