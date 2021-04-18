﻿using CZToolKit.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtension
{
    /// <summary> 获取CC的真实高度 </summary>
    public static float GetRealHeight(this CharacterController characterController)
    {
        return Mathf.Max(characterController.radius * 2, characterController.height);
    }

    /// <summary> 获取CC顶部半圆中心 </summary>
    public static Vector3 GetTopCenter(this CharacterController characterController)
    {
        return Vector3.down * characterController.radius + Vector3.up * characterController.GetRealHeight() / 2 + characterController.center;
    }

    /// <summary> 获取CC底部半圆中心 </summary>
    public static Vector3 GetBottomCenter(this CharacterController characterController)
    {
        return Vector3.up * characterController.radius + Vector3.down * characterController.GetRealHeight() / 2 + characterController.center;
    }

    /// <summary> 快速排序 </summary>
    public static void Sort<T>(this List<T> _original, Func<T, T, bool> _func)
    {
        if (_original.Count == 1)
            return;

        // 抽取一个数据作为中间值
        int index = UnityEngine.Random.Range(0, _original.Count);
        T rN = _original[index];

        // 声明小于中间值的列表
        List<T> left = new List<T>(Mathf.Max(4, _original.Count / 2));
        // 声明大于中间值的列表
        List<T> right = new List<T>(Mathf.Max(4, _original.Count / 2));
        // 遍历数组，与中间值比较，小于中间值的放在left，大于中间值的放在right
        for (int i = 0; i < _original.Count; i++)
        {
            if (i == index) continue;

            if (_func(_original[i], rN))
                left.Add(_original[i]);
            else
                right.Add(_original[i]);
        }

        _original.Clear();

        // 如果左列表元素个数不为0，就把左列表也排序
        if (left.Count != 0)
        {
            left.Sort(_func);
            _original.AddRange(left);
        }
        _original.Add(rN);
        // 如果右列表元素个数不为0，就把右列表也排序
        if (right.Count != 0)
        {
            right.Sort(_func);
            _original.AddRange(right);
        }
        return;
    }

    /// <summary> 获取颜色明度 </summary>
    public static float GetLuminance(this Color _color)
    {
        return 0.299f * _color.r + 0.587f * _color.g + 0.114f * _color.b;
    }

    public static string GetRelativePath(this Transform _transform, Transform _parent)
    {
        string path = _transform.name;
        Transform trans = _transform.parent;
        while (trans != null && trans != _parent)
        {
            path = trans.name + "/" + path;
            trans = trans.parent;
        }
        return path;
    }

    public static Rect GetSide(this Rect _rect, UIDirections _sideDirection, float _side, float _offset = 0)
    {
        switch (_sideDirection)
        {
            case UIDirections.MiddleCenter:
                return new Rect(_rect.x + _side / 2, _rect.y + _side / 2, _rect.width - _side, _rect.height - _side);
            case UIDirections.Top:
                return new Rect(_rect.x + _side / 2, _rect.y - _side / 2 + _offset, _rect.width - _side, _side);
            case UIDirections.Bottom:
                return new Rect(_rect.x + _side / 2, _rect.y + _rect.height - _side / 2 + _offset, _rect.width - _side, _side);
            case UIDirections.Left:
                return new Rect(_rect.x - _side / 2 + _offset, _rect.y + _side / 2, _side, _rect.height - _side);
            case UIDirections.Right:
                return new Rect(_rect.x + _rect.width - _side / 2 + _offset, _rect.y + _side / 2, _side, _rect.height - _side);
            case UIDirections.TopLeft:
                return new Rect(_rect.x - _side / 2 + _offset, _rect.y - _side / 2 + _offset, _side, _side);
            case UIDirections.TopRight:
                return new Rect(_rect.x + _rect.width - _side / 2 + _offset, _rect.y - _side / 2 + _offset, _side, _side);
            case UIDirections.BottomLeft:
                return new Rect(_rect.x - _side / 2 + _offset, _rect.y + _rect.height - _side / 2 + _offset, _side, _side);
            case UIDirections.BottomRight:
                return new Rect(_rect.x + _rect.width - _side / 2 + _offset, _rect.y + _rect.height - _side / 2 + _offset, _side, _side);
        }
        return new Rect();
    }
}