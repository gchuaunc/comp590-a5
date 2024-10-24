using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Debug.LogWarning("More than one Player detected in the scene, disabling this one");
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
