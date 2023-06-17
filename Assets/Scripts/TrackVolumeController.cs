using UnityEngine;

public class TrackVolumeController : MonoBehaviour
{
    public AudioSource trackAudioSource;
    public float minVolumeDistance = 10f;
    public float maxVolumeDistance = 30f;
    private Transform listenerTransform;

    void Start()
    {
        // econtra o objeto camera
        listenerTransform = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        float listenerDistance = Vector3.Distance(transform.position, listenerTransform.position);
        float t = Mathf.InverseLerp(minVolumeDistance, maxVolumeDistance, listenerDistance);
        float volume = Mathf.Lerp(1f, 0f, t);
        trackAudioSource.volume = volume;
    }
}