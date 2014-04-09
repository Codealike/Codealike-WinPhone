namespace Codealike.Tests.TestUtils
{
    using System.Reflection;

    using Ninject;

    public static class NinjectIoC
    {
        public static IKernel Kernel;

        static NinjectIoC()
        {
            Reset();
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        public static void Reset()
        {
            Kernel = new StandardKernel();
            Kernel.Load(Assembly.GetExecutingAssembly());
        }
    }
}