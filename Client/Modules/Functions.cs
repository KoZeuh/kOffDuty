using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

namespace kOffDuty.Client.Modules
{
    class Functions
    {
        public static void ToggleSound(bool state)
        {
            if (state)
            {
                API.StartAudioScene("MP_LEADERBOARD_SCENE");
            }
            else
            {
                API.StopAudioScene("MP_LEADERBOARD_SCENE");
            }
        }

        public static void DrawAdvancedText(float x, float y , float w, float h,float sc,string text,int r,int g,int b,int a,int font,int jus)
        {
            API.SetTextFont(font);
            API.SetTextProportional(false);
            API.SetTextScale(sc, sc);
            API.N_0x4e096588b13ffeca(jus);
            API.SetTextColour(r, g, b, a);
            API.SetTextDropShadow();
            API.SetTextEdge(1, 0, 0, 0, 255);
            API.SetTextDropShadow();
            API.SetTextOutline();
            API.SetTextEntry("STRING");
            API.AddTextComponentString(text);
            API.DrawText((float)(x - 0.1) + w, (float)(y - 0.02) + h);

        }

        public static async void LoadModel(string model)
        {
            var keyModel = (uint)API.GetHashKey(model);
            API.RequestModel(keyModel);
            while (!API.HasModelLoaded(keyModel))
            {
                await BaseScript.Delay(1);
            }
        }

        public static int SpawnVehicle(string modelName, float x,float y,float z,float heading)
        {
            LoadModel(modelName);
            BaseScript.Delay(1000);
            var newKeyModel = (uint)API.GetHashKey(modelName);

            Debug.WriteLine(modelName, newKeyModel);

            var entitySpawnedWithCommand = API.CreateVehicle(newKeyModel, x,y,z, heading,true,false);

            API.SetPedIntoVehicle(API.PlayerPedId(), entitySpawnedWithCommand, -1);
            API.PlaceObjectOnGroundProperly(entitySpawnedWithCommand);

            if (API.DoesEntityExist(entitySpawnedWithCommand))
            {
                return entitySpawnedWithCommand;
            }
            else
            {
                return 0;
            }
        }

        private static Vector3 getCamDirection()
        {
            var plyPed = API.PlayerPedId();

            var heading = API.GetGameplayCamRelativeHeading() + API.GetEntityHeading(plyPed);

            var pitch = API.GetGameplayCamRelativePitch();

            var coords = new Vector3((float)Math.Sin(heading * Math.PI / 180.0), (float)Math.Cos(heading * Math.PI / 180.0),(float)Math.Sin(pitch * Math.PI / 180.0));

            var len = Math.Sqrt((coords.X * coords.X) + (coords.Y * coords.Y) + (coords.Z * coords.Z));

            return coords;
        }

        public static async void NoClip(bool state)
        {
            var newState = state;
            var pPed = API.PlayerPedId();
            if (newState != true)
            {
                API.FreezeEntityPosition(pPed, false);
                API.SetEntityInvincible(pPed, false);
                API.SetEntityCollision(pPed, true, true);
                API.SetEntityVisible(pPed, true, false);
                API.SetEveryoneIgnorePlayer(API.PlayerId(), false);
                API.SetPoliceIgnorePlayer(API.PlayerId(), false);

            }
            else
            {
                while (newState)
                {
                    API.FreezeEntityPosition(pPed, true);
                    API.SetEntityInvincible(pPed, true);
                    API.SetEntityCollision(pPed, false, false);
                    API.SetEntityVisible(pPed, false, false);
                    API.SetEveryoneIgnorePlayer(API.PlayerId(), true);
                    API.SetPoliceIgnorePlayer(API.PlayerId(), true);
                    var coords = API.GetEntityCoords(pPed, true);
                    var camCoords = getCamDirection();
                    API.SetEntityVelocity(pPed, (float)0.01, (float)0.01, (float)0.01);
                    if (API.IsControlPressed(0, 32))
                    {
                        coords.X = (float)(coords.X + (2.0 * camCoords.X));
                        coords.Y = (float)(coords.Y + (2.0 * camCoords.Y));
                        coords.Z = (float)(coords.Z + (2.0 * camCoords.Z));
                    }

                    if (API.IsControlPressed(0, 269))
                    {
                        coords.X = (float)(coords.X - (2.0 * camCoords.X));
                        coords.Y = (float)(coords.Y - (2.0 * camCoords.Y));
                        coords.Z = (float)(coords.Z - (2.0 * camCoords.Z));
                    }
                    API.SetEntityCoordsNoOffset(pPed, coords.X, coords.Y, coords.Z, true, true, true);

                    await BaseScript.Delay(1);
                }
            }

        }
    }
}
