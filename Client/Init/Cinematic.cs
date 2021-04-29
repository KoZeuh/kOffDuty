using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using kOffDuty.Client.Modules;

using static kOffDuty.Client.Config;
using static kOffDuty.Client.Sync;

namespace kOffDuty.Client.Init
{
    class Cinematic
    {


        public async static void StartCinematic()
        {
            Functions.ToggleSound(true);

            while (API.GetIsLoadingScreenActive())
            {
                await BaseScript.Delay(100);
                API.ShutdownLoadingScreen();
                API.ShutdownLoadingScreenNui();
            }

            if (!API.IsPlayerSwitchInProgress())
            {
                API.SwitchOutPlayer(API.PlayerPedId(), 0, 1);
                ConfigTable.InCinematic = true;
            }

            while (API.GetPlayerSwitchState() != 5)
            {
                await BaseScript.Delay(1);
            }

            while (ConfigTable.InCinematic)
            {
                await BaseScript.Delay(1);

                var pPed = API.PlayerPedId();
                Functions.DrawAdvancedText((float)0.488, (float)0.816, (float)0.105, (float)0.0010, (float)2.0, "~g~E~s~ pour valider ton entrée", 255, 255, 255, 255, 6, 0);

                if (API.IsControlJustPressed(0, 38))
                {
                    API.HideHudAndRadarThisFrame();
                    API.SetDrawOrigin((float)0.0, (float)0.0, (float)0.0, 0);

                    while (!API.IsScreenFadedIn())
                    {
                        await BaseScript.Delay(0);
                        API.HideHudAndRadarThisFrame();
                        API.SetDrawOrigin((float)0.0, (float)0.0, (float)0.0, 0);
                    }
                    API.SwitchInPlayer(pPed);
                    API.ClearPedTasksImmediately(pPed);
                    API.ClearPlayerWantedLevel(pPed);
                    API.SetMaxWantedLevel(0);
                    API.RemoveAllPedWeapons(pPed, true);
                    API.ClearPlayerWantedLevel(API.PlayerId());
                    API.NetworkSetFriendlyFireOption(true);
                    API.SetCanAttackFriendly(pPed, true, true);
                    API.ClearOverrideWeather();
                    API.ClearWeatherTypePersist();
                    API.SetWeatherTypePersist("EXTRASUNNY");
                    API.SetWeatherTypeNow("EXTRASUNNY");
                    API.SetWeatherTypeNowPersist("EXTRASUNNY");
                    API.NetworkOverrideClockTime(8, 12, 0);
                    API.NetworkSetTalkerProximity((float)15.0);
                    API.StopScreenEffect("MP_OrbitalCannon");
                    ConfigTable.InCinematic = false;
                    Commands.RegisterAllCommands();
                    AfterCinematic();
                }
            }

                
        }


        static async void AfterCinematic()
        {
            while (API.GetPlayerSwitchState() != 12)
            {
                await BaseScript.Delay(500);
            }
            while (ConfigTable.InCinematic)
            {
                await BaseScript.Delay(500);
            }
            var pPed = API.PlayerPedId();
            var pCoords = API.GetEntityCoords(pPed, true);
            var cam = API.CreateCam("DEFAULT_SCRIPTED_CAMERA", true);
            API.RenderScriptCams(true, false, 0, false, false);
            API.SetCamCoord(cam, 411.85f, -998.96f, -99.50f);
            API.SetCamRot(cam, (float)0.0,(float)0.0,(float)0.0,(int)ConfigTable.headingCameraAfterCinematic);
            API.FreezeEntityPosition(pPed, true);
            API.SetEntityHeading(pPed, (float)261.22);
            Game.PlayerPed.Position = (Vector3)ConfigTable.posAfterCinematic;

            ConfigSync.delta = 1;
        }
    }
}
