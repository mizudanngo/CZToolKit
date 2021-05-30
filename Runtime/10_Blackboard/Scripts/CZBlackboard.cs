#region 注 释
/***
 *
 *  Title:
 *  
 *  Description:
 *  
 *  Date:
 *  Version:
 *  Writer: 
 *
 */
#endregion
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CZToolKit.Core.Blackboards
{
    [Serializable]
    public class CZBlackboard
    {
        /// <summary> key是guid </summary>
        [SerializeField]
        Dictionary<string, ICZType> dataMap = new Dictionary<string, ICZType>();
        /// <summary> 名称和GUID的映射 </summary>
        [SerializeField, HideInInspector]
        Dictionary<string, string> guidMap = new Dictionary<string, string>();

        public IReadOnlyDictionary<string, ICZType> DataMap { get { return dataMap; } }
        public IReadOnlyDictionary<string, string> GUIDMap { get { return guidMap; } }

        //public CZBlackboard()
        //{
        //    dataMap = new Dictionary<string, ICZType>()
        //    guidMap = new Dictionary<string, string>();
        //}

        public bool ContainsName(string _name)
        {
            return guidMap.ContainsKey(_name);
        }

        public bool ContainsGUID(string _guid)
        {
            return dataMap.ContainsKey(_guid);
        }

        public bool SetData(string _name, ICZType _data)
        {
            if (TryGetData(_name, out ICZType data))
                return false;

            if (!guidMap.TryGetValue(_name, out string guid))
                guidMap[_name] = guid = Guid.NewGuid().ToString();

            dataMap[guid] = _data;
            return true;
        }

        public bool TryGetData(string _name, out ICZType _data)
        {
            _data = null;
            if (!guidMap.TryGetValue(_name, out string guid)) return false;
            if (!dataMap.TryGetValue(guid, out ICZType data)) return false;

            _data = data;
            return true;
        }

        public bool TryGetValue<T>(string _name, out T _value)
        {
            _value = default;
            if (!guidMap.TryGetValue(_name, out string guid)) return false;
            if (!dataMap.TryGetValue(guid, out ICZType data)) return false;
            if (data is CZType<T> tData)
            {
                _value = tData.Value;
                return true;
            }
            return false;
        }

        public void SetData<T>(string _name, T _value)
        {
            if (!guidMap.TryGetValue(_name, out string guid))
            {
                guidMap[_name] = guid = Guid.NewGuid().ToString();
                dataMap[guid] = new CZType<T>(_value);
                return;
            }

            if (!dataMap.TryGetValue(guid, out ICZType data))
                dataMap[guid] = data = new CZType<T>(_value);
            else if (data is CZType<T> tData)
                tData.Value = _value;
        }

        public bool RemoveData(string _name)
        {
            if (!guidMap.TryGetValue(_name, out string guid))
                return false;
            guidMap.Remove(_name);
            if (dataMap.ContainsKey(guid))
                dataMap.Remove(guid);
            return true;
        }

        public bool Rename(string _oldName, string _newName)
        {
            if (string.IsNullOrEmpty(_oldName) || string.IsNullOrEmpty(_newName)) return false;
            if (!guidMap.TryGetValue(_oldName, out string guid)) return false;
            if (guidMap.ContainsKey(_newName)) return false;

            guidMap.Remove(_oldName);
            guidMap[_newName] = guid;
            return true;
        }

        public void Fixed()
        {
            if (dataMap == null)
                dataMap = new Dictionary<string, ICZType>();
            if (guidMap == null)
                guidMap = new Dictionary<string, string>();
        }
    }
}
