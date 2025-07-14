namespace SDK_Demo.Common
{
    public interface ISdkModule
    {
        string ModuleName { get; }
        void Initialize();
        bool IsInitialized { get; }
    }
}


