using UnityEngine;

public class Saber : MonoBehaviour
{
    [SerializeField] private float extendSpeed = 0.1f;
    [SerializeField] private float minScale = 0f;
    [SerializeField] private float maxScale = 0.7f;
    [SerializeField] private Transform bladeTransform;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;
    [SerializeField] private AudioClip swingClip;
    [SerializeField] private float pitchVariation = 0.1f;

    public bool IsOn { get; private set; }

    private float currentScaleT; // between 0 and 1 - slerp T value
    private float scaleDelta;
    private float xScale;
    private float zScale;

    // Start is called before the first frame update
    void Start()
    {
        IsOn = true;
        currentScaleT = 1f;
        scaleDelta = 0f;
        xScale = bladeTransform.localScale.x;
        zScale = bladeTransform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        // get input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsOn)
            {
                IsOn = false;
                scaleDelta = -maxScale / extendSpeed;
                PlayClip(closeClip);
            } else
            {
                IsOn = true;
                scaleDelta = maxScale / extendSpeed;
                PlayClip(openClip);
            }
        }

        // apply scaling changes
        if (IsOn)
        {
            Vector3 newScale = Vector3.Slerp(new Vector3(xScale, minScale, zScale), new Vector3(xScale, maxScale, zScale), currentScaleT);
            currentScaleT += scaleDelta * Time.deltaTime;
            if (currentScaleT > 1f)
            {
                currentScaleT = 1f;
                scaleDelta = 0;
            }
            bladeTransform.localScale = newScale;
        } else
        {
            Vector3 newScale = Vector3.Slerp(new Vector3(xScale, minScale, zScale), new Vector3(xScale, maxScale, zScale), currentScaleT);
            currentScaleT += scaleDelta * Time.deltaTime;
            if (currentScaleT < 0f)
            {
                currentScaleT = 0f;
                scaleDelta = 0;
            }
            bladeTransform.localScale = newScale;
        }
    }

    private void PlayClip(AudioClip clip, bool varyPitch = true)
    {
        float pitch = 1f;
        if (varyPitch)
        {
            pitch += Random.Range(-pitchVariation, pitchVariation);
        }
        audioSource.clip = clip;
        audioSource.pitch = pitch;
        audioSource.Play();
    }

    public void PlaySwingClip()
    {
        PlayClip(swingClip);
    }
}
