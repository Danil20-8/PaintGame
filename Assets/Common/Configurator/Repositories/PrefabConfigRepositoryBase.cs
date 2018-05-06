using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;

namespace Configurator.Repositories
{
    public class PrefabConfigRepositoryBase : MonoBehaviour, IConfigRepository
    {
        public Dictionary<Type, IOption> Load()
        {
            return GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                            .Where(f => f.FieldType.GetInterfaceMap(typeof(IOption)).TargetType != null && f.GetCustomAttributes(typeof(SerializeField), false).Any())
                            .ToDictionary(f => f.FieldType, f => f.GetValue(this) as IOption);
        }

        public void Save(Dictionary<Type, IOption> options)
        {
            throw new NotImplementedException();
        }
    }
}
