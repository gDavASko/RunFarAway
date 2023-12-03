using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace KBP.CORE
{
    [CreateAssetMenu(fileName = nameof(SOConstantsContainer), menuName = "KBP/SOConstantsContainer")]
    public class SOConstantsContainer: ScriptableObject, IConstants
    {
        [FormerlySerializedAs("constants")] [SerializeField]
        public ObjectIdPairContainer<List<string>> _constants = new ObjectIdPairContainer<List<string>>();


        public List<string> GetConsts(params string[] groupIDs)
        {
            List<string> res = new List<string>();
            List<string> filters = groupIDs != null && groupIDs.Length > 0 ? groupIDs.ToList<string>() : new List<string>();

            if(filters.Count <= 0)
            {
                foreach(var v in _constants.Values)
                {
                    res.AddRange(v);
                }
            }
            else
            {
                foreach(string filter in filters)
                {
                    if(_constants.Keys.Contains(filter))
                    {
                        res.AddRange(_constants[filter]);
                    }
                }
            }

            return res;
        }

        public void AddConsts(string groupId, List<string> consts)
        {
            if(!_constants.Keys.Contains(groupId))
            {
                _constants.Add(groupId, consts);
            }
            else
            {
                var existsConsts = _constants.GetObject(groupId);

                foreach(string newConst in consts)
                {
                    if(!existsConsts.Contains(newConst))
                    {
                        existsConsts.Add(newConst);
                    }
                }

                _constants.Add(groupId, existsConsts);
            }

            #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            #endif
        }

        private void Reset()
        {
            _constants = new ObjectIdPairContainer<List<string>>();

            List<string> windows  = new List<string>();
            windows.Add("window.main");
            windows.Add("window.game");
            windows.Add("window.loading");
            windows.Add("window.result");
            windows.Add("window.settings");
            _constants.Add("window", windows);

            List<string> events = new List<string>();
            events.Add("open");
            events.Add("close");
            events.Add("click");
            events.Add("enable");
            events.Add("disable");
            _constants.Add("events.ui", events);
        }
    }
}