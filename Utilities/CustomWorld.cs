using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.Graphics;
using Terraria.ModLoader;
using Terraria;
using FallenLands.Players;

namespace FallenLands.Utilities
{
    public class CustomWorld : ModSystem
    {
        public override void PostDrawTiles()
        {

            if (Main.LocalPlayer.TryGetModPlayer(out CustomPlayer customPlayer))
            {
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
                if (customPlayer.sectionIndex is not null && !Main.keyState.PressingShift())
                {
                    Section section = FallenLands.sections[customPlayer.sectionIndex.Value];
                    Utils.DrawRect(Main.spriteBatch, section.entryBox, Color.Red);
                    Utils.DrawRect(Main.spriteBatch, section.exitBox, Color.Blue);
                }
                else
                {
                    foreach (var section in FallenLands.sections)
                    {
                        Utils.DrawRect(Main.spriteBatch, section.entryBox, Color.Red);
                        Utils.DrawRect(Main.spriteBatch, section.exitBox, Color.Blue);
                    }
                }
                Main.spriteBatch.End();
            }
        }
        public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
        {
            if (Main.LocalPlayer.TryGetModPlayer(out CustomPlayer customPlayer))
            {
                Transform.Zoom = new Vector2(MathHelper.Clamp(customPlayer.zoom, 0.1f, 10f)); // limite para no matar la ram
            }
            else
            {
                // oops rayo cosmico
            }
        }
        public static void DrawRect(SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            Utils.DrawLine(spriteBatch, new Vector2(rect.Left, rect.Top), new Vector2(rect.Left, rect.Bottom), color, color, 16f);
            Utils.DrawLine(spriteBatch, new Vector2(rect.Left, rect.Bottom), new Vector2(rect.Right, rect.Bottom), color, color, 16f);
            Utils.DrawLine(spriteBatch, new Vector2(rect.Right, rect.Bottom), new Vector2(rect.Right, rect.Top), color, color, 16f);
            Utils.DrawLine(spriteBatch, new Vector2(rect.Right, rect.Top), new Vector2(rect.Left, rect.Top), color, color, 16f);
        }

    }
}
