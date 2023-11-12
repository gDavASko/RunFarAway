using System;
using UnityEditor;
using UnityEngine;

public class LevelMarker : MonoBehaviour
{
    [SerializeField] private string _id = "marker";
    public string Id => _id;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position + Vector3.up * 0.5f, name);
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
#endif
}