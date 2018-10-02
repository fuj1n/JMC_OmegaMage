using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Console = Colorful.Console;

namespace WorldEdit
{
    public class WorldFile
    {
        public string portal;
        public string background;
        public Dictionary<char, string> keys;
        public Dictionary<char, string> labels;
        public Dictionary<string, string> tags;
        public Dictionary<string, char> imageCodes;

        [JsonIgnore]
        public Bitmap missingno;
        [JsonIgnore]
        public Bitmap blank = new Bitmap(32, 32);
        [JsonIgnore]
        public Dictionary<int, char> imageCodesConverted;

        private Dictionary<string, Image> textures = new Dictionary<string, Image>();
        private Room defaultRoom = new Room();

        public void LoadTextures(string directory)
        {
            try
            {
                if (imageCodes != null)
                    imageCodesConverted = imageCodes.Select(ic => new { K = int.Parse("FF" + ic.Key, System.Globalization.NumberStyles.HexNumber), V = ic.Value }).ToDictionary(kvp => kvp.K, kvp => kvp.V);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot read image codes " + e.GetType().Name, Color.DarkRed);
            }

            foreach (string texture in keys.Select(x => x.Value).Concat(tags.Select(x => x.Value)))
            {
                if (texture.StartsWith("$") || textures.ContainsKey(texture))
                    continue;

                LoadTexture(texture, directory);
            }

            LoadTexture(portal, directory);
            LoadTexture(background, directory);

            missingno = new Bitmap(32, 32);
            Graphics g = Graphics.FromImage(missingno);
            g.FillRectangles(Brushes.Magenta, new Rectangle[]
            {
                new Rectangle(0, 0, 16, 16),
                new Rectangle(16, 16, 16, 16)
            });
            g.FillRectangles(Brushes.Black, new Rectangle[]
            {
                new Rectangle(16, 0, 16, 16),
                new Rectangle(0, 16, 16, 16)
            });
            textures["missingno"] = missingno;
        }

        private void LoadTexture(string texture, string directory)
        {
            if (textures.ContainsKey(texture))
                return;

            try
            {
                Console.WriteLineFormatted("Attempting to load texture {0}", Color.Green, Color.White, "\"" + texture + "\"");
                textures[texture] = new Bitmap(Path.Combine(directory, texture));
                Console.WriteLineFormatted("\t\tLoaded a {0} texture with pixel format {1}", Color.White,
                    new Colorful.Formatter(textures[texture].Width + "x" + textures[texture].Height, Color.Cyan),
                    new Colorful.Formatter(textures[texture].PixelFormat, Color.Yellow));
            }
            catch (Exception e)
            {
                Console.WriteLineFormatted("Cannot read \"{0}\" + (" + e.GetType().Name + ")", Color.Green, Color.DarkRed, texture);
            }
        }

        public Image GetTexture(string texture)
        {
            if (texture == "$blank")
                return blank;
            // Done so the palette works correctly
            else if (texture == "$wall")
                texture = tags[defaultRoom.wall];
            else if (texture == "$floor")
                texture = tags[defaultRoom.floor];

            if (!textures.ContainsKey(texture) && tags.ContainsKey(texture))
                texture = tags[texture];

            if (!textures.ContainsKey(texture))
                return missingno;

            return textures[texture];
        }
    }
}
