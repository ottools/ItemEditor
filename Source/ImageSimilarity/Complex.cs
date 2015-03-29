#region Licence
/**
* Copyright (C) 2015 Nailson S. <nailsonnego@gmail.com>
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

using System;
using System.Collections.Generic;
using System.Text;

namespace ImageSimilarity
{
	public class Complex
	{
		private double m_Re;

		public double Re
		{
			get { return m_Re; }
			set { m_Re = value; }
		}
		private double m_Im;

		public double Im
		{
			get { return m_Im; }
			set { m_Im = value; }
		}
		public Complex()
		{
		}
		public Complex(double r, double i)
		{
			m_Re = r;
			m_Im = i;
		}
		public Complex(double r)
		{
			m_Re = r;
			m_Im = 0;
		}
		public double GetModulus()
		{
			return Math.Sqrt(m_Re * m_Re + m_Im * m_Im);
		}
		public override string ToString()
		{
			return m_Re + " + " + m_Im + "i";
		}
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
			return new Complex(a.Re * b.Re - a.Im * b.Im, a.Re * b.Im + a.Im * b.Re);
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
			double denom = b.Re * b.Re + b.Im * b.Im;
			if (denom == 0)
			{
				throw new DivideByZeroException();
			}
			return new Complex((a.Re * b.Re + a.Im * b.Im) / denom, (a.Im * b.Re - a.Re * b.Im) / denom);
		}
		public static Complex operator /(Complex a, double b)
		{
			return new Complex(a.Re / b, a.Im / b);
		}
		public static Complex operator /(double b, Complex a)
		{
			return new Complex(b / a.Re, b / a.Im);
		}
	}
}
