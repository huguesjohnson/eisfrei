/*
Eisfrei: Herzog Zwei ROM Editor
Copyright (c) 2008-2018 Hugues Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;

namespace com.huguesjohnson.eisfrei
{
	/// <summary>
	/// Contains enumerations used by Eisfrei.
	/// Externalizing these would probably make sense if there was ever any desire to make this application more generic (right now there isn't).
	/// </summary>
	public abstract class Enums
	{
		/// <summary>
		/// Where order attributes are located relative to their starting address.
		/// </summary>
		public enum OrderOffsets : int
		{
			Name=0,
			Cost=10
		}

		/// <summary>
		/// How long order attributes are.
		/// </summary>
		public enum OrderLengths : int
		{
			Name=10,
			Cost=2
		}

		/// <summary>
		/// Where unit attributes are located relative to their starting address.
		/// </summary>
		public enum UnitOffsets : int
		{
			Name=0,
			Cost=8,
			Orders=30
		}

		/// <summary>
		/// How long unit attributes are.
		/// </summary>
		public enum UnitLengths : int
		{
			Name=8,
			Cost=2,
			Orders=2
		}

		/// <summary>
		/// Mappings for which orders can be applied to a unit.
		/// </summary>
		public enum OrderMatrix : int
		{
			BDF1SD=2,
			PWSS10=32768,
			AT101H=512,
			DFF02A=32,
			AF001A=4,
			AT101=8,
			BA001C=1024
		}
	}
}
