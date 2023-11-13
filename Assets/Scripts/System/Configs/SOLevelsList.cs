using System;
using UnityEngine;

namespace RFW
{
    [CreateAssetMenu(fileName = nameof(SOLevelsList), menuName = "RFW/SOLevelsList")]
    public class SOLevelsList : ScriptableObject, ITypeInfo
    {
        [SerializeField] private string[] _levelsIds = null;

        public Type Type => typeof(SOLevelsList);

        public string[] GetLevels()
        {
            return _levelsIds;
        }
    }
}