#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VinTools.BetterRuleTiles
{
    public static class Functions
    {
        //texture creation
        public static Texture2D CreateColoredTexture(float color) => CreateColoredTexture(new Color(color, color, color));
        public static Texture2D CreateColoredTexture(Color color)
        {
            //create texture
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, color);
            tex.filterMode = FilterMode.Point;
            tex.Apply();
            return tex;
        }

        public static Texture2D CreateMissingTexture()
        {
            //create texture
            Texture2D tex = new Texture2D(2, 2);

            //set texture colors
            tex.SetPixel(0, 0, Color.black);
            tex.SetPixel(0, 1, new Color(1, 0, 1));
            tex.SetPixel(1, 0, new Color(1, 0, 1));
            tex.SetPixel(1, 1, Color.black);

            //apply texture
            tex.filterMode = FilterMode.Point;
            tex.Apply();
            return tex;
        }
        public static Texture2D CreateFilledTexture(Color color, int width, int height)
        {
            //create texture
            Texture2D tex = new Texture2D(width, height);

            //set texture colors
            for (int x = 0; x < tex.width; x++)
            {
                for (int y = 0; y < tex.height; y++)
                {
                    //set color
                    tex.SetPixel(x, y, color);
                }
            }

            //apply texture
            tex.filterMode = FilterMode.Point;
            tex.Apply();
            return tex;
        }
        public static Texture2D CreateSlopeTexture(Color color, float startHeight, float endHeight, int width, int height)
        {
            //create texture
            Texture2D tex = new Texture2D(width, height);

            //set texture colors
            for (int x = 0; x < tex.width; x++)
            {
                for (int y = 0; y < tex.height; y++)
                {
                    float currentHeight = Mathf.Lerp(startHeight, endHeight, (float)x / (width - 1));
                    float heightPixel = (height * currentHeight);

                    //set color
                    tex.SetPixel(x, y, y <= heightPixel ? color : new Color(0, 0, 0, 0));
                }
            }

            //apply texture
            tex.filterMode = FilterMode.Point;
            tex.Apply();
            return tex;
        }
        public static Texture2D Base64ToTexture(string base64)
        {
            Texture2D tex = new Texture2D(1, 1);
            tex.hideFlags = HideFlags.HideAndDontSave;
            tex.LoadImage(Convert.FromBase64String(base64));
            return tex;
        }
        //texture modification
        public static Texture2D MirrorTexture(Texture2D original, bool flipX, bool flipY)
        {
            if (!original.isReadable) return original;
            if (!flipX && !flipY) return original;

            //create texture
            Texture2D tex = new Texture2D(original.width, original.height);

            //set texture colors
            for (int x = 0; x < tex.width; x++)
            {
                for (int y = 0; y < tex.height; y++)
                {
                    Color color = original.GetPixel(flipX ? tex.width - x - 1 : x, flipY ? tex.height - y - 1 : y);
                    tex.SetPixel(x, y, color);
                }
            }

            //apply texture
            tex.filterMode = FilterMode.Point;
            tex.Apply();
            return tex;
        }
        //transformation
        public static Vector2Int TransformPoint(Vector2Int point, Vector2Int iH, Vector2Int jH) => point.x * iH + point.y * jH;
    }
}
#endif