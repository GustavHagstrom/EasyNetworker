using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNetworker.Shared;

public delegate object ServiceFactory(Type ServiceType);
public static class ServiceFactoryExtensions
{
    public static T GetInstance<T>(this ServiceFactory factory) => (T)factory(typeof(T));
    public static IEnumerable<T> GetInstances<T>(this ServiceFactory factory) => (IEnumerable<T>)factory(typeof(IEnumerable<T>));
}