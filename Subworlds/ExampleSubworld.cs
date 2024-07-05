using SubworldLibrary;
using System.Collections.Generic;
using Terraria.WorldBuilding;
using Terraria;

namespace FallenLands.Subworlds
{
    public class ExampleSubworld : Subworld
    {
        public override int Width => 1000;
        public override int Height => 1000;

        public override bool ShouldSave => false;
        public override bool NoPlayerSaving => true;

        public override List<GenPass> Tasks => new List<GenPass>()
        {
            new ExampleGenPass()
        };

        // Sets the time to the middle of the day whenever the subworld loads
        public override void OnLoad()
        {
            Main.dayTime = true;
            Main.time = 27000;
        }
    }
}
