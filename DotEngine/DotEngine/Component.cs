namespace DotEngine;

using System;
using System.Collections.Generic;

public class Component : Object
{
    private bool _enabled = true;

    public Transform Transform => GameObject.Transform;
    public GameObject GameObject { get; set; }

    public bool Enabled => _enabled;

    public event Action<bool> EnableStatusChanged;

    public Component? GetComponent(Type type) => GameObject.GetComponent(type);

    #region TryGetComponent
    public T? GetComponent<T>() => GameObject.GetComponent<T>();

    public bool TryGetComponent(Type type, out Component component) =>
        GameObject.TryGetComponent(type, out component);

    public bool TryGetComponent<T>(out T component) =>
        GameObject.TryGetComponent<T>(out component);
    #endregion

    #region GetComponentInChildren
    public Component? GetComponentInChildren(Type type, bool includeInactive = false) =>
        GameObject.GetComponentInChildren(type, includeInactive);

    public T? GetComponentInChildren<T>(bool includeInactive = false) =>
        GameObject.GetComponentInChildren<T>(includeInactive);

    public Component[]? GetComponentsInChildren(Type type, bool includeInactive = false) =>
        GameObject.GetComponentsInChildren(type, includeInactive);

    public Component[]? GetComponentsInChildren(Type type) =>
        GameObject.GetComponentsInChildren(type);

    public T[]? GetComponentsInChildren<T>(bool includeInactive) where T : Component =>
        GameObject.GetComponentsInChildren<T>(includeInactive);

    public T[]? GetComponentsInChildren<T>() where T : Component =>
        GameObject.GetComponentsInChildren<T>(false);
    #endregion

    #region GetComponentInParent
    public Component? GetComponentInParent(Type type, bool includeInactive) =>
        GameObject.GetComponentInParent(type, includeInactive);

    public Component? GetComponentInParent(Type type) =>
        GameObject.GetComponentInParent(type);

    public T? GetComponentInParent<T>() where T : Component =>
        GameObject.GetComponentInParent<T>();
    #endregion

    #region GetComponentsInParent
    public Component[]? GetComponentsInParent(Type t) =>
        GameObject.GetComponentsInParent(t);

    public T[] GetComponentsInParent<T>(bool includeInactive) where T : Component =>
        GameObject.GetComponentsInParent<T>(includeInactive);

    public T[]? GetComponentsInParent<T>() where T : Component =>
        GameObject.GetComponentsInParent<T>();

    public Component[] GetComponents(Type type) =>
        GameObject.GetComponents(type);

    public void GetComponents(Type type, List<Component> results) =>
        GameObject.GetComponents(type, results);

    public void GetComponents<T>(List<T> results) where T : Component =>
        GameObject.GetComponents(results);

    public T[] GetComponents<T>() where T : Component =>
        GameObject.GetComponents<T>();
    #endregion

    public int GetComponentIndex() =>
        GameObject.GetComponentIndex(this);

    public bool CompareTag(string tag) =>
        GameObject.CompareTag(tag);

    public void SetEnableStatus(bool enable)
    {
        if (_enabled == enable)
            return;

        _enabled = enable;
        EnableStatusChanged?.Invoke(enable);
    }
}
