#region 注 释
/***
 *
 *  Title:
 *  
 *  Description:
 *  
 *  Date:
 *  Version:
 *  Writer: 半只龙虾人
 *  Github: https://github.com/HalfLobsterMan
 *  Blog: https://www.crosshair.top/
 *
 */
#endregion
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CZToolKit.Core.SharedVariable
{
    public static class SharedVariableUtility
    {
        public static IEnumerable<SharedVariable> CollectionObjectSharedVariables(object _object)
        {
            Type sharedType = typeof(SharedVariable);
            foreach (var fieldInfo in Utility_Reflection.GetFieldInfos(_object.GetType()))
            {
                if (sharedType.IsAssignableFrom(fieldInfo.FieldType))
                {
                    SharedVariable variable = fieldInfo.GetValue(_object) as SharedVariable;
                    if (variable == null)
                    {
                        variable = Activator.CreateInstance(fieldInfo.FieldType) as SharedVariable;
                        fieldInfo.SetValue(_object, variable);
                    }
                    yield return variable;
                    continue;
                }
            }
        }

    }
}
