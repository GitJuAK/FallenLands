using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace FallenLands.Items
{
    public class SolarStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 pos = new Vector2(velocity.X * (float)4.25, velocity.Y * (float)4.25);
            position += pos;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
        public override void SetDefaults()
        {
            Item.damage = 650;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 27;
            Item.height = 27;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6.5f;
            Item.value = Item.sellPrice(copper: 11, silver: 67, gold: 7);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.maxStack = 1;
            Item.shoot = ProjectileID.LunarFlare;
            Item.shootSpeed = 10;
            Item.noMelee = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Ruby, 1);
            recipe.AddIngredient(ItemID.FragmentVortex, 10);
            recipe.Register();
        }
    }
}