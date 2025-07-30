using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    //Stores information on game settings and is passed on from menu scene

    public Toggle invertMouse;

    public Slider setVolume;

    public bool invertFlight = false;


    private void Start()
    {
        setVolume.onValueChanged.AddListener(delegate { SetVolume(); });
        invertMouse.onValueChanged.AddListener(delegate { InvertFlight(); });

        AudioListener.volume = setVolume.value;
    }

    //Sets general sound volume on audio listener to slider value
    private void SetVolume()
    {
        AudioListener.volume = setVolume.value;
    }

    //Uses Toggle to control whether Mouse y axis is inverted during the game
    private void InvertFlight()
    {
        invertFlight = invertMouse.isOn;
    }
}
