/*
Eisfrei: Herzog Zwei ROM Editor
 Some code borrowed from Aridia: Phantasy Star III ROM Editor 
 Modifications of original code noted in comments
Copyright (c) 2007-2018 Hugues Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;

using com.huguesjohnson.aridia.MegaDriveIO;

namespace com.huguesjohnson.eisfrei
{
	/// <summary>
	/// Contains static utility methods used by Eisfrei.
	/// </summary>
	public abstract class EisfreiUtils
	{
		private static string dataPath;

		/// <summary>
		/// The full path to where data files are stored.
		/// </summary>
		public static string DataPath
		{
			get
			{
				if(dataPath==null)
				{
					dataPath=Application.ExecutablePath;
					int indexOf=dataPath.LastIndexOf(@"\")+1;
					dataPath=dataPath.Substring(0,indexOf)+@"data\";
				}
				return(dataPath);
			}
		}

		/// <summary>
		/// Deserialize an XML file.
		/// </summary>
		/// <param name="serializer">The XmlSerializer to use.</param>
		/// <param name="fileName">The file to read from.</param>
		/// <returns></returns>
		private static object deserialize(XmlSerializer serializer,string fileName)
		{
			FileStream stream=null;
			try
			{
				stream=new FileStream(DataPath+fileName+".xdata",FileMode.Open,FileAccess.Read);
				return(serializer.Deserialize(stream));
			}
			finally
			{
				if(stream!=null){ stream.Close(); }
			}
		}

		/// <summary>
		/// Creates an XmlSerializer for collections of BaseMDData.
		/// </summary>
		/// <returns>An XmlSerializer for collections of BaseMDData.</returns>
		private static XmlSerializer getBaseMDDataCollectionSerializer()
		{
			return(new XmlSerializer(typeof(BaseMDDataCollection),new Type[]{(typeof(BaseMDData)),(typeof(MDString)),(typeof(MDInteger))}));
		}

		/// <summary>
		/// Creates an XmlSerializer for collections of BaseMDData.
		/// </summary>
		/// <returns>An XmlSerializer for collections of BaseMDData.</returns>
		private static XmlSerializer getDataRangeCollectionSerializer()
		{
			return(new XmlSerializer(typeof(DataRangeCollection),new Type[]{(typeof(DataRange))}));
		}
		
		/// <summary>
		/// Creates an XmlSerializer for collections of LookupValue.
		/// </summary>
		/// <returns>An XmlSerializer for collections of LookupValue.</returns>
		private static XmlSerializer getLookupValueCollectionSerializer()
		{
			return(new XmlSerializer(typeof(LookupValueCollection),new Type[]{(typeof(LookupValue))}));
		}

		/// <summary>
		/// Load list items for the dialog list view.
		/// </summary>
		/// <param name="romIO">The MDBinaryRomIO to load values from.</param>
		/// <returns>ListViewItem[]</returns>
		public static ListViewItem[] getTextListItems(MDBinaryRomIO romIO)
		{
			ArrayList items=new ArrayList();
			DataRangeCollection collection=(DataRangeCollection)deserialize(getDataRangeCollectionSerializer(),"Text-DataRanges");
			DataRange[] allItems=collection.getAll();
			int count=allItems.Length;
			for(int index=0;index<count;index++)
			{
				DataRange dataRange=allItems[index];
				int currentOffset=dataRange.StartAddress-1;
				while(currentOffset<dataRange.EndAddress)
				{
					string currentValue=romIO.readString(currentOffset,(dataRange.EndAddress-currentOffset)).Replace("@"," ");
					int length=currentValue.Length;
					int address=currentOffset;
					if(currentValue.Trim().Length>0)
					{
						//try to make editing long strings friendlier
						if(currentValue.Length>32)
						{
							string[] substrings=currentValue.Split('\0');
							int substringCount=substrings.Length;
							int lineCounter=1;
							int substringAddress=address;
							for(int substringIndex=0;substringIndex<substringCount;substringIndex++)
							{
								string substring=substrings[substringIndex];
								//compared to Phantasy Star III the strings in Herzog Zwei are a mess, this block is to deal with the ending credits
								if(substring.StartsWith("\u0001")||substring.StartsWith("\u0002"))
								{
									substring=substring.Substring(1);
									substringAddress++;
								}
								if(substring.Trim().Length>0)
								{
									items.Add(new ListViewItem(new string[]{substring,dataRange.Description+" [Line: "+lineCounter+"]",substringAddress.ToString(),substring.Length.ToString()}));
									lineCounter++;
								}
								substringAddress+=substring.Length+1;
							}
						}
						else
						{
							items.Add(new ListViewItem(new string[]{currentValue,dataRange.Description,address.ToString(),length.ToString()}));
						}
					}
					currentOffset+=length+1;
				}				
			}
			return((ListViewItem[])items.ToArray(typeof(ListViewItem)));
		}

		/// <summary>
		/// Load list items for the dialog list view.
		/// </summary>
		/// <param name="romIO">The MDBinaryRomIO to load values from.</param>
		/// <returns>ListViewItem[]</returns>
		public static ListViewItem[] getGeneralTextListItems(MDBinaryRomIO romIO)
		{
			string categoryName="General-Text";
			LookupValueCollection collection=(LookupValueCollection)deserialize(getLookupValueCollectionSerializer(),categoryName);
			LookupValue[] allItems=collection.getAll();
			int size=allItems.Length;
			ListViewItem[] items=new ListViewItem[size];
			for(int index=0;index<size;index++)
			{
				int address=allItems[index].IntValue;
				string description=allItems[index].Description;
				int length;
				int indexOf=description.IndexOf(" (");
				if(indexOf>0)
				{
					length=description.Substring(0,indexOf).Length;
				}
				else
				{
					length=description.Length;
				}
				string currentValue=romIO.readString(address,length).Replace("@"," "); 
				items[index]=new ListViewItem(new string[]{currentValue,categoryName,address.ToString(),length.ToString()});
			}
			return(items);
		}

		/// <summary>
		/// Load list items for the graphics list view.
		/// </summary>
		/// <param name="name">Name of list to load - "Graphics" or "Sprites".</param>
		/// <param name="romIO">The MDBinaryRomIO to load values from.</param>
		/// <returns>ListViewItem[]</returns>
		public static ListViewItem[] getGraphicsListItems(string name,MDBinaryRomIO romIO)
		{
			DataRangeCollection collection=(DataRangeCollection)deserialize(getDataRangeCollectionSerializer(),name+"-DataRanges");
			DataRange[] allItems=collection.getAll();
			int size=allItems.Length;
			ListViewItem[] items=new ListViewItem[size];
			for(int index=0;index<size;index++)
			{
				items[index]=new ListViewItem(new string[]{allItems[index].Description,allItems[index].StartAddress.ToString(),allItems[index].EndAddress.ToString()});
			}
			return(items);
		}

		/// <summary>
		/// Load a LookupValueCollection into a combobox.
		/// </summary>
		/// <param name="comboBox">The combobox to populate.</param>
		/// <param name="listName">The name of the collection to load.</param>
		public static void loadLookupValues(System.Windows.Forms.ComboBox comboBox,string listName)
		{
			LookupValueCollection collection=(LookupValueCollection)deserialize(getLookupValueCollectionSerializer(),listName);
			LookupValue[] allItems=collection.getAll();
			int size=allItems.Length;
			for(int index=0;index<size;index++)
			{
				comboBox.Items.Add(allItems[index]);
			}
		}

		/// <summary>
		/// Load a LookupValueCollection into a listview.
		/// </summary>
		/// <param name="listView">The listview to populate.</param>
		/// <param name="listName">The name of the collection to load.</param>
		public static void loadLookupValues(System.Windows.Forms.ListView listView,string listName)
		{
			LookupValueCollection collection=getLookupValueCollection(listName);
			LookupValue[] allItems=collection.getAll();
			int size=allItems.Length;
			for(int index=0;index<size;index++)
			{
				listView.Items.Add(new ListViewItem(new string[]{allItems[index].Description,allItems[index].IntValue.ToString()}));
			}
		}

		/// <summary>
		/// Get a LookupValueCollection.
		/// </summary>
		/// <param name="listName">The name of the collection to load.</param>
		/// <returns>The requested LookupValueCollection.</returns>
		public static LookupValueCollection getLookupValueCollection(string listName)
		{
			return((LookupValueCollection)deserialize(getLookupValueCollectionSerializer(),listName));
		}

		/// <summary>
		/// Reads an order name, Herzog Zwei uses non-ASCII characters for numbers in order names.
		/// </summary>
		/// <param name="address">The address to read at.</param>
		/// <param name="address">The length of the string to read.</param>
		/// <param name="romIO">The MDBinaryRomIO to load values from.</param>
		/// <returns>A string containing the order name.</returns>
		public static string readName(int address,int length,MDBinaryRomIO romIO)
		{
			byte[] bytes=romIO.readBytes(address,length);
			//convert to character array
			char[] chars=new char[length];
			for(int index=0;index<length;index++)
			{
				if(((bytes[index]>64)&&(bytes[index]<91))||(bytes[index]==32))
				{
					chars[index]=(char)bytes[index];
				}
				else if((bytes[index]>=183)&&(bytes[index]<193))
				{
					int intValue=(int)bytes[index]-183;
					chars[index]=intValue.ToString().ToCharArray()[0];
				}
				else if(bytes[index]==64)
				{
					chars[index]='0';
				}
				else if(bytes[index]==95)
				{
					chars[index]='-';
				}
				else
				{
					chars[index]='?'; //for debugging, if this happens it means I messed-up
				}
			}
			//convert to a string
			return(new string(chars));
		}

		/// <summary>
		/// Tests whether an MDInteger is valid (greater than zero and less than NumBytes*255+1)
		/// </summary>
		/// <param name="mdInt">The MDInteger to test.</param>
		/// <returns>True if valid.</returns>
		public static bool validateMDInteger(MDInteger mdInt)
		{
			double currentValue=mdInt.CurrentValue;
			double maxValue=Math.Pow(2.0,(double)(8*mdInt.NumBytes));
			return((currentValue>-1.0)&&(currentValue<maxValue));
		}

		/// <summary>
		/// Writes an order name, Herzog Zwei uses non-ASCII characters for numbers in order names.
		/// </summary>
		/// <param name="address">The address to write at.</param>
		/// <param name="newValue">The new value to write.</param>
		/// <param name="romIO">The MDBinaryRomIO to write values to.</param>
		public static void writeName(int address,string newValue,MDBinaryRomIO romIO)
		{
			//create an array to store the bytes
			int maxLength=(int)Enums.OrderLengths.Name;
			byte[] bytes=new byte[maxLength];
			while(newValue.Length<maxLength)
			{
				newValue=newValue+' ';
			}
			//convert the string to chars
			char[] chars=newValue.ToCharArray(0,maxLength);
			//convert to bytes
			for(int index=0;index<maxLength;index++)
			{
				char c=chars[index];
				if(Char.IsLetter(c))
				{
					bytes[index]=(byte)c;
				}
				else if(Char.IsDigit(c))
				{
					int intValue=(int)Char.GetNumericValue(c)+183;
					bytes[index]=(byte)intValue;
				}
				else if(c.Equals('0'))
				{
					bytes[index]=64;
				}
				else if(c.Equals('-'))
				{
					bytes[index]=95;
				}
				else if(c.Equals(' '))
				{
					bytes[index]=32;
				}
				else
				{
					bytes[index]=(byte)'?'; //for debugging, if this happens it means I messed-up
				}
			
			}
			//write the bytes
			romIO.writeBytes(address,bytes);
		}

		/// <summary>
		/// Return the first occurance of some text in a ListView.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <param name="column">The column to search in.</param>
		/// <param name="startIndex">The row index to start searching at.</param>
		/// <param name="listView">The ListView to search.</param>
		/// <returns>The row index the text was found at, -1 if not found.</returns>
		public static int findNextInListView(string text,int column,int startIndex,ListView listView)
		{
			string searchText=text.ToLower();
			int rowCount=listView.Items.Count;
			int index=startIndex;
			int foundAt=-1;
			while((index<rowCount)&&(foundAt==-1))
			{
				string currentValue=listView.Items[index].SubItems[column].Text.ToLower().Replace("\xF8"," ").Replace("\xE4"," ");
				if(currentValue.IndexOf(searchText)>-1)
				{
					foundAt=index;				
				}
				else
				{
					index++;
				}
			}
			return(foundAt);
		}

		/// <summary>
		/// Return the first occurance of some text in a ListView.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <param name="column">The column to search in.</param>
		/// <param name="startIndex">The row index to start searching at.</param>
		/// <param name="listView">The ListView to search.</param>
		/// <returns>The row index the text was found at, -1 if not found.</returns>
		public static int findPreviousInListView(string text,int column,int startIndex,ListView listView)
		{
			string searchText=text.ToLower();
			int rowCount=listView.Items.Count;
			int index=startIndex;
			int foundAt=-1;
			while((index>0)&&(foundAt==-1))
			{
				string currentValue=listView.Items[index].SubItems[column].Text.ToLower().Replace("\xF8"," ").Replace("\xE4"," ");
				if(currentValue.IndexOf(searchText)>-1)
				{
					foundAt=index;				
				}
				else
				{
					index--;
				}
			}
			return(foundAt);
		}

		/// <summary>
		/// see http://support.microsoft.com/kb/319401
		/// </summary>
		/// <param name="e"></param>
		/// <param name="listView">The ListView to sort.</param>
		public static void sortListView(ColumnClickEventArgs e,ListView listView)
		{
			ListViewColumnSorter sorter=(ListViewColumnSorter)listView.ListViewItemSorter;
			if(e.Column==sorter.SortColumn )
			{
				if(sorter.Order==SortOrder.Ascending)
				{
					sorter.Order=SortOrder.Descending;
				}
				else
				{
					sorter.Order=SortOrder.Ascending;
				}
			}
			else
			{
				sorter.SortColumn=e.Column;
				sorter.Order=SortOrder.Ascending;
			}
			// Perform the sort with these new sort options.
			listView.Sort();
		}
	}
}
