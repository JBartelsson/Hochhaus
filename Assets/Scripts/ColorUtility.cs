using UnityEngine;

public static class CMYKColorUtility
{
    // Convert CMYK to RGB
    public static Color CMYKToRGB(float c, float m, float y, float k)
    {
        float r = (1 - c) * (1 - k);
        float g = (1 - m) * (1 - k);
        float b = (1 - y) * (1 - k);
        return new Color(r, g, b);
    }
    
    public static Color CMYKToRGB(CMYKColor color)
    {
        return CMYKToRGB(color.C, color.M, color.Y, color.K);
    }

    // Convert RGB to CMYK
    public static CMYKColor RGBToCMYK(Color color)
    {
        float r = color.r;
        float g = color.g;
        float b = color.b;

        float k = 1 - Mathf.Max(r, g, b);
        float c = (1 - r - k) / (1 - k);
        float m = (1 - g - k) / (1 - k);
        float y = (1 - b - k) / (1 - k);

        if (Mathf.Approximately(k, 1)) c = m = y = 0; // Avoid NaN issues

        return new CMYKColor(c, m, y, k);
    }
    
    public static CMYKColor MixCMYKColors(
        (float c, float m, float y, float k) color1,
        (float c, float m, float y, float k) color2,
        float ratio = .5f)
    {
        // float mixC = Mathf.Lerp(color1.c, color2.c, ratio);
        // float mixM = Mathf.Lerp(color1.m, color2.m, ratio);
        // float mixY = Mathf.Lerp(color1.y, color2.y, ratio);
        // float mixK = Mathf.Lerp(color1.k, color2.k, ratio);
        float factor = .8f;
        float mixC = Mathf.Clamp((color1.c + color2.c) * factor, 0, 1f);
        float mixM = Mathf.Clamp((color1.m + color2.m) * factor, 0, 1f);
        float mixY = Mathf.Clamp((color1.y + color2.y) * factor, 0, 1f);
        float mixK = Mathf.Clamp((color1.k + color2.k) * factor, 0, 1f);

        return new CMYKColor(mixC, mixM, mixY, mixK);
    }
    
    public static CMYKColor MixCMYKColors(
        CMYKColor color1,
        CMYKColor color2,
        float ratio = .5f)
    {
        return MixCMYKColors((color1.C, color1.M, color1.Y, color1.K), (color2.C, color2.M, color2.Y, color2.K));
    }
}