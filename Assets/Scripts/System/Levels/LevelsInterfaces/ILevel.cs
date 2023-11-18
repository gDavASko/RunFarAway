using System;
using System.Collections.Generic;
using RFW.Events;
using UnityEngine;

namespace RFW.Levels
{
   public interface ILevel : IDisposable, IInitializable
   {
      string Id { get; }

      Transform Transform { get; }
      Vector3 PlayerSpawnPoint { get; }
      List<KeyValuePair<string, Vector3>> EnemySpawnPoints { get; }
      List<KeyValuePair<string, Vector3>> ItemsSpawnPoints { get; }
   }
}