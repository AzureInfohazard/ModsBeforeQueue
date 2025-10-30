using Vintagestory.API.Server;
using HarmonyLib;
using Vintagestory.Server;
using System.Reflection;
using QueueAPI;
using QueueAPI.Handlers;
namespace ModsBeforeQueue
{
    public class ModBeforeQueueJoinHandlerName_thing(ICoreServerAPI api) : SimpleJoinQueueHandler(api)
    {
        private readonly ICoreServerAPI api = api;
        private MethodInfo _createPacketIdentificationMethod = AccessTools.Method(typeof(ServerMain), "CreatePacketIdentification",[ typeof(bool) ]);

        public override IJoinQueueHandler.IPlayerConnectResult OnPlayerConnect(Packet_ClientIdentification clientIdentPacket, ConnectedClient client, string entitlements)
        {
            var server = api.GetInternalServer();
            lock (server.ConnectionQueue)
            {

                if (JoinedPlayerCount >= server.Config.MaxClients)
                {
                    var packet = (Packet_Server)_createPacketIdentificationMethod.Invoke(server, [false]);
                    server.SendPacket(client.Id, packet);
                }

            }
            
            return base.OnPlayerConnect(clientIdentPacket, client, entitlements);
        }
    }
}

