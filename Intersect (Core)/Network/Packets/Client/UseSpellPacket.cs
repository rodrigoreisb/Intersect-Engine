using MessagePack;
using System;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class UseSpellPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public UseSpellPacket()
        {
        }

        public UseSpellPacket(int slot, Guid targetId)
        {
            Slot = slot;
            TargetId = targetId;
        }

        [Key(0)]
        public int Slot { get; set; }

        [Key(1)]
        public Guid TargetId { get; set; }

        //by rodrigo

        [Key(2)]
        public int targetX { get; set; }
        [Key(3)]
        public int targetY { get; set; }
        [Key(4)]
        public int viewLeft { get; set; }
        [Key(5)]
        public int viewTop { get; set; }

        //end

    }

}
