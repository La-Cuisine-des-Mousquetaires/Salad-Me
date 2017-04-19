using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;

namespace Salad_Me
{
    class Image
    {
        protected Bitmap img;

        public Image(Bitmap img)
        {
            this.img = img;
        }

        public bool isGreen()
        {
            int g;
            int w;
            int y;
            int x;

            g = 0;
            w = 0;
            y = 0;
       
            while (y < this.img.Height)
            {
                x = 0;
                while (x < this.img.Width)
                {
                    Color pix = new Color(this.img.GetPixel(x, y));
                    if ((int)(pix.R) + (int)(pix.B -  20) < (int)pix.G)
                        g++;
                    else if ((pix.R > 200 && pix.B > 200 && pix.G > 200))
                        w++;
                    x++;
                }
                y++;
            }


            if (g > w)
                return (true);
            return (false);
        }
    }
}