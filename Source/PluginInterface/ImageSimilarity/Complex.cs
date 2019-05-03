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
using System;
#endregion

namespace ImageSimilarity
{
    public class Complex
    {
        #region Constructors

        public Complex()
        {
            this.Re = 0;
            this.Im = 0;
        }

        public Complex(double r, double i)
        {
            this.Re = r;
            this.Im = i;
        }

        public Complex(double r)
        {
            this.Re = r;
            this.Im = 0;
        }

        #endregion

        #region Public Properties

        public double Re { get; set; }

        public double Im { get; set; }

        #endregion

        #region Static Methods

        public static Complex operator -(Complex a)
        {
            return new Complex(-a.Re, -a.Im);
        }

        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.Re + b.Re, a.Im + b.Im);
        }

        public static Complex operator +(Complex a, double b)
        {
            return new Complex(a.Re + b, a.Im);
        }

        public static Complex operator +(double b, Complex a)
        {
            return new Complex(a.Re + b, a.Im);
        }

        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.Re - b.Re, a.Im - b.Im);
        }

        public static Complex operator -(Complex a, double b)
        {
            return new Complex(a.Re - b, a.Im);
        }

        public static Complex operator -(double b, Complex a)
        {
            return new Complex(b - a.Re, -a.Im);
        }

        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex((a.Re * b.Re) - (a.Im * b.Im), (a.Re * b.Im) + (a.Im * b.Re));
        }

        public static Complex operator *(Complex a, double b)
        {
            return new Complex(a.Re * b, a.Im * b);
        }

        public static Complex operator *(double b, Complex a)
        {
            return new Complex(a.Re * b, a.Im * b);
        }

        public static Complex operator /(Complex a, Complex b)
        {
            double denom = (b.Re * b.Re) + (b.Im * b.Im);
            if (denom == 0)
            {
                throw new DivideByZeroException();
            }

            return new Complex(((a.Re * b.Re) + (a.Im * b.Im)) / denom, ((a.Im * b.Re) - (a.Re * b.Im)) / denom);
        }

        public static Complex operator /(Complex a, double b)
        {
            return new Complex(a.Re / b, a.Im / b);
        }

        public static Complex operator /(double b, Complex a)
        {
            return new Complex(b / a.Re, b / a.Im);
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return this.Re + " + " + this.Im + "i";
        }

        public double GetModulus()
        {
            return Math.Sqrt((this.Re * this.Re) + (this.Im * this.Im));
        }

        #endregion
    }
}
