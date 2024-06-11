using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shimy.LethalCompanyMod
{
    internal class ConfigController
    {
        private ConfigEntry<string> ServerNameCfg;
        internal string ServerName
        {
            get
            {
                if(ServerNameCfg.Value == "")
                {
                    return (string)ServerNameCfg.DefaultValue;
                }
                return ServerNameCfg.Value;
            }
            set
            {
                ServerNameCfg.Value = value;
            }
        }
        public ConfigController(ConfigFile Config) 
        {
            ServerNameCfg = Config.Bind("Server Settings", "Server name", "Default Server Name", "Server name, overwrites the menu input in game.");
        }
    }
}
