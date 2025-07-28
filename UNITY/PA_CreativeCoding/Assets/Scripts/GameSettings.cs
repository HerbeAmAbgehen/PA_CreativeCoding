using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public Toggle invertMouse;

    public Slider setVolume;
    
    public bool invertFlight = false;

    private float volume = Mathf.Clamp(50, 0, 100);

    private void Update()
    {
        invertFlight = invertMouse.isOn;

        volume = setVolume.value;
    }
}
