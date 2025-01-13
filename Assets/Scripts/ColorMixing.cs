using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ColorMixing : MonoBehaviour
{
    public CustomColor cmyk1;
    public CustomColor cmyk2;
    [FormerlySerializedAs("renderer")] [SerializeField] MeshRenderer meshRenderer;
    [FormerlySerializedAs("renderer")] [SerializeField] MeshRenderer meshRendererColor1;
    [FormerlySerializedAs("renderer")] [SerializeField] MeshRenderer meshRendererColor2;
    // Start is called before the first frame update
    void Start()
    {
        // Convert RGB to CMYK
        // Debug.Log($"First Color: "+ ColorUtility.CMYKToRGB(cmyk1));
        // Debug.Log($"Second Color: "+ ColorUtility.CMYKToRGB(cmyk2));
        //
        // // Mix the CMYK colors
        // var mixedCMYK = ColorUtility.MixCMYKColors(cmyk1, cmyk2);
        //
        // // Convert back to RGB
        // Color mixedColor = ColorUtility.CMYKToRGB(mixedCMYK);
        // Debug.Log($"Resulting Color: "+ mixedColor);

        // Apply to material
        meshRendererColor1.material.color = cmyk1.rgbColor;
        meshRendererColor2.material.color = cmyk2.rgbColor;
        meshRenderer.material.color = ColorUtility.MixColors(cmyk1, cmyk2).rgbColor;
    }

    private void OnValidate()
    {
        Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
