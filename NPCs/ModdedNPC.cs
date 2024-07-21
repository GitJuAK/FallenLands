using FallenLands.Items;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;



namespace FallenLands.NPCs
{
    public class ModdedNPC : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Merchant)    // If the NPC is the Merchant and is called Harold
            {
                shop.Add(ItemID.AmberStaff);   
                shop.Add(ModContent.ItemType<ChargedFlower>());
                shop.GetEntry(ItemID.MiningHelmet).Disable(); 
            }         
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if(npc.IsABoss())
            {
                npcLoot.RemoveWhere(rule => rule is LeadingConditionRule r); // This will remove all LeadingConditionRules from the loot table.
                npcLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule r2); // This will remove all ItemDropWithConditionRules from the loot table.
                npcLoot.RemoveWhere(rule => rule is CommonDrop r3); // This will remove all CommonDrop rules from the loot table.
                npcLoot.RemoveWhere(rule => rule is DropOneByOne r4); // This will remove all DropOneByOne rules from the loot table.
                npcLoot.RemoveWhere(rule => rule is DropBasedOnExpertMode r5); // This will remove all DropBasedOnExpertMode rules from the loot table.
                npcLoot.RemoveWhere(rule => rule is DropBasedOnMasterMode r6); // This will remove all DropBasedOnMasterMode rules from the loot table.
                npcLoot.RemoveWhere(rule => rule is DropBasedOnMasterAndExpertMode r7); // This will remove all DropBasedOnWorldDifficulty rules from the loot table.
            }

            if(npc.netID == ModContent.NPCType<CalamityMod.NPCs.DesertScourge.DesertScourgeHead>())
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SolarStaff>(), 4));
            }
        }
    }
}
