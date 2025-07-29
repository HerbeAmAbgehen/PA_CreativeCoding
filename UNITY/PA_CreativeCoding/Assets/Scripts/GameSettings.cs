using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public Toggle invertMouse;

    public Slider setVolume;

    public bool invertFlight = false;


    private void Start()
    {
        setVolume.onValueChanged.AddListener(delegate { SetVolume(); });
        invertMouse.onValueChanged.AddListener(delegate { InvertFlight(); });

        AudioListener.volume = setVolume.value;
    }

    private void SetVolume()
    {
        AudioListener.volume = setVolume.value;
    }

    private void InvertFlight()
    {
        invertFlight = invertMouse.isOn;
    }
}
