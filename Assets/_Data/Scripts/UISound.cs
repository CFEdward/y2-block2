using UnityEngine;
using UnityEngine.UI;

public class UISound : MonoBehaviour
{
    [SerializeField]
    public Slider slider;
    public string soundName = "ButtonPop";

    public bool muting;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        //FindObjectOfType<AudioManager>().Play("Sparks");
    }

    public void Volume(float volume)
    {
        AudioListener.volume = volume;
    }
    
    //public void Pitch(float pitch)
    //{
    //    AudioListener.pitch = pitch;
    //}

    public void PlayClick()
    {
        FindFirstObjectByType<AudioManager>().Play("ButtonClick");
    }

    public void MuteToggle(bool muted)
    {
        if (muted)
        {
            muting = true;
            AudioListener.volume = 0;
            Debug.Log("muting");
        }

        else
        {
            Debug.Log("NOT muting");
            muting = false;
            AudioListener.volume = 1;
        }
    }



    public void PlayVolumeCheck(float value)
    {
        if (muting) {
            return;
                }
        else {

            if (audioManager != null)
            {
                FindFirstObjectByType<AudioManager>().Play("ButtonExample");
            }
            else
            {
                Debug.LogWarning("Cannot play sound: AudioManager is not assigned.");
            }

            Debug.Log($"Slider value changed: {value}");
            FindFirstObjectByType<AudioManager>().Play("ButtonExample");
        }
    }


}
