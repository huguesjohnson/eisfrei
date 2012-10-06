/*
Eisfrei: Herzog Zwei ROM Editor
Copyright (c) 2008-2009 Hugues Johnson

Eisfrei is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License version 2 
as published by the Free Software Foundation.

Aridia is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>. 
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
