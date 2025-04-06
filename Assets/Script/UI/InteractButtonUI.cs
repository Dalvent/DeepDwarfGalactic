using UnityEngine;
using UnityEngine.UI;

public class InteractButtonSlider : MonoBehaviour
{
    public Image BackgroundImage;
    public Image CooldownImage;
    public Image FullImage;
    
    private float _persent;
    public float Persent
    {
        get => _persent;
        set
        {
            _persent = value;
            
            CooldownImage.fillAmount = Persent;
            
            bool isReady = _persent >= 1;
            if (isReady)
            {
                FullImage.enabled = true;
                CooldownImage.enabled = false;
                BackgroundImage.enabled = false;
            }
            else
            {
                FullImage.enabled = false;
                CooldownImage.enabled = true;
                BackgroundImage.enabled = true;
            }
        }
    }
}