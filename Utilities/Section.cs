using Microsoft.Xna.Framework;

namespace FallenLands.Utilities
{
    public readonly struct Section
    {
        // entryBox es un rectangulo (en coordenadas del mundo) que detecta si el rectangulo jugador intersecto con el de la seccion
        // exitBox es un rectangulo (en coordenadas del mundo) que detecta si el rectangulo de la seccion deja de contener el rectangulo del jugador
        // zoomModifier es un multiplicador de zoom que se le añade al jugador, este no modifica el ajuste del zoom del terraria nativo, solo se le multiplica despues del calculo del zoom
        // margin es un par de valores que definen los "limites" de la camara en la seccion un margen de 300 en la X hace que la camara no pueda estar a 300 pixeles horizontalmente del borde del rectangulo de entrada (o salida, ver useEntryBoxAsMargin), de la seccion
        // useEntryBoxAsMargin es un bool que modifica el calculo de os limites de la camara, verdadero hace que use entryBox, falso hace que use exitBox
        public readonly Rectangle entryBox, exitBox;
        public readonly float zoomModfier;
        public readonly Vector2 margin;
        public readonly bool useEntryBoxAsMargin;
        public Section(Rectangle entryBox, float zoomModfier = 1, int exitBoxExtraMargin = 300, Vector2? margin = null, bool useEntryBoxAsMargin = true)
        {
            this.zoomModfier = zoomModfier;
            entryBox.X <<= 4;
            entryBox.Y <<= 4;
            entryBox.Width <<= 4;
            entryBox.Height <<= 4;
            this.entryBox = entryBox;
            exitBox = entryBox;
            exitBox.Inflate(exitBoxExtraMargin, exitBoxExtraMargin);
            this.margin = margin ?? new Vector2(300, 300);
            this.useEntryBoxAsMargin = useEntryBoxAsMargin;
        }
        public Section(Rectangle entryBox, Rectangle exitBox, float zoomModfier = 1, Vector2? margin = null, bool useEntryBoxAsMargin = true)
        {
            this.zoomModfier = zoomModfier;
            entryBox.X <<= 4;
            entryBox.Y <<= 4;
            entryBox.Width <<= 4;
            entryBox.Height <<= 4;

            exitBox.X <<= 4;
            exitBox.Y <<= 4;
            exitBox.Width <<= 4;
            exitBox.Height <<= 4;

            this.entryBox = entryBox;
            this.exitBox = exitBox;

            this.margin = margin ?? new Vector2(300, 300);
            this.useEntryBoxAsMargin = useEntryBoxAsMargin;
        }
    }
}
