using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace FallenLands.Items
{
    public class ChargedFlower : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 12));
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Default;
            Item.width = 40;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = Item.sellPrice(copper: 40, silver: 30, gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.noMelee = true; 
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FlowerofFrost, 1);
            recipe.AddIngredient(ItemID.FragmentVortex, 10);
            recipe.Register();
        }

        public override bool CanUseItem(Player player)
        {
            Vector2 closestTeleportPosition = ClosestTeleport(player); // Find the nearest teleporter from the player
            /*if (player.name != "Fallen") // If the player's name is not "Fallen"
            {
                Main.NewText("You aren't hhh!", Color.Red);
                return false;
            }*/

            if (IsPlayerOnTeleporter(player))
            {
                Main.NewText("You are already on a teleporter!", Color.Red);
                return false;
            }

            if (closestTeleportPosition == Vector2.Zero)
            { 
                Main.NewText("Didn't find any teleporter", Color.Red);
                return false;
            }

            else
            {
                NetworkText text = NetworkText.FromLiteral(player.name + " was teleported to the nearest teleporter");  
                player.Teleport(closestTeleportPosition, 1); // Teleport the player to the teleporter
                Main.NewText(text, Color.White);
            }
 
            return true;
        }

        public override bool? UseItem(Player player)
        {
            return true;
        }

        public bool IsPlayerOnTeleporter(Player player)
        {
            Vector2 playerPosition = new Vector2(player.position.X / 16, (player.position.Y / 16) + 3);
            Tile tile = Framing.GetTileSafely((int)playerPosition.X, (int)playerPosition.Y);
            return tile.TileType == TileID.Teleporter;
        }

        public Vector2 ClosestTeleport(Player player)
        {
            
            Vector2 closestTeleportPosition = Vector2.Zero;
            float closestDistanceSquared = float.MaxValue;

            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j); // Obtains the tile at the specified position
                    if (tile.TileType == TileID.Teleporter)
                    {
                        Vector2 teleportPosition = new Vector2(i * 16, (j - 3) * 16);

                        if (player.Center.X - teleportPosition.X < 0) // If the player is to the right of the teleporter
                            teleportPosition.X += 16;

                        else if (player.Center.X - teleportPosition.X > 0) // If the player is to the left of the teleporter
                            teleportPosition.X -= 16;

                        float distanceSquared = Vector2.DistanceSquared(player.Center, teleportPosition);

                        if (distanceSquared < closestDistanceSquared)
                        {
                            closestTeleportPosition = teleportPosition;
                            closestDistanceSquared = distanceSquared;
                        }
                    }
                }
            }

            return closestTeleportPosition;
        }
    }
}