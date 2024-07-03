using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using FallenLands.Utilities;

namespace FallenLands.Players
{
    public class CustomPlayer : ModPlayer
    {
        public Vector2 cameraPos;
        public int? sectionIndex;
        public float zoom = 1f; // esto probablemente puede ser static pero lo dejo por si existe algo de splitscreen lo cual es improbable pero bueno!!1

        // metodo para asignar posicion de la camara, ya que sera un 
        public override void ModifyScreenPosition()
        {
            if (sectionIndex is not null && !FallenLands.sections[sectionIndex.Value].exitBox.Contains(Player.getRect()))
            {
                sectionIndex = null;
            }
            if (sectionIndex is null)
            {
                for (int i = 0; i < FallenLands.sections.Count; i++)
                {
                    Section section = FallenLands.sections[i];
                    if (Player.getRect().Intersects(section.entryBox))
                    {
                        sectionIndex = i;
                        break;
                    }
                }
            }
            Vector2 target = Vector2.Zero;
            target.X = Player.Center.X;
            target.Y = Player.Center.Y;

#if IGNORE_PLAYER_ZOOM
			float zoom = 1f;
#else
            float sectionZoom = Main.ForcedMinimumZoom * MathHelper.Clamp(Main.GameZoomTarget, 1f, 2f);
#endif
            if (sectionIndex is not null)
            {
                sectionZoom *= FallenLands.sections[sectionIndex.Value].zoomModfier;
            }

            float newZoom = MathHelper.Lerp(zoom, sectionZoom, 0.1f);
            newZoom = MathHelper.Clamp(newZoom - zoom, -0.03f, 0.03f) + zoom; // hacer que el cambio del zoom sea menos drastico
            zoom = newZoom;

            float lerpValue = 0.8f;
            bool modifiedX = false, modifiedY = false;
            float distance = Vector2.Distance(cameraPos, target);

            if (sectionIndex is not null)
            {
                Section section = FallenLands.sections[sectionIndex.Value];
                Rectangle entryBox = section.entryBox, exitBox = section.exitBox;
                float marginX = section.margin.X / zoom;
                float marginY = section.margin.Y / zoom;
                if (section.useEntryBoxAsMargin)
                {
                    modifiedX = Clamp(ref target.X, entryBox.Left + marginX, entryBox.Right - marginX);
                    modifiedY = Clamp(ref target.Y, entryBox.Top + marginY, entryBox.Bottom - marginY);
                }
                else
                {
                    modifiedX = Clamp(ref target.X, exitBox.Left + marginX, exitBox.Right - marginX);
                    modifiedY = Clamp(ref target.Y, exitBox.Top + marginY, exitBox.Bottom - marginY);
                }
                if (!modifiedX && !modifiedY)
                {
                    lerpValue += 0.2f / (1f + distance / 32f);
                }
            }
            target.X -= Main.screenWidth / 2 - Main.cameraX;
            target.Y -= Main.screenHeight / 2 - Player.gfxOffY;
            Vector2 newCameraPos = Vector2.Lerp(target, cameraPos, lerpValue);
            if (distance > (Main.screenWidth * Main.screenHeight) / 2.5) // muy lejos!! mejor tpear camara a jugador
            {
                newCameraPos = target;
            }
            else if (!modifiedX && !modifiedY && distance < 512 / sectionZoom) // si no esta en los limites de la seccion y la distancia entre el jugador y la camara es menor a 8 bloques, suavizar movimiento de la camara
                newCameraPos = Vector2.Clamp(newCameraPos - cameraPos, new Vector2(-8f), new Vector2(8)) + cameraPos;

            cameraPos = newCameraPos;
            Main.screenPosition = cameraPos;
        }
        public static bool Clamp(ref float value, float min, float max)
        {
            if (value > max)
            {
                value = max;
                return true;
            }
            if (value < min)
            {
                value = min;
                return true;
            }
            return false;
        }
    }
}
