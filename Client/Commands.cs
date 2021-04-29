using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using kOffDuty.Client.Modules;

namespace kOffDuty.Client
{
    class Commands : BaseScript
    {

        //public Commands()
        //{
        public static void RegisterAllCommands()
        {
            API.RegisterCommand("car", new Action<int, List<object>, string>((src, args, raw) =>
            {
                var text = args[0].ToString();
                var pCoords = API.GetEntityCoords(API.PlayerPedId(), true);
                var pHeading = API.GetEntityHeading(API.PlayerPedId());

                var entityVeh = Functions.SpawnVehicle(text, pCoords.X, pCoords.Y, pCoords.Z, pHeading);

                if (entityVeh != 0)
                {
                    Screen.ShowNotification("Tu as sorti un véhicule");
                }
                else
                {
                    Screen.ShowNotification("Model introuvable..");
                }
            }), false);

            API.RegisterCommand("give_weapon", new Action<int, List<object>, string>((src, args, raw) =>
            {
                var argList = args[0].ToString();
                var pPed = API.PlayerPedId();

                if (argList.Any() && Enum.TryParse(argList, true, out WeaponHash weapon))
                {
                    API.GiveWeaponToPed(pPed, (uint)weapon, 250, true, true);
                    Screen.ShowNotification("Tu as reçu une arme");
                }

            }), false);

            API.RegisterCommand("tp", new Action<int, List<object>, string>((src, args, raw) =>
            {
                var pPed = API.PlayerPedId();
                API.SetEntityCoordsNoOffset(pPed, (float)args[0], (float)args[1], (float)args[2], false, false, false);

            }), false);

            TriggerEvent("chat:addSuggestion", "/car", "Permet de faire spawn une voiture proche de toi.");
            TriggerEvent("chat:addSuggestion", "/give_weapon", "Permet de se give une arme.");
        }
    }
}
