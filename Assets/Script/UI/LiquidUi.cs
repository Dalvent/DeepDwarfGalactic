using Script.Helpers;
using UnityEngine;

public class LiquidUi : MonoBehaviour
{
    public RectTransform Liquid;
    public RectTransform LiquidTop;
    public RectTransform LiquidBottom;

    private float _percent;
    public float Percent
    {
        get => _percent;
        set
        {
            if (_percent == value)
                return;

            _percent = value;
            float y = Mathf.Lerp(LiquidBottom.position.y, LiquidTop.position.y, value);
            Liquid.position = Liquid.position.SetY(y);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
