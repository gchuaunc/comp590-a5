using UnityEngine;

public class Saber : MonoBehaviour
{
    [SerializeField] private float extendSpeed = 0.1f;
    [SerializeField] private float minScale = 0f;
    [SerializeField] private float maxScale = 0.7f;
    [SerializeField] private Transform bladeTransform;

    private bool isOn;
    private float currentScaleT; // between 0 and 1 - slerp T value
    private float scaleDelta;
    private float xScale;
    private float zScale;

    // Start is called before the first frame update
    void Start()
    {
        isOn = true;
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
            if (isOn)
            {
                isOn = false;
                scaleDelta = -maxScale / extendSpeed;
            } else
            {
                isOn = true;
                scaleDelta = maxScale / extendSpeed;
            }
        }

        // apply scaling changes
        if (isOn)
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
}
