using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Configurator
{
    public class Configurator : Component, IConfigurator
    {
        Dictionary<Type, IOption> options;
        ICommonOptionFactory optionFactory;

        IConfigRepository configRepository;

        public Configurator(IConfigRepository configRepository, ICommonOptionFactory optionFactory)
        {
            this.configRepository = configRepository;
            options = configRepository.Load();
            this.optionFactory = optionFactory;
        }

        public TOption GetOption<TOption>() where TOption : class, IOption
        {
            IOption option;
            if (!options.TryGetValue(typeof(TOption), out option))
            {
                var tOption = optionFactory.GetOption<TOption>();
                options.Add(typeof(TOption), tOption);
                return tOption;
            }
            else
                return (TOption)option;   
        }

        public void Save()
        {
            configRepository.Save(options);
        }
    }
}
