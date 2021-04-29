using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

namespace kOffDuty.Client
{
    class Config
    {
        public static class ConfigTable
        {
            public static bool InCinematic { get; set; }

            public static object posAfterCinematic = new Vector3(408.99f, -999.09f, -100.0f);
            public static object posCameraAfterCinematic = new Vector3(411.85f, -998.96f, -99.50f);
            public static float headingCameraAfterCinematic = (float)89.35;
        }
    }
}
