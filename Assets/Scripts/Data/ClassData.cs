using UnityEngine;

namespace UTS
{
    [System.Serializable]
    public class ClassData
    {
        public string TheName;
        public float r, g, b;

        public Color GetColor()
        {          

            if (r > 1f || g > 1f || b > 1f)
            {
                //Debug.Log(r + " " + g + " " + b);
                r /= 255;
                g /= 255;
                b /= 255;
            }
            return new Color(r, g, b, 1);
        }

        public void SetColor(Color theColor)
        {
            r = theColor.r;
            g = theColor.g;
            b = theColor.b;
        }
    }
}