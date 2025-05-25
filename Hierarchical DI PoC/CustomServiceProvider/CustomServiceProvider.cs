//namespace ToSic.HierarchicalDI.CustomServiceProvider;
//internal class CustomServiceProvider(IServiceProvider realSp): IServiceProvider
//{
//    public object? GetService(Type serviceType)
//    {
//        UseCount++;
//        return realSp.GetService(serviceType) ??
//               throw new InvalidOperationException($"Service of type {serviceType.FullName} not found in the service provider.");
//    }

//    public static int UseCount { get; private set; }
//}
