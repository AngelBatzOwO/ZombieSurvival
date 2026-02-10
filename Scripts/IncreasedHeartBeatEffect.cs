using UnityEngine;
public class IncreasedHeartBeatEffect : MonoBehaviour
{
    public Transform healthIcon; // Reference to the HealthIcon image game object
    public float normalDistance = 10f; // Distance where the heartbeat is normal
    public float dangerDistance = 5f; // Distance where the heartbeat is fastest
    public float increasedHeartbeatSpeed = 2f; // Heartbeat speed when a zombie is very close
    public AudioSource heartbeatAudio; // AudioSource for the heartbeat sound
    public float normalAudioPitch = 1f; // Pitch of the heartbeat at normal speed
    public float normalHeartbeatVolume = 0.2f; // Volume of the heartbeat at normal speed
    public float maxHeartbeatVolume = 1f; // Maximum volume of the heartbeat at max speed
    private HeartbeatScaleEffect heartbeatScaleEffect;
    private Transform playerTransform; // Reference to the player object
    private GameObject[] zombies; // Array to hold Zombie-tagged objects
    void Start()
    {
        // Get the HeartbeatScaleEffect script from the HealthIcon
    if (healthIcon != null)
        {
            heartbeatScaleEffect = healthIcon.GetComponent<HeartbeatScaleEffect>();
        }
        // Find the player's transform
        playerTransform = gameObject.transform;
        // Play the heartbeat audio on scene awaken
        if (heartbeatAudio != null)
        {
            heartbeatAudio.loop = true;
            heartbeatAudio.pitch = normalAudioPitch; // Start with normal pitch
            heartbeatAudio.volume = normalHeartbeatVolume; // Start with normal volume
            heartbeatAudio.Play();
        }
    }
    void Update()
    {
        // Find all Zombie-tagged objects
        zombies = GameObject.FindGameObjectsWithTag("Zombie");
        float nearestZombieDistance = float.MaxValue;
        // Calculate the distance to the closest Zombie
        foreach (var zombie in zombies)
        {
            float distance = Vector3.Distance(playerTransform.position,
            zombie.transform.position);
            if (distance < nearestZombieDistance)
        {
                nearestZombieDistance = distance;
            }
        }
        // Adjust the heartbeat speed, audio pitch, and volume based on proximity
        if (nearestZombieDistance <= dangerDistance)
        {
            heartbeatScaleEffect.currentHeartbeatSpeed = increasedHeartbeatSpeed;
        }
        else if (nearestZombieDistance <= normalDistance)
        {
            // Gradually scale the heartbeat speed based on proximity
            heartbeatScaleEffect.currentHeartbeatSpeed =
            Mathf.Lerp(heartbeatScaleEffect.normalHeartbeatSpeed, increasedHeartbeatSpeed,
            1f - ((nearestZombieDistance - dangerDistance) / (normalDistance -
            dangerDistance)));
        }
        else
        {
            // Return to normal heartbeat speed when no zombies are nearby
            heartbeatScaleEffect.currentHeartbeatSpeed =
            heartbeatScaleEffect.normalHeartbeatSpeed;
        }
        // Adjust audio properties based on the current heartbeat speed
        AdjustAudioProperties();
    }
    private void AdjustAudioProperties()
    {
    if (heartbeatAudio != null && heartbeatScaleEffect != null)
        {
            // Adjust pitch proportionally to the heartbeat speed
            heartbeatAudio.pitch = normalAudioPitch *
            (heartbeatScaleEffect.currentHeartbeatSpeed /
            heartbeatScaleEffect.normalHeartbeatSpeed);
            // Adjust volume proportionally to the heartbeat speed
            heartbeatAudio.volume = Mathf.Lerp(normalHeartbeatVolume,
            maxHeartbeatVolume,
            (heartbeatScaleEffect.currentHeartbeatSpeed -
            heartbeatScaleEffect.normalHeartbeatSpeed) /
            (increasedHeartbeatSpeed - heartbeatScaleEffect.normalHeartbeatSpeed));
        }
    }
}
