using System;
using System.Collections;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEditor;

[AttributeUsage(AttributeTargets.Field)]
public class DebuggedAttribute : Attribute
{
    public MemberInfo content;

    public virtual void ShowContent()
    {
        EditorGUILayout.BeginHorizontal();
        GetContent();
        EditorGUILayout.EndHorizontal();
    }

    protected virtual void GetContent()
    {
        if (content != null)
        {
            EditorGUILayout.LabelField($"Script: {content.ReflectedType} | Field : { ((FieldInfo)content).FieldType }");
        }
    }
}


[AttributeUsage(AttributeTargets.Property)]
public class DebuggedPropertyAttribute : DebuggedAttribute
{
    public delegate void PropertyHandler();
    private PropertyHandler OnPropertyChanged;
    private object _property;
    object property 
    {
        get
        {
            return _property;
        }
        set
        {
            OnPropertyChanged?.Invoke();
            _property = value;
        }
    }
    public DebuggedPropertyAttribute(object property)
    {
        this.property = property;
        _property = property;
    }

    ~DebuggedPropertyAttribute()
    {
        OnPropertyChanged = null;
    }

    protected override void GetContent()
    {
        if (content != null)
        {
            EditorGUILayout.LabelField($"Script: {content.ReflectedType} |Type: { ((PropertyInfo)content).PropertyType } | Field : {content.Name}");
        }
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class DebuggedFieldAttribute : DebuggedAttribute
{
    public DebuggedFieldAttribute(MemberInfo content)
    {

    }
    protected override void GetContent()
    {
        if (content != null)
        {

            EditorGUILayout.LabelField($"Script: {content.ReflectedType} | Field : { ((FieldInfo)content).FieldType }");
        }
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class DebuggedObserverAttribute : DebuggedAttribute
{
    public DebuggedObserverAttribute(MemberInfo content)
    {
        
    }
}
