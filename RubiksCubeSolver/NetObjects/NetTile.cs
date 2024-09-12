using System;
using System.Windows.Forms;
using System.Drawing;
using OpenTK;
using static RubixCubeSolver.Objects.RubiksCubePiece;

namespace RubiksCubeSolver.NetObjects
{
    class NetTile
    {
        /// <summary>
        /// Whether color can be changed
        /// </summary>
        bool colorImmutable = false;

        /// <summary>
        /// 1White, 2Red, 3Blue, 4Orange, 5Green, 6Yellow
        /// </summary>
        public int myColor { get; private set; } = 0;
        
        /// <summary>
        /// Gives this class control over the timer on the main form
        /// </summary>
        public static bool runTimer = false;

        /// <summary>
        /// States to the timer whether the tiles need to be brightened or darkened
        /// </summary>
        public static bool darken = false;

        /// <summary>
        /// The button which forms the tile
        /// </summary>
        public Button netTile { get; private set; } = new Button
        {
            Size = new Size(50, 50),
            BackColor = Color.CadetBlue
        };

        /// <summary>
        /// A single tile forming part of a Rubiks Cube Net
        /// </summary>
        /// <param name="positionIn">The position of the tile (x,y)</param>
        /// <param name="defaultColor">If not 0, then fix the colour of the tile to one of the colors on the Rubik's Cube</param>
        public NetTile(int[] positionIn, RubiksCubeColors defaultColor = 0)
        {
            /// Set position
            netTile.Location = new Point(positionIn[0], positionIn[1]);

            /// If the color is set
            if (defaultColor != 0)
            {
                /// Permanently change the color of the tile to whatever default color is
                myColor = (int)defaultColor;
                colorImmutable = true;
                netTile.BackColor = ConvertVector3ToColor(ConvertRubiksCubeEnumToColor(myColor));
            }

            /// Allow the color of the tile to be changed, or darkened, by clicking on it
            netTile.Click += myButton_Click;

            /// Ready and Show tile
            netTile.Enabled = true;
            netTile.Show();

        }

        /// <summary>
        /// Change color of tile when clicked on
        /// </summary>
        private void myButton_Click(object sender, EventArgs e)
        {
            if (!runTimer)
            {
                CycleColor();
            }
            
        }

        /// <summary>
        /// Change the color of the tile, to the next color on the RubiksCube
        /// </summary>
        private void CycleColor()
        {
            if (!colorImmutable)
            {
                myColor = myColor % 6 + 1;
                netTile.BackColor = ConvertVector3ToColor(ConvertRubiksCubeEnumToColor(myColor));
            }

            else
            {
                HighlightTile(true);
            }
            
        }

        /// <summary>
        /// Highlights the tile temporarily. Used to show tile is immutable
        /// </summary>
        private void HighlightTile(bool darken)
        {
            Vector3 thisColor = ConvertRubiksCubeEnumToColor(myColor);
            Vector3 newColor = new Vector3(thisColor);
            float multiplier = darken ? 0.9f : 1.1f;
            
            for (int i = 0; i < 60; i++)
            {
                newColor = newColor * multiplier;
                netTile.BackColor = Color.FromArgb((int)Math.Round(newColor[0] * 255f), (int)Math.Round(newColor[1] * 255f), (int)Math.Round(newColor[2] * 255f));
            }

            for (int i = 0; i < 60; i++)
            {
                newColor = newColor / multiplier;
                netTile.BackColor = Color.FromArgb((int)Math.Round(newColor[0] * 255f), (int)Math.Round(newColor[1] * 255f), (int)Math.Round(newColor[2] * 255f));
            }

            netTile.BackColor = ConvertVector3ToColor(thisColor);

        }

        public void SetPermanentColor(RubiksCubeColors color)
        {
            /// Permanently change the color of the tile to whatever default color is
            myColor = (int)color;
            colorImmutable = true;
            netTile.BackColor = ConvertVector3ToColor(ConvertRubiksCubeEnumToColor(myColor));

        }

        Color ConvertVector3ToColor(Vector3 vectorIn)
        {
            return Color.FromArgb((int)Math.Round(vectorIn[0] * 255f), (int)Math.Round(vectorIn[1] * 255f), (int)Math.Round(vectorIn[2] * 255f));
        }

    }
}

