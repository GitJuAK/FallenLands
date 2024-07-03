using System.Collections.Generic;
using FallenLands.Utilities;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace FallenLands
{
    public class FallenLands : Mod
    {
        public static List<Section> sections = new List<Section>();
        public override void Load()
        {
            // poner rectangulos en coordenadas de bloques!! ya que son multiplicadas por 16 despues para convertirlas a de mundo
            sections.Add(new Section(new Rectangle(100, 0, 130, 130), 0.5f));
            sections.Add(new Section(new Rectangle(230, 0, 130, 130)));
            sections.Add(new Section(new Rectangle(100, 130, 130, 130), 2f));
            sections.Add(new Section(new Rectangle(230, 130, 130, 130)));
        }
    }
}