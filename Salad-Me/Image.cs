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

        public int getWhite()
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

            return (w * 100 / (w + g));
        }

        protected double[] find_center(Bitmap img)
        {
            double[] center = new double[3];
            int i;
            int j;
            double size;
            int tmp;
            int line;
            Color pix;
            Color pix2;
            Color pix3;
            Color pix4;

            i = 0;
            size = 0;
            line = 0;
            while (i < img.Height)
            {
                j = 0;
                tmp = 0;
                while (j < this.img.Width)
                {
                    pix = new Color(this.img.GetPixel((int)j, (int)i));
                    if (((int)(pix.R) + (int)(pix.B) - 20 < (int)(pix.G) || ((pix.R > 200 && pix.B > 200 && pix.G > 200))))
                        tmp++;
                    j++;
                }
                if (tmp > size)
                {
                    line = i;
                    size = tmp;
                }
                i++;
            }
            center[0] = line;
            tmp = 0;
            i = 0;
            while (tmp < 20 && i < this.img.Width - 1)
            {
                pix = new Color(this.img.GetPixel((int)i, (int)line));
                pix2 = new Color(this.img.GetPixel((int)i + 1, (int)line));
                pix3 = new Color(this.img.GetPixel((int)i, (int)line - 1));
                pix4 = new Color(this.img.GetPixel((int)i, (int)line + 1));
                if (((int)(pix.R) < (int)pix.G && (int)(pix.B) - 20 < (int)pix.G || ((pix.R > 200 && pix.B > 200 && pix.G > 200))))
                    tmp++;
                else if (((int)(pix2.R) < (int)pix2.G && (int)(pix2.B) - 20 < (int)pix2.G) || ((pix2.R > 200 && pix2.B > 200 && pix2.G > 200)))
                    tmp++;
                else if (((int)(pix3.R) < (int)pix3.G && (int)(pix3.B) - 20 < (int)pix3.G) || ((pix3.R > 200 && pix3.B > 200 && pix3.G > 200)))
                    tmp++;
                else if (((int)(pix4.R) < (int)pix4.G && (int)(pix4.B) - 20 < (int)pix4.G) || ((pix4.R > 200 && pix4.B > 200 && pix4.G > 200)))
                    tmp++;
                else
                    tmp = 0;
                i++;
            }
            i = i - tmp;
            int k;
            k = img.Width - 1;
            tmp = 0;
            j = 0;
            while (tmp < 20 && k > 0)
            {
                pix = new Color(this.img.GetPixel(k, (int)line));
                pix2 = new Color(this.img.GetPixel((int)k - 1, (int)line));
                pix3 = new Color(this.img.GetPixel((int)k, (int)line - 1));
                pix4 = new Color(this.img.GetPixel((int)k, (int)line + 1));
                if (((int)(pix.R) < (int)pix.G && (int)(pix.B) - 20 < (int)pix.G) || ((pix.R > 200 && pix.B > 200 && pix.G > 200)))
                    tmp++;
                else if (((int)(pix2.R) < (int)pix2.G && (int)(pix2.B) - 20 < (int)pix2.G) || ((pix2.R > 200 && pix2.B > 200 && pix2.G > 200)))
                    tmp++;
                else if (((int)(pix3.R) < (int)pix3.G && (int)(pix3.B) - 20 < (int)pix3.G) || ((pix3.R > 200 && pix3.B > 200 && pix3.G > 200)))
                    tmp++;
                else if (((int)(pix.R) < (int)pix.G && (int)(pix.B) - 20 < (int)pix4.G) || ((pix4.R > 200 && pix4.B > 200 && pix4.G > 200)))
                    tmp++;
                else
                    tmp = 0;
                k--;
                j++;
            }
            k = img.Width - (i + j);
            center[1] = k / 2 + i;
            center[2] = Math.Sqrt(Math.Pow((k / 2), 2) / 2);
            return (center);
        }

        public double getNecrose()
        {
            double[] center = this.find_center(this.img);
            double necro;
            double i;
            double j;
            Color pix;

            j = 0;
            necro = 0;
            int k;
            k = 0;
            while (j < (center[0] + center[2]) && j < this.img.Height)
            {
                i = center[1] - center[2];
                while (i < (center[1] + center[2]) && i < this.img.Width)
                {
                    pix = new Color(this.img.GetPixel((int)i, (int)j));
                    if (pix.R > pix.G && pix.R > pix.B && (pix.R - pix.G) > 30 && (pix.R - pix.B > 30 && pix.R > 30 && pix.R < 170 && (pix.G > pix.B && pix.R > 30 && pix.G > 30 && pix.B > 30)))
                        necro++;
                    i++;
                    k++;
                }
                j++;
            }
            return (necro);
        }
    }
}