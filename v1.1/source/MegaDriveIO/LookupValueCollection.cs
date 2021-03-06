/*
MegaDriveIO: Utilities to read/write a Mega Drive binary ROM image
Originally created for Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2018 Hugues Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections;

namespace com.huguesjohnson.aridia.MegaDriveIO
{
	/// <summary>
	/// Stores a collection of LookupValue objects.
	/// </summary>
	[Serializable]
	public class LookupValueCollection
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public LookupValueCollection()
		{ 
			this.collection=new ArrayList();
		}

		/// <summary>
		/// The underlying collection that stores all the items.
		/// </summary>
		private ArrayList collection;
		/// <summary>
		/// The underlying collection that stores all the items.
		/// </summary>
		public ArrayList Collection
		{
			get{ return(this.collection);}
			set{ this.collection=value; }
		}

		/// <summary>
		/// Adds and item to the collection.
		/// </summary>
		/// <param name="lookupValue"></param>
		public void add(LookupValue lookupValue)
		{
			this.collection.Add(lookupValue);
		}

		/// <summary>
		/// Returns the number of items in the collection.
		/// </summary>
		/// <returns>The number of items in the collection.</returns>
		public int getSize()
		{
			return(this.collection.Count);
		}

		/// <summary>
		/// Returns the first item in the collection with the given description.
		/// </summary>
		/// <param name="description">The description to search for.</param>
		/// <returns>The first item in the collection with the description, otherwise null.</returns>
		public LookupValue getByDescription(string description)
		{
			int size=this.getSize();
			for(int index=0;index<size;index++)
			{
				LookupValue testValue=(LookupValue)this.collection[index];
				if(testValue.Description.Equals(description))
				{
					return(testValue);
				}
			}
			return(null);
		}

		/// <summary>
		/// Returns the first item in the collection with the given value.
		/// </summary>
		/// <param name="description">The value to search for.</param>
		/// <returns>The first item in the collection with the value, otherwise null.</returns>
		public LookupValue getByValue(int intValue)
		{
			int size=this.getSize();
			for(int index=0;index<size;index++)
			{
				LookupValue testValue=(LookupValue)this.collection[index];
				if(testValue.IntValue==intValue)
				{
					return(testValue);
				}
			}
			return(null);
		}

		/// <summary>
		/// Returns all the items in the collection.
		/// </summary>
		/// <returns>All the items in the collection.</returns>
		public LookupValue[] getAll()
		{
			return((LookupValue[])this.collection.ToArray(typeof(LookupValue)));
		}
	}
}
