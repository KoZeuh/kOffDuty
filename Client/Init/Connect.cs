using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using kOffDuty.Client.Init;

namespace kOffDuty.Client.Init
{
    public class Connect : BaseScript
    {
        /// <summary>
        /// Gets or sets the global singleton instance reference.
        /// </summary>
        /// <value>
        /// The singleton <see cref="Connect"/> instance.
        /// </value>
        public static Connect Instance { get; protected set; }

        /// <summary>
        /// Primary client entrypoint.
        /// Initializes a new instance of the <see cref="Connect"/> class.
        /// </summary>
        /// 
        public Connect()
        {
            Debug.WriteLine("Init");
            Instance = this;

            Commands.RegisterAllCommands();
            EventHandlers["playerSpawned"] += new Action(Cinematic.StartCinematic);
        }
    }
}
