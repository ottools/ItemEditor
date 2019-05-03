#region Licence
/**
* Copyright © 2014-2019 OTTools <https://github.com/ottools/ItemEditor/>
*
* This program is free software; you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation; either version 2 of the License, or
* (at your option) any later version.
*
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License along
* with this program; if not, write to the Free Software Foundation, Inc.,
* 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/
#endregion

#region Using Statements
using System.Drawing;
#endregion

namespace ImageSimilarity
{
    public class ImageSimilarity
    {
        #region Static Methods

        public static void CompareImage(Bitmap bitmap1, Bitmap bitmap2, int blockSize, out double similarity)
        {
            similarity = 0.0;

            Bitmap ff2dbitmap1 = Fourier.fft2d(bitmap1, false);
            Bitmap ff2dbitmap2 = Fourier.fft2d(bitmap2, false);

            double[,] keySignature = ImageUtils.CalculateEuclideanDistance(ff2dbitmap1, blockSize);
            double[,] compareSignature = ImageUtils.CalculateEuclideanDistance(ff2dbitmap2, blockSize);

            similarity = ImageUtils.CompareSignature(keySignature, compareSignature);
        }

        public static void CompareImageRGB(Bitmap bitmap1, Bitmap bitmap2, int blockSize, out double similarity)
        {
            similarity = 0.0;

            Bitmap ff2dbitmap1 = Fourier.fft2dRGB(bitmap1, true);
            Bitmap ff2dbitmap2 = Fourier.fft2dRGB(bitmap2, true);

            double[,] keySignature = ImageUtils.CalculateEuclideanDistance(ff2dbitmap1, blockSize);
            double[,] compareSignature = ImageUtils.CalculateEuclideanDistance(ff2dbitmap2, blockSize);

            similarity = ImageUtils.CompareSignature(keySignature, compareSignature);
        }

        #endregion
    }
}
