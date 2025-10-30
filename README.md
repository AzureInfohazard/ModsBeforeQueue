# ModsBeforeQueue
##*THIS IS A SERVER SIDE MOD*
##Allows Clients to download mods before being put into queue.
Sends a extra server information packet before sending the normal server queue packet
Currently looks to work with no known issues

### How and What
Currently adds a Harmony Prefix to PreFinalizePlayerIdentification checks to see if the server is full (to determine if client will be sent to queue) then sends a ServerIdentity Packet calling CreatePacketIdentification(false) to generate the packet with the HasPrivilege("controlserver") flag set to false, This is the only possible bug I can forsee if the client dosent recieve the second ServerInformation packet AND is a a server member with the controlserver ability, it might not ?display? correctly.

 

*Thats it, Thats All*
