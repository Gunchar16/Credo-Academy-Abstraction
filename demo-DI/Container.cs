using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using demo_DI.Abstractions;

namespace demo_DI
{
    public class Container : IoContainer
    {
        public LinkedList<object> list;
        public Container()
        {
            list = new LinkedList<object>();
        }
        void IoContainer.Register<T>()
        {
            list.AddLast(Activator.CreateInstance(typeof(T)) as T);
        }

        void IoContainer.Register<T, R>()
        {
            if(typeof(T).IsAssignableFrom(typeof(R)))
            {
                list.AddLast((T)Activator.CreateInstance(typeof(R)));
            }
        }

        void IoContainer.Register<T>(Func<T> factory)
        {
            list.AddLast(factory.Invoke());
        }

        T IoContainer.Resolve<T>()
        {
            foreach(var item in list)
            {
                if (typeof(T).IsAssignableFrom(item.GetType()))
                    return (T)item;
            }
            throw new Exception();
        }
    }
}
