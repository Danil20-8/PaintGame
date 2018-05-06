using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configurator
{
    public class CommonOptionFactory : ICommonOptionFactory
    {
        IDictionary<Type, IOptionFactory> factories;

        public CommonOptionFactory()
        {
            this.factories = new Dictionary<Type, IOptionFactory>();
        }

        public CommonOptionFactory(IDictionary<Type, IOptionFactory> factories)
        {
            this.factories = factories;
        }

        public TOption GetOption<TOption>() where TOption : class, IOption
        {
            IOptionFactory factory;
            if(!factories.TryGetValue(typeof(TOption), out factory))
            {
                throw new FactoryCannotCreateOptionException(GetType(), typeof(TOption));
            }
            return factory.GetOption() as TOption;
        }

        public CommonOptionFactory Add<TOption>(IOptionFactory factory) where TOption : class, IOption
        {
            factories[typeof(TOption)] = factory;
            return this;
        }
    }
}
