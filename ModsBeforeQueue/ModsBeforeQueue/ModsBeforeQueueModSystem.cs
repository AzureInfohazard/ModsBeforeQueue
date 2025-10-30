using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;
using System;
using QueueAPI;
using System.Numerics;
using System.Net.Sockets;
namespace ModsBeforeQueue
{
    public class ModsBeforeQueueModSystem : ModSystem
    {
        public override bool ShouldLoad(EnumAppSide forSide) => forSide == EnumAppSide.Server;

        public override void StartServerSide(ICoreServerAPI api)
        {
            api.ModLoader.GetModSystem<QueueAPIModSystem>().Handler = new ModBeforeQueueJoinHandlerName_thing(api);
        }
   
    }
}

