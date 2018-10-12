using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class EntityFactory
{
    private static Dictionary<char, Type> entities = new Dictionary<char, Type>();
    private static Dictionary<Type, MethodInfo[]> callbacks = new Dictionary<Type, MethodInfo[]>();

    public static IEntity CreateEntity(char type, TypeTemplatePair[] typeTemplates, int id)
    {
        if (!entities.ContainsKey(type))
            return null;

        GameObject template = typeTemplates.Where(x => x.type.Equals(entities[type].Name)).Select(x => x.template).FirstOrDefault();
        if (!template)
            return null;

        GameObject instance = GameObject.Instantiate(template);
        IEntity entity = (IEntity)instance.GetComponentInChildren(entities[type]);
        if (entity == null)
            entity = (IEntity)instance.AddComponent(entities[type]);

        foreach (MethodInfo callback in callbacks[entities[type]])
        {
            if (callback.GetParameters().Length == 2)
                callback.Invoke(entity, new object[] { type, id });
            else if (callback.GetParameters().Length == 1)
                callback.Invoke(entity, new object[] { type });
            else if (callback.GetParameters().Length == 0)
                callback.Invoke(entity, new object[] { });
        }

        return entity;
    }

    // Called on launch
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        // Find all the classes that have the EnemyFactory attribute
        var discoveredEnemies = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                from t in assembly.GetTypes()
                                let attrib = t.GetCustomAttribute<EntityFactoryAttribute>(false)
                                where attrib != null
                                select new { type = t, attribute = attrib };



        foreach (var found in discoveredEnemies)
        {
            if (!typeof(IEntity).IsAssignableFrom(found.type) || !typeof(MonoBehaviour).IsAssignableFrom(found.type))
            {
                Debug.LogWarning("Found EntityFactory attribute on class that doesn't implement IEntity or extend MonoBehaviour, skipping.");
                continue;
            }

            foreach (char c in found.attribute.factoryKeys)
            {
                if (entities.ContainsKey(c))
                {
                    Debug.LogWarning("Key " + c + " already defined for " + entities[c].Name + " when trying to register " + found.type.Name + ", skipping.");
                    continue;
                }

                entities[c] = found.type;
            }

            callbacks[found.type] = (from m in found.type.GetMethods()
                                     let p = m.GetParameters()
                                     where p.Length == 0 || (p.Length < 3 && p[0].ParameterType == typeof(char) && (p.Length == 1 || p[1].ParameterType == typeof(int)))
                                     let attrib = m.GetCustomAttribute<EntityFactoryCallbackAttribute>(true)
                                     where attrib != null
                                     select m).ToArray();
        }
    }
}

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class EntityFactoryAttribute : Attribute
{
    public char[] factoryKeys;

    public EntityFactoryAttribute(params char[] factoryKeys)
    {
        this.factoryKeys = factoryKeys;
    }
}

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class EntityFactoryCallbackAttribute : Attribute
{ }

[Serializable]
public class TypeTemplatePair
{
    public string type;
    public GameObject template;
}