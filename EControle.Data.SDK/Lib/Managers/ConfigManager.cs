using System.Collections.Generic;
using System.Threading.Tasks;
using EControle.Core.Data.Models;

namespace EControle.Data.SDK.Lib.Managers
{
    public class ConfigManager
    {
        private Worker _manager;
        public ConfigManager(Worker _worker)
        {
            _manager = _worker;
        }

        public async Task<List<SedcAccount>> GetSEDCUsers()
        {
            return (List<SedcAccount>) await _manager.SedcAccounts.GetAll();
        }
    }
}