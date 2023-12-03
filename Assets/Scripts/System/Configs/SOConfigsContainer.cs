using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RFW
{
   public class SOConfigsContainer : IConfigGetter
   {
      private Dictionary<Type, ITypeInfo> _dictConfigs = null;

      public async UniTask LoadCofigsAsync(string[] configIds, IGettableAsset _getter)
      {
         _dictConfigs = new Dictionary<Type, ITypeInfo>();

         foreach (var configID in configIds)
         {
            var config = await _getter.LoadResource<ITypeInfo>(configID);

            if (config == null)
            {
               Debug.LogError($"[{nameof(SOConfigsContainer)}] Try to load non Exist Config!");
               continue;
            }

            _dictConfigs[config.Type] = config;
         }
      }

      public T GetConfig<T>() where T: class
      {
         if (_dictConfigs == null || _dictConfigs.Count == 0)
         {
            Debug.LogError($"[{nameof(SOConfigsContainer)}] " +
                           $"Try to get Config with Type <{typeof(T).Name}> when config not Loaded!");
            return default;
         }

         if (_dictConfigs.TryGetValue(typeof(T), out ITypeInfo config))
         {
            return config as T;
         }
         else
         {
            Debug.LogError($"[{nameof(SOConfigsContainer)}] " +
                           $"Try to get Non Exist Config with Type <{typeof(T).Name}>!");
            return default;
         }
      }

      public void Dispose()
      {
         _dictConfigs.Clear();
         _dictConfigs = null;
      }
   }
}