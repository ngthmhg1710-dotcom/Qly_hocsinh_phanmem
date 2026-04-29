using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public static class FontManager
    {
        private static Dictionary<string, PrivateFontCollection> loadedFonts = new Dictionary<string, PrivateFontCollection>();

        public static Font GetFontByName(string fontFileNameWithoutExtension, float size, FontStyle style = FontStyle.Regular)
        {
            string fontFileName = fontFileNameWithoutExtension + ".ttf";
            string fontPath = Path.Combine(Application.StartupPath, fontFileName);

            if (!loadedFonts.ContainsKey(fontFileName))
            {
                PrivateFontCollection pfc = new PrivateFontCollection();
                if (File.Exists(fontPath))
                {
                    pfc.AddFontFile(fontPath);
                    loadedFonts[fontFileName] = pfc;
                }
                else
                {
                    MessageBox.Show($"Font file not found: {fontPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new Font("Arial", size, style);
                }
            }

            return new Font(loadedFonts[fontFileName].Families[0], size, style);
        }
    }
}