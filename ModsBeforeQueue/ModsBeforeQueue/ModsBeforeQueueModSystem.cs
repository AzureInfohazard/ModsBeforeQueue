using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;
using HarmonyLib;
using Vintagestory.Server;
using System.Reflection;
using System;
namespace ModsBeforeQueue
{
    [HarmonyPatch]
    public class ModsBeforeQueueModSystem : ModSystem
    {
        private HarmonyLib.Harmony? _harmony;
        private ICoreServerAPI _api;
        public override bool ShouldLoad(EnumAppSide forSide) => forSide == EnumAppSide.Server;
        // Called on server and client
        // Useful for registering block/entity classes on both sides
        public override void Start(ICoreAPI api)
        {

        }
        public override void StartServerSide(ICoreServerAPI api)
        {
            _api = api;
            _harmony = new Harmony(Mod.Info.ModID);
            _harmony.PatchAll();

        }
       
        public override void StartClientSide(ICoreClientAPI api)
        {

        }
    }
    [HarmonyPatch(typeof(ServerMain), "PreFinalizePlayerIdentification")]
    public static class ModBeforeQueuePatch
    {
        public static bool Prefix(ServerMain __instance, Packet_ClientIdentification packet, ConnectedClient client, string entitlements)
        {
            MethodInfo createPackedid = AccessTools.Method(typeof(ServerMain), "CreatePacketIdentification", new Type[] { typeof(bool) });
            if (createPackedid != null)
            {
                if (__instance.Clients.Count - 1 >= __instance.Config.MaxClients)
                {
                    if (__instance.Config.MaxClientsInQueue > 0)
                    {
                        __instance.SendPacket(client.Id, (Packet_Server)createPackedid.Invoke(__instance, new object[] { false }));
                    }
                }

            }
            return true;
        }
    }
}

