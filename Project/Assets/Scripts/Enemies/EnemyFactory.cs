﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class EnemyFactory
{
    private static Dictionary<char, Type> enemies = new Dictionary<char, Type>();
    private static Dictionary<Type, MethodInfo[]> callbacks = new Dictionary<Type, MethodInfo[]>();

    public static IEnemy CreateEnemy(char type, TypeTemplatePair[] typeTemplates)
    {
        if (!enemies.ContainsKey(type))
            return null;

        GameObject template = typeTemplates.Where(x => x.type.Equals(enemies[type].Name)).FirstOrDefault().template;
        if (!template)
            return null;

        GameObject instance = GameObject.Instantiate(template);
        IEnemy enemy = (IEnemy)instance.GetComponent(enemies[type]);
        if (enemy == null)
            enemy = (IEnemy)instance.AddComponent(enemies[type]);

        foreach (MethodInfo callback in callbacks[enemies[type]])
        {
            if (callback.GetParameters().Length == 1)
                callback.Invoke(enemy, new object[] { type });
            else if (callback.GetParameters().Length == 0)
                callback.Invoke(enemy, new object[] { });
        }

        return enemy;
    }

    // Called on launch
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        // Find all the classes that have the EnemyFactory attribute
        var discoveredEnemies = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                from t in assembly.GetTypes()
                                let attrib = t.GetCustomAttribute<EnemyFactoryAttribute>(false)
                                where attrib != null
                                select new { type = t, attribute = attrib };



        foreach (var found in discoveredEnemies)
        {
            if (!typeof(IEnemy).IsAssignableFrom(found.type) || !typeof(MonoBehaviour).IsAssignableFrom(found.type))
            {
                Debug.LogWarning("Found EnemyFactory attribute on class that doesn't implement IEnemy or extend MonoBehaviour, skipping.");
                continue;
            }

            foreach (char c in found.attribute.factoryKeys)
            {
                if (enemies.ContainsKey(c))
                {
                    Debug.LogWarning("Key " + c + " already defined for " + enemies[c].Name + " when trying to register " + found.type.Name + ", skipping.");
                    continue;
                }

                enemies[c] = found.type;
            }

            callbacks[found.type] = (from m in found.type.GetMethods()
                                     let p = m.GetParameters()
                                     where p.Length == 0 || (p.Length == 1 && p[0].ParameterType == typeof(char))
                                     let attrib = m.GetCustomAttribute<EnemyFactoryCallbackAttribute>(true)
                                     where attrib != null
                                     select m).ToArray();
        }
    }
}

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class EnemyFactoryAttribute : Attribute
{
    public char[] factoryKeys;

    public EnemyFactoryAttribute(params char[] factoryKeys)
    {
        this.factoryKeys = factoryKeys;
    }
}

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class EnemyFactoryCallbackAttribute : Attribute
{ }

[Serializable]
public class TypeTemplatePair
{
    public string type;
    public GameObject template;
}