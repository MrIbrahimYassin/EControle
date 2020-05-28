using EControle.Data.EF.Connection;
using EControle.Data.SDK.Lib.Managers;

namespace EControle.Data.SDK.Lib
{
    public class SDK
    {
        public Worker Worker { get; set; }
        public UsersManager UsersManager { get; set; }
        public AccountManager AccountManager { get; set; }
        public ConfigManager ConfigManager { get; set; }
        public SEDCManager SEDCManager { get; set; }

        public SDK()
        {
            Worker = new Worker(new DataCtx());
            UsersManager = new UsersManager(Worker);
            AccountManager = new AccountManager(Worker);
            ConfigManager = new ConfigManager(Worker);
            SEDCManager = new SEDCManager(Worker);
        }
    }
}