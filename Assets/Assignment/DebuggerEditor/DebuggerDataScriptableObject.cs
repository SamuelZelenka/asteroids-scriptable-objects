using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Debugger Data/Debugger Data", fileName = "new DebuggerData")]
public class DebuggerDataScriptableObject : ScriptableObject
{
    private List<DebuggedAttribute> _options = new List<DebuggedAttribute>();

    public void ShowContent()
    {
        foreach (DebuggedAttribute option in _options)
        {
            option.ShowContent();
        }
    }
    private void OnEnable()
    {
        GetDebuggedAttributes();
    }

    private void GetDebuggedAttributes()
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (Assembly assembly in assemblies)
        {
            Type[] assemblyTypes = assembly.GetTypes();

            foreach (Type type in assemblyTypes)
            {
                BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                MemberInfo[] members = type.GetMembers(flags);
                foreach (MemberInfo member in members)
                {
                    if (member.CustomAttributes.ToArray().Length > 0)
                    {
                        DebuggedAttribute debuggedAttribute = member.GetCustomAttribute<DebuggedAttribute>();
                        if (debuggedAttribute != null)
                        {
                            _options.Add(debuggedAttribute);
                            debuggedAttribute.content = member;
                        }
                    }
                }
            }
        }
    }
}
