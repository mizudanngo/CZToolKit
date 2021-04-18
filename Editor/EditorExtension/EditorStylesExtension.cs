﻿using UnityEditor;
using UnityEngine;

namespace CZToolKit.Core.Editors
{
    public static class EditorStylesExtension
    {
        static GUIStyle transparent;
        /// <summary> 完全透明的Style </summary>
        public static GUIStyle Transparent
        {
            get
            {
                if (transparent == null)
                {
                    transparent = new GUIStyle();
                    transparent.normal.background = MakeTex(1, 1, new Color(1, 1, 1, 0));
                }
                return transparent;
            }
        }

        static GUIStyle middleLabelStyle;
        /// <summary> 居中Label </summary>
        public static GUIStyle MiddleLabelStyle
        {
            get
            {
                if (middleLabelStyle == null)
                {
                    middleLabelStyle = new GUIStyle(EditorStyles.label);
                    middleLabelStyle.alignment = TextAnchor.MiddleCenter;
                }
                return middleLabelStyle;
            }
        }

        static GUIStyle leftLabelStyle;
        /// <summary> 左对齐Label </summary>
        public static GUIStyle LeftLabelStyle
        {
            get
            {
                if (leftLabelStyle == null)
                {
                    leftLabelStyle = new GUIStyle(EditorStyles.label);
                    leftLabelStyle.alignment = TextAnchor.MiddleLeft;
                }
                return leftLabelStyle;
            }
        }


        static GUIStyle rightLabelStyle;
        /// <summary> 右对齐Label </summary>
        public static GUIStyle RightLabelStyle
        {
            get
            {
                if (rightLabelStyle == null)
                {
                    rightLabelStyle = new GUIStyle(EditorStyles.label);
                    rightLabelStyle.alignment = TextAnchor.MiddleRight;
                }
                return rightLabelStyle;
            }
        }

        static Texture2D whiteTexture;
        /// <summary> 白色Texture(1,1) </summary>
        public static Texture2D WhiteTexture
        {
            get
            {
                if (whiteTexture == null)
                    whiteTexture = MakeTex(1, 1, Color.white);
                return whiteTexture;
            }
        }

        public static Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}