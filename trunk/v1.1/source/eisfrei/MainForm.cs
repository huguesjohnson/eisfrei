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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using com.huguesjohnson.aridia.MegaDriveIO;
using com.huguesjohnson.aridia.PaletteEditor;
using com.huguesjohnson.aridia.TileEditor;

namespace com.huguesjohnson.eisfrei
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private MDBinaryRomIO romIO;
		private ListViewColumnSorter listViewNavigateSorter;
		private ListViewColumnSorter listViewGraphicsSorter;
		private ListViewColumnSorter listViewSpritesSorter;
		private ListViewColumnSorter listViewPalettesSorter;
		private ListViewColumnSorter listViewDialogTextSorter;
		private System.Windows.Forms.ListView listViewNavigate;
		private System.Windows.Forms.ColumnHeader columnHeader;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItemEisfrei;
		private System.Windows.Forms.MenuItem menuItemOpenRom;
		private System.Windows.Forms.MenuItem menuItemRepairChecksum;
		private System.Windows.Forms.MenuItem menuItemHomepage;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.StatusBarPanel statusBarPanel;
		private System.Windows.Forms.TabControl tabControlMainContent;
		private System.Windows.Forms.TabPage tabPageMain;
		private System.Windows.Forms.TextBox textBoxCalculatedChecksum;
		private System.Windows.Forms.Label labelCalculatedChecksum;
		private System.Windows.Forms.TextBox textBoxChecksum;
		private System.Windows.Forms.Label labelChecksum;
		private System.Windows.Forms.TextBox textBoxRomHeader;
		private System.Windows.Forms.Label labelRomHeader;
		private System.Windows.Forms.Button buttonChecksum;
		private System.Windows.Forms.Button buttonOpenRom;
		private System.Windows.Forms.TextBox textBoxRomPath;
		private System.Windows.Forms.Label labelRom;
		private System.Windows.Forms.PictureBox pictureBoxWarning;
		private System.Windows.Forms.TextBox textBoxWarningText;
		private System.Windows.Forms.OpenFileDialog openFileRomDialog;
		private System.Windows.Forms.TabPage tabPageOrders;
		private System.Windows.Forms.TabPage tabPageUnits;
		private System.Windows.Forms.TabPage tabPageText;
		private System.Windows.Forms.TabPage tabPageGraphics;
		private System.Windows.Forms.ComboBox comboBoxSelectOrder;
		private System.Windows.Forms.GroupBox groupBoxOrder;
		private System.Windows.Forms.Label labelOrderAddress;
		private System.Windows.Forms.TextBox textBoxOrderAddress;
		private System.Windows.Forms.TextBox textBoxOrderName;
		private System.Windows.Forms.Label labelOrderName;
		private System.Windows.Forms.Label labelOrderNameHelp;
		private System.Windows.Forms.Label labelOrderCostHelp;
		private System.Windows.Forms.TextBox textBoxOrderCost;
		private System.Windows.Forms.Label labelOrderCost;
		private System.Windows.Forms.ComboBox comboBoxSelectUnit;
		private System.Windows.Forms.Label labelSelectUnit;
		private System.Windows.Forms.Label labelUnitCostHelp;
		private System.Windows.Forms.TextBox textBoxUnitCost;
		private System.Windows.Forms.Label labelUnitCost;
		private System.Windows.Forms.Label labelUnitNameHelp;
		private System.Windows.Forms.TextBox textBoxUnitName;
		private System.Windows.Forms.Label labelUnitName;
		private System.Windows.Forms.TextBox textBoxUnitAddress;
		private System.Windows.Forms.Label labelUnitAddress;
		private System.Windows.Forms.GroupBox groupBoxUnitOrders;
		private System.Windows.Forms.CheckBox checkBoxBDF1SD;
		private System.Windows.Forms.CheckBox checkBoxAF001A;
		private System.Windows.Forms.CheckBox checkBoxAT101;
		private System.Windows.Forms.CheckBox checkBoxDFF02A;
		private System.Windows.Forms.CheckBox checkBoxAT101H;
		private System.Windows.Forms.CheckBox checkBoxBA001C;
		private System.Windows.Forms.CheckBox checkBoxPWSS10;
		private System.Windows.Forms.ListView listViewDialogText;
		private System.Windows.Forms.ColumnHeader columnDialogTextHeaderCurrentValue;
		private System.Windows.Forms.ColumnHeader columnDialogTextCategory;
		private System.Windows.Forms.ColumnHeader columnDialogTextAddress;
		private System.Windows.Forms.ColumnHeader columncolumnDialogTextLength;
		private System.Windows.Forms.Label labelSelectOrder;
		private System.Windows.Forms.GroupBox groupBoxUnits;
		private bool disableCheckboxValidation;
		private System.Windows.Forms.ListView listViewGraphics;
		private System.Windows.Forms.Button buttonLaunchTileEditor;
		private System.Windows.Forms.ColumnHeader columnHeaderGraphicsDescription;
		private System.Windows.Forms.ColumnHeader columnHeaderGraphicsStart;
		private System.Windows.Forms.ColumnHeader columnHeaderGraphicsEnd;
		private System.Windows.Forms.TabPage tabPageSprites;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button buttonLaunchTileEditorSprites;
		private System.Windows.Forms.ListView listViewSprites;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.TabPage tabPagePalettes;
		private System.Windows.Forms.Label labelPaletteFind;
		private System.Windows.Forms.Button buttonPalettePrevious;
		private System.Windows.Forms.Button buttonPaletteFind;
		private System.Windows.Forms.TextBox textBoxPaletteFind;
		private System.Windows.Forms.Label labelPalettePreview;
		private System.Windows.Forms.Button buttonLaunchPaletteEditor;
		private System.Windows.Forms.ListView listViewPalettes;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.PictureBox pictureBoxPalettePreview;
		private System.Windows.Forms.Label labelFindText;
		private System.Windows.Forms.Button buttonFindTextPrevious;
		private System.Windows.Forms.Button buttonFindTextNext;
		private System.Windows.Forms.TextBox textBoxFindText;
		private int textFindIndex;
		private int paletteFindIndex;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//setup listview sorters
			this.listViewNavigateSorter=new ListViewColumnSorter();
			this.listViewNavigate.ListViewItemSorter=this.listViewNavigateSorter;
			this.listViewGraphicsSorter=new ListViewColumnSorter();
			this.listViewGraphics.ListViewItemSorter=this.listViewGraphicsSorter;
			this.listViewSpritesSorter=new ListViewColumnSorter();
			this.listViewSprites.ListViewItemSorter=this.listViewSpritesSorter;
			this.listViewPalettesSorter=new ListViewColumnSorter();
			this.listViewPalettes.ListViewItemSorter=this.listViewPalettesSorter;
			this.listViewDialogTextSorter=new ListViewColumnSorter();
			this.listViewDialogText.ListViewItemSorter=this.listViewDialogTextSorter;
			//setup other stuff
			this.tabControlMainContent.SelectedIndex=0;
			this.listViewNavigate.Items[0].Selected=true;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Main");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Graphics");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Orders");
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Palettes");
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Sprites");
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Text");
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Units");
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.listViewNavigate = new System.Windows.Forms.ListView();
			this.columnHeader = new System.Windows.Forms.ColumnHeader();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuItemEisfrei = new System.Windows.Forms.MenuItem();
			this.menuItemOpenRom = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItemRepairChecksum = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItemHomepage = new System.Windows.Forms.MenuItem();
			this.menuItemAbout = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.statusBarPanel = new System.Windows.Forms.StatusBarPanel();
			this.tabControlMainContent = new System.Windows.Forms.TabControl();
			this.tabPageMain = new System.Windows.Forms.TabPage();
			this.textBoxWarningText = new System.Windows.Forms.TextBox();
			this.pictureBoxWarning = new System.Windows.Forms.PictureBox();
			this.labelRomHeader = new System.Windows.Forms.Label();
			this.buttonChecksum = new System.Windows.Forms.Button();
			this.buttonOpenRom = new System.Windows.Forms.Button();
			this.textBoxRomPath = new System.Windows.Forms.TextBox();
			this.textBoxCalculatedChecksum = new System.Windows.Forms.TextBox();
			this.labelCalculatedChecksum = new System.Windows.Forms.Label();
			this.textBoxChecksum = new System.Windows.Forms.TextBox();
			this.labelChecksum = new System.Windows.Forms.Label();
			this.textBoxRomHeader = new System.Windows.Forms.TextBox();
			this.labelRom = new System.Windows.Forms.Label();
			this.tabPageGraphics = new System.Windows.Forms.TabPage();
			this.buttonLaunchTileEditor = new System.Windows.Forms.Button();
			this.listViewGraphics = new System.Windows.Forms.ListView();
			this.columnHeaderGraphicsDescription = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderGraphicsStart = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderGraphicsEnd = new System.Windows.Forms.ColumnHeader();
			this.tabPageOrders = new System.Windows.Forms.TabPage();
			this.groupBoxOrder = new System.Windows.Forms.GroupBox();
			this.labelOrderCostHelp = new System.Windows.Forms.Label();
			this.textBoxOrderCost = new System.Windows.Forms.TextBox();
			this.labelOrderCost = new System.Windows.Forms.Label();
			this.labelOrderNameHelp = new System.Windows.Forms.Label();
			this.textBoxOrderName = new System.Windows.Forms.TextBox();
			this.labelOrderName = new System.Windows.Forms.Label();
			this.textBoxOrderAddress = new System.Windows.Forms.TextBox();
			this.labelOrderAddress = new System.Windows.Forms.Label();
			this.comboBoxSelectOrder = new System.Windows.Forms.ComboBox();
			this.labelSelectOrder = new System.Windows.Forms.Label();
			this.tabPagePalettes = new System.Windows.Forms.TabPage();
			this.pictureBoxPalettePreview = new System.Windows.Forms.PictureBox();
			this.labelPaletteFind = new System.Windows.Forms.Label();
			this.buttonPalettePrevious = new System.Windows.Forms.Button();
			this.buttonPaletteFind = new System.Windows.Forms.Button();
			this.textBoxPaletteFind = new System.Windows.Forms.TextBox();
			this.labelPalettePreview = new System.Windows.Forms.Label();
			this.buttonLaunchPaletteEditor = new System.Windows.Forms.Button();
			this.listViewPalettes = new System.Windows.Forms.ListView();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.tabPageSprites = new System.Windows.Forms.TabPage();
			this.buttonLaunchTileEditorSprites = new System.Windows.Forms.Button();
			this.listViewSprites = new System.Windows.Forms.ListView();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.tabPageText = new System.Windows.Forms.TabPage();
			this.labelFindText = new System.Windows.Forms.Label();
			this.buttonFindTextPrevious = new System.Windows.Forms.Button();
			this.buttonFindTextNext = new System.Windows.Forms.Button();
			this.textBoxFindText = new System.Windows.Forms.TextBox();
			this.listViewDialogText = new System.Windows.Forms.ListView();
			this.columnDialogTextHeaderCurrentValue = new System.Windows.Forms.ColumnHeader();
			this.columnDialogTextCategory = new System.Windows.Forms.ColumnHeader();
			this.columnDialogTextAddress = new System.Windows.Forms.ColumnHeader();
			this.columncolumnDialogTextLength = new System.Windows.Forms.ColumnHeader();
			this.tabPageUnits = new System.Windows.Forms.TabPage();
			this.groupBoxUnits = new System.Windows.Forms.GroupBox();
			this.groupBoxUnitOrders = new System.Windows.Forms.GroupBox();
			this.checkBoxPWSS10 = new System.Windows.Forms.CheckBox();
			this.checkBoxBA001C = new System.Windows.Forms.CheckBox();
			this.checkBoxAT101H = new System.Windows.Forms.CheckBox();
			this.checkBoxDFF02A = new System.Windows.Forms.CheckBox();
			this.checkBoxAT101 = new System.Windows.Forms.CheckBox();
			this.checkBoxAF001A = new System.Windows.Forms.CheckBox();
			this.checkBoxBDF1SD = new System.Windows.Forms.CheckBox();
			this.labelUnitCostHelp = new System.Windows.Forms.Label();
			this.textBoxUnitCost = new System.Windows.Forms.TextBox();
			this.labelUnitCost = new System.Windows.Forms.Label();
			this.labelUnitNameHelp = new System.Windows.Forms.Label();
			this.textBoxUnitName = new System.Windows.Forms.TextBox();
			this.labelUnitName = new System.Windows.Forms.Label();
			this.textBoxUnitAddress = new System.Windows.Forms.TextBox();
			this.labelUnitAddress = new System.Windows.Forms.Label();
			this.comboBoxSelectUnit = new System.Windows.Forms.ComboBox();
			this.labelSelectUnit = new System.Windows.Forms.Label();
			this.openFileRomDialog = new System.Windows.Forms.OpenFileDialog();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
			this.tabControlMainContent.SuspendLayout();
			this.tabPageMain.SuspendLayout();
			this.tabPageGraphics.SuspendLayout();
			this.tabPageOrders.SuspendLayout();
			this.groupBoxOrder.SuspendLayout();
			this.tabPagePalettes.SuspendLayout();
			this.tabPageSprites.SuspendLayout();
			this.tabPageText.SuspendLayout();
			this.tabPageUnits.SuspendLayout();
			this.groupBoxUnits.SuspendLayout();
			this.groupBoxUnitOrders.SuspendLayout();
			this.SuspendLayout();
			// 
			// listViewNavigate
			// 
			this.listViewNavigate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listViewNavigate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeader});
			this.listViewNavigate.FullRowSelect = true;
			this.listViewNavigate.HideSelection = false;
			this.listViewNavigate.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																							 listViewItem1,
																							 listViewItem2,
																							 listViewItem3,
																							 listViewItem4,
																							 listViewItem5,
																							 listViewItem6,
																							 listViewItem7});
			this.listViewNavigate.Location = new System.Drawing.Point(0, 0);
			this.listViewNavigate.MultiSelect = false;
			this.listViewNavigate.Name = "listViewNavigate";
			this.listViewNavigate.Scrollable = false;
			this.listViewNavigate.Size = new System.Drawing.Size(104, 328);
			this.listViewNavigate.TabIndex = 1;
			this.listViewNavigate.View = System.Windows.Forms.View.Details;
			this.listViewNavigate.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewNavigate_ColumnClick);
			this.listViewNavigate.SelectedIndexChanged += new System.EventHandler(this.listViewNavigate_SelectedIndexChanged);
			// 
			// columnHeader
			// 
			this.columnHeader.Text = "Pages";
			this.columnHeader.Width = 203;
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItemEisfrei});
			// 
			// menuItemEisfrei
			// 
			this.menuItemEisfrei.Index = 0;
			this.menuItemEisfrei.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.menuItemOpenRom,
																							this.menuItem3,
																							this.menuItemRepairChecksum,
																							this.menuItem5,
																							this.menuItemHomepage,
																							this.menuItemAbout,
																							this.menuItem8,
																							this.menuItemExit});
			this.menuItemEisfrei.Text = "&Eisfrei";
			// 
			// menuItemOpenRom
			// 
			this.menuItemOpenRom.Index = 0;
			this.menuItemOpenRom.Text = "&Open ROM..";
			this.menuItemOpenRom.Click += new System.EventHandler(this.menuItemOpenRom_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			// 
			// menuItemRepairChecksum
			// 
			this.menuItemRepairChecksum.Enabled = false;
			this.menuItemRepairChecksum.Index = 2;
			this.menuItemRepairChecksum.Text = "&Repair Checksum";
			this.menuItemRepairChecksum.Click += new System.EventHandler(this.menuItemRepairChecksum_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "-";
			// 
			// menuItemHomepage
			// 
			this.menuItemHomepage.Index = 4;
			this.menuItemHomepage.Text = "&Homepage..";
			this.menuItemHomepage.Click += new System.EventHandler(this.menuItemHomepage_Click);
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Index = 5;
			this.menuItemAbout.Text = "&About..";
			this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 6;
			this.menuItem8.Text = "-";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Index = 7;
			this.menuItemExit.Text = "E&xit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 331);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						 this.statusBarPanel});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(682, 16);
			this.statusBar.SizingGrip = false;
			this.statusBar.TabIndex = 3;
			// 
			// statusBarPanel
			// 
			this.statusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusBarPanel.Text = "No ROM open, please open a ROM from the Main tab or menu";
			this.statusBarPanel.ToolTipText = "No ROM open, please open a ROM from the Main tab or menu";
			this.statusBarPanel.Width = 682;
			// 
			// tabControlMainContent
			// 
			this.tabControlMainContent.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControlMainContent.Controls.Add(this.tabPageMain);
			this.tabControlMainContent.Controls.Add(this.tabPageGraphics);
			this.tabControlMainContent.Controls.Add(this.tabPageOrders);
			this.tabControlMainContent.Controls.Add(this.tabPagePalettes);
			this.tabControlMainContent.Controls.Add(this.tabPageSprites);
			this.tabControlMainContent.Controls.Add(this.tabPageText);
			this.tabControlMainContent.Controls.Add(this.tabPageUnits);
			this.tabControlMainContent.Location = new System.Drawing.Point(104, 0);
			this.tabControlMainContent.Name = "tabControlMainContent";
			this.tabControlMainContent.SelectedIndex = 0;
			this.tabControlMainContent.ShowToolTips = true;
			this.tabControlMainContent.Size = new System.Drawing.Size(576, 328);
			this.tabControlMainContent.TabIndex = 2;
			this.tabControlMainContent.SelectedIndexChanged += new System.EventHandler(this.tabControlMainContent_SelectedIndexChanged);
			// 
			// tabPageMain
			// 
			this.tabPageMain.Controls.Add(this.textBoxWarningText);
			this.tabPageMain.Controls.Add(this.pictureBoxWarning);
			this.tabPageMain.Controls.Add(this.labelRomHeader);
			this.tabPageMain.Controls.Add(this.buttonChecksum);
			this.tabPageMain.Controls.Add(this.buttonOpenRom);
			this.tabPageMain.Controls.Add(this.textBoxRomPath);
			this.tabPageMain.Controls.Add(this.textBoxCalculatedChecksum);
			this.tabPageMain.Controls.Add(this.labelCalculatedChecksum);
			this.tabPageMain.Controls.Add(this.textBoxChecksum);
			this.tabPageMain.Controls.Add(this.labelChecksum);
			this.tabPageMain.Controls.Add(this.textBoxRomHeader);
			this.tabPageMain.Controls.Add(this.labelRom);
			this.tabPageMain.Location = new System.Drawing.Point(4, 25);
			this.tabPageMain.Name = "tabPageMain";
			this.tabPageMain.Size = new System.Drawing.Size(568, 299);
			this.tabPageMain.TabIndex = 0;
			this.tabPageMain.Text = "Main";
			this.tabPageMain.ToolTipText = "Main tab, open ROM and fix checksum";
			// 
			// textBoxWarningText
			// 
			this.textBoxWarningText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxWarningText.Enabled = false;
			this.textBoxWarningText.Location = new System.Drawing.Point(88, 224);
			this.textBoxWarningText.Multiline = true;
			this.textBoxWarningText.Name = "textBoxWarningText";
			this.textBoxWarningText.Size = new System.Drawing.Size(352, 72);
			this.textBoxWarningText.TabIndex = 25;
			this.textBoxWarningText.Text = @"Changes to ROM images can not be undone. Some edits may potentially cause the game to be unplayable. Editing something other than a Herzog Zwei BIN image with this program would be a very bad idea. The author of this program is not responsible for damage to any files edited with it. In other words, use at your own risk.";
			// 
			// pictureBoxWarning
			// 
			this.pictureBoxWarning.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxWarning.Image")));
			this.pictureBoxWarning.Location = new System.Drawing.Point(32, 232);
			this.pictureBoxWarning.Name = "pictureBoxWarning";
			this.pictureBoxWarning.Size = new System.Drawing.Size(48, 51);
			this.pictureBoxWarning.TabIndex = 23;
			this.pictureBoxWarning.TabStop = false;
			// 
			// labelRomHeader
			// 
			this.labelRomHeader.Location = new System.Drawing.Point(16, 32);
			this.labelRomHeader.Name = "labelRomHeader";
			this.labelRomHeader.Size = new System.Drawing.Size(104, 20);
			this.labelRomHeader.TabIndex = 17;
			this.labelRomHeader.Text = "ROM Header: ";
			this.labelRomHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonChecksum
			// 
			this.buttonChecksum.Enabled = false;
			this.buttonChecksum.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonChecksum.Image = ((System.Drawing.Image)(resources.GetObject("buttonChecksum.Image")));
			this.buttonChecksum.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonChecksum.Location = new System.Drawing.Point(432, 80);
			this.buttonChecksum.Name = "buttonChecksum";
			this.buttonChecksum.Size = new System.Drawing.Size(120, 32);
			this.buttonChecksum.TabIndex = 16;
			this.buttonChecksum.Text = "Repair Checksum";
			this.buttonChecksum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonChecksum.Click += new System.EventHandler(this.buttonChecksum_Click);
			// 
			// buttonOpenRom
			// 
			this.buttonOpenRom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonOpenRom.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenRom.Image")));
			this.buttonOpenRom.Location = new System.Drawing.Point(520, 8);
			this.buttonOpenRom.Name = "buttonOpenRom";
			this.buttonOpenRom.Size = new System.Drawing.Size(32, 20);
			this.buttonOpenRom.TabIndex = 15;
			this.buttonOpenRom.Click += new System.EventHandler(this.buttonOpenRom_Click);
			// 
			// textBoxRomPath
			// 
			this.textBoxRomPath.Location = new System.Drawing.Point(120, 8);
			this.textBoxRomPath.Name = "textBoxRomPath";
			this.textBoxRomPath.ReadOnly = true;
			this.textBoxRomPath.Size = new System.Drawing.Size(392, 20);
			this.textBoxRomPath.TabIndex = 14;
			this.textBoxRomPath.Text = "";
			// 
			// textBoxCalculatedChecksum
			// 
			this.textBoxCalculatedChecksum.Location = new System.Drawing.Point(120, 56);
			this.textBoxCalculatedChecksum.Name = "textBoxCalculatedChecksum";
			this.textBoxCalculatedChecksum.ReadOnly = true;
			this.textBoxCalculatedChecksum.Size = new System.Drawing.Size(144, 20);
			this.textBoxCalculatedChecksum.TabIndex = 22;
			this.textBoxCalculatedChecksum.Text = "";
			// 
			// labelCalculatedChecksum
			// 
			this.labelCalculatedChecksum.Location = new System.Drawing.Point(288, 56);
			this.labelCalculatedChecksum.Name = "labelCalculatedChecksum";
			this.labelCalculatedChecksum.Size = new System.Drawing.Size(120, 20);
			this.labelCalculatedChecksum.TabIndex = 21;
			this.labelCalculatedChecksum.Text = "Calculated Checksum: ";
			this.labelCalculatedChecksum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxChecksum
			// 
			this.textBoxChecksum.Location = new System.Drawing.Point(408, 56);
			this.textBoxChecksum.Name = "textBoxChecksum";
			this.textBoxChecksum.ReadOnly = true;
			this.textBoxChecksum.Size = new System.Drawing.Size(144, 20);
			this.textBoxChecksum.TabIndex = 20;
			this.textBoxChecksum.Text = "";
			// 
			// labelChecksum
			// 
			this.labelChecksum.Location = new System.Drawing.Point(16, 56);
			this.labelChecksum.Name = "labelChecksum";
			this.labelChecksum.Size = new System.Drawing.Size(104, 20);
			this.labelChecksum.TabIndex = 19;
			this.labelChecksum.Text = "Stored Checksum: ";
			this.labelChecksum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxRomHeader
			// 
			this.textBoxRomHeader.Location = new System.Drawing.Point(120, 32);
			this.textBoxRomHeader.Name = "textBoxRomHeader";
			this.textBoxRomHeader.ReadOnly = true;
			this.textBoxRomHeader.Size = new System.Drawing.Size(432, 20);
			this.textBoxRomHeader.TabIndex = 18;
			this.textBoxRomHeader.Text = "";
			// 
			// labelRom
			// 
			this.labelRom.Location = new System.Drawing.Point(16, 8);
			this.labelRom.Name = "labelRom";
			this.labelRom.Size = new System.Drawing.Size(104, 20);
			this.labelRom.TabIndex = 13;
			this.labelRom.Text = "Editing ROM: ";
			this.labelRom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabPageGraphics
			// 
			this.tabPageGraphics.Controls.Add(this.buttonLaunchTileEditor);
			this.tabPageGraphics.Controls.Add(this.listViewGraphics);
			this.tabPageGraphics.Location = new System.Drawing.Point(4, 25);
			this.tabPageGraphics.Name = "tabPageGraphics";
			this.tabPageGraphics.Size = new System.Drawing.Size(568, 299);
			this.tabPageGraphics.TabIndex = 4;
			this.tabPageGraphics.Text = "Graphics";
			this.tabPageGraphics.ToolTipText = "Edit graphics";
			// 
			// buttonLaunchTileEditor
			// 
			this.buttonLaunchTileEditor.Enabled = false;
			this.buttonLaunchTileEditor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonLaunchTileEditor.Image = ((System.Drawing.Image)(resources.GetObject("buttonLaunchTileEditor.Image")));
			this.buttonLaunchTileEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonLaunchTileEditor.Location = new System.Drawing.Point(440, 264);
			this.buttonLaunchTileEditor.Name = "buttonLaunchTileEditor";
			this.buttonLaunchTileEditor.Size = new System.Drawing.Size(120, 32);
			this.buttonLaunchTileEditor.TabIndex = 17;
			this.buttonLaunchTileEditor.Text = "Launch Tile Editor";
			this.buttonLaunchTileEditor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonLaunchTileEditor.Click += new System.EventHandler(this.buttonLaunchTileEditor_Click);
			// 
			// listViewGraphics
			// 
			this.listViewGraphics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeaderGraphicsDescription,
																							   this.columnHeaderGraphicsStart,
																							   this.columnHeaderGraphicsEnd});
			this.listViewGraphics.FullRowSelect = true;
			this.listViewGraphics.HideSelection = false;
			this.listViewGraphics.Location = new System.Drawing.Point(8, 8);
			this.listViewGraphics.MultiSelect = false;
			this.listViewGraphics.Name = "listViewGraphics";
			this.listViewGraphics.Size = new System.Drawing.Size(552, 248);
			this.listViewGraphics.TabIndex = 7;
			this.listViewGraphics.View = System.Windows.Forms.View.Details;
			this.listViewGraphics.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewGraphics_ColumnClick);
			this.listViewGraphics.SelectedIndexChanged += new System.EventHandler(this.listViewGraphics_SelectedIndexChanged);
			// 
			// columnHeaderGraphicsDescription
			// 
			this.columnHeaderGraphicsDescription.Text = "Description";
			this.columnHeaderGraphicsDescription.Width = 298;
			// 
			// columnHeaderGraphicsStart
			// 
			this.columnHeaderGraphicsStart.Text = "Start Address";
			this.columnHeaderGraphicsStart.Width = 119;
			// 
			// columnHeaderGraphicsEnd
			// 
			this.columnHeaderGraphicsEnd.Text = "End Address";
			this.columnHeaderGraphicsEnd.Width = 110;
			// 
			// tabPageOrders
			// 
			this.tabPageOrders.Controls.Add(this.groupBoxOrder);
			this.tabPageOrders.Controls.Add(this.comboBoxSelectOrder);
			this.tabPageOrders.Controls.Add(this.labelSelectOrder);
			this.tabPageOrders.Location = new System.Drawing.Point(4, 25);
			this.tabPageOrders.Name = "tabPageOrders";
			this.tabPageOrders.Size = new System.Drawing.Size(568, 299);
			this.tabPageOrders.TabIndex = 1;
			this.tabPageOrders.Text = "Orders";
			this.tabPageOrders.ToolTipText = "Edit orders given to units";
			// 
			// groupBoxOrder
			// 
			this.groupBoxOrder.Controls.Add(this.labelOrderCostHelp);
			this.groupBoxOrder.Controls.Add(this.textBoxOrderCost);
			this.groupBoxOrder.Controls.Add(this.labelOrderCost);
			this.groupBoxOrder.Controls.Add(this.labelOrderNameHelp);
			this.groupBoxOrder.Controls.Add(this.textBoxOrderName);
			this.groupBoxOrder.Controls.Add(this.labelOrderName);
			this.groupBoxOrder.Controls.Add(this.textBoxOrderAddress);
			this.groupBoxOrder.Controls.Add(this.labelOrderAddress);
			this.groupBoxOrder.Location = new System.Drawing.Point(8, 40);
			this.groupBoxOrder.Name = "groupBoxOrder";
			this.groupBoxOrder.Size = new System.Drawing.Size(448, 256);
			this.groupBoxOrder.TabIndex = 2;
			this.groupBoxOrder.TabStop = false;
			// 
			// labelOrderCostHelp
			// 
			this.labelOrderCostHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelOrderCostHelp.Location = new System.Drawing.Point(216, 72);
			this.labelOrderCostHelp.Name = "labelOrderCostHelp";
			this.labelOrderCostHelp.Size = new System.Drawing.Size(200, 21);
			this.labelOrderCostHelp.TabIndex = 20;
			this.labelOrderCostHelp.Text = "[1-65535], will be [10-655350] in game";
			this.labelOrderCostHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxOrderCost
			// 
			this.textBoxOrderCost.Location = new System.Drawing.Point(96, 72);
			this.textBoxOrderCost.MaxLength = 5;
			this.textBoxOrderCost.Name = "textBoxOrderCost";
			this.textBoxOrderCost.Size = new System.Drawing.Size(112, 20);
			this.textBoxOrderCost.TabIndex = 18;
			this.textBoxOrderCost.Text = "";
			this.textBoxOrderCost.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxOrderCost_Validating);
			// 
			// labelOrderCost
			// 
			this.labelOrderCost.Location = new System.Drawing.Point(16, 72);
			this.labelOrderCost.Name = "labelOrderCost";
			this.labelOrderCost.Size = new System.Drawing.Size(72, 21);
			this.labelOrderCost.TabIndex = 19;
			this.labelOrderCost.Text = "Cost:";
			this.labelOrderCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelOrderNameHelp
			// 
			this.labelOrderNameHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelOrderNameHelp.Location = new System.Drawing.Point(216, 48);
			this.labelOrderNameHelp.Name = "labelOrderNameHelp";
			this.labelOrderNameHelp.Size = new System.Drawing.Size(136, 21);
			this.labelOrderNameHelp.TabIndex = 17;
			this.labelOrderNameHelp.Text = "[A-Z] [0-9] [-] only";
			this.labelOrderNameHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxOrderName
			// 
			this.textBoxOrderName.Location = new System.Drawing.Point(96, 48);
			this.textBoxOrderName.MaxLength = 10;
			this.textBoxOrderName.Name = "textBoxOrderName";
			this.textBoxOrderName.Size = new System.Drawing.Size(112, 20);
			this.textBoxOrderName.TabIndex = 3;
			this.textBoxOrderName.Text = "";
			this.textBoxOrderName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxOrderName_Validating);
			// 
			// labelOrderName
			// 
			this.labelOrderName.Location = new System.Drawing.Point(16, 48);
			this.labelOrderName.Name = "labelOrderName";
			this.labelOrderName.Size = new System.Drawing.Size(72, 21);
			this.labelOrderName.TabIndex = 16;
			this.labelOrderName.Text = "Name:";
			this.labelOrderName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxOrderAddress
			// 
			this.textBoxOrderAddress.Location = new System.Drawing.Point(96, 24);
			this.textBoxOrderAddress.Name = "textBoxOrderAddress";
			this.textBoxOrderAddress.ReadOnly = true;
			this.textBoxOrderAddress.Size = new System.Drawing.Size(112, 20);
			this.textBoxOrderAddress.TabIndex = 2;
			this.textBoxOrderAddress.Text = "";
			// 
			// labelOrderAddress
			// 
			this.labelOrderAddress.Location = new System.Drawing.Point(16, 24);
			this.labelOrderAddress.Name = "labelOrderAddress";
			this.labelOrderAddress.Size = new System.Drawing.Size(72, 21);
			this.labelOrderAddress.TabIndex = 1;
			this.labelOrderAddress.Text = "Address: ";
			this.labelOrderAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxSelectOrder
			// 
			this.comboBoxSelectOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSelectOrder.Location = new System.Drawing.Point(96, 16);
			this.comboBoxSelectOrder.Name = "comboBoxSelectOrder";
			this.comboBoxSelectOrder.Size = new System.Drawing.Size(248, 21);
			this.comboBoxSelectOrder.TabIndex = 1;
			this.comboBoxSelectOrder.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectOrder_SelectedIndexChanged);
			// 
			// labelSelectOrder
			// 
			this.labelSelectOrder.Location = new System.Drawing.Point(16, 16);
			this.labelSelectOrder.Name = "labelSelectOrder";
			this.labelSelectOrder.Size = new System.Drawing.Size(72, 21);
			this.labelSelectOrder.TabIndex = 0;
			this.labelSelectOrder.Text = "Select Order: ";
			this.labelSelectOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabPagePalettes
			// 
			this.tabPagePalettes.Controls.Add(this.pictureBoxPalettePreview);
			this.tabPagePalettes.Controls.Add(this.labelPaletteFind);
			this.tabPagePalettes.Controls.Add(this.buttonPalettePrevious);
			this.tabPagePalettes.Controls.Add(this.buttonPaletteFind);
			this.tabPagePalettes.Controls.Add(this.textBoxPaletteFind);
			this.tabPagePalettes.Controls.Add(this.labelPalettePreview);
			this.tabPagePalettes.Controls.Add(this.buttonLaunchPaletteEditor);
			this.tabPagePalettes.Controls.Add(this.listViewPalettes);
			this.tabPagePalettes.Location = new System.Drawing.Point(4, 25);
			this.tabPagePalettes.Name = "tabPagePalettes";
			this.tabPagePalettes.Size = new System.Drawing.Size(568, 299);
			this.tabPagePalettes.TabIndex = 6;
			this.tabPagePalettes.Text = "Palettes";
			// 
			// pictureBoxPalettePreview
			// 
			this.pictureBoxPalettePreview.Location = new System.Drawing.Point(72, 232);
			this.pictureBoxPalettePreview.Name = "pictureBoxPalettePreview";
			this.pictureBoxPalettePreview.Size = new System.Drawing.Size(224, 14);
			this.pictureBoxPalettePreview.TabIndex = 22;
			this.pictureBoxPalettePreview.TabStop = false;
			this.pictureBoxPalettePreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxPalettePreview_Paint);
			// 
			// labelPaletteFind
			// 
			this.labelPaletteFind.Location = new System.Drawing.Point(16, 264);
			this.labelPaletteFind.Name = "labelPaletteFind";
			this.labelPaletteFind.Size = new System.Drawing.Size(32, 20);
			this.labelPaletteFind.TabIndex = 21;
			this.labelPaletteFind.Text = "Find:";
			this.labelPaletteFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonPalettePrevious
			// 
			this.buttonPalettePrevious.Enabled = false;
			this.buttonPalettePrevious.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalettePrevious.Location = new System.Drawing.Point(376, 264);
			this.buttonPalettePrevious.Name = "buttonPalettePrevious";
			this.buttonPalettePrevious.Size = new System.Drawing.Size(68, 20);
			this.buttonPalettePrevious.TabIndex = 20;
			this.buttonPalettePrevious.Text = "Previous";
			this.buttonPalettePrevious.Click += new System.EventHandler(this.buttonPalettePrevious_Click);
			// 
			// buttonPaletteFind
			// 
			this.buttonPaletteFind.Enabled = false;
			this.buttonPaletteFind.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPaletteFind.Location = new System.Drawing.Point(304, 264);
			this.buttonPaletteFind.Name = "buttonPaletteFind";
			this.buttonPaletteFind.Size = new System.Drawing.Size(68, 20);
			this.buttonPaletteFind.TabIndex = 19;
			this.buttonPaletteFind.Text = "Next";
			this.buttonPaletteFind.Click += new System.EventHandler(this.buttonPaletteFind_Click);
			// 
			// textBoxPaletteFind
			// 
			this.textBoxPaletteFind.Enabled = false;
			this.textBoxPaletteFind.Location = new System.Drawing.Point(56, 264);
			this.textBoxPaletteFind.Name = "textBoxPaletteFind";
			this.textBoxPaletteFind.Size = new System.Drawing.Size(240, 20);
			this.textBoxPaletteFind.TabIndex = 18;
			this.textBoxPaletteFind.Text = "";
			this.textBoxPaletteFind.TextChanged += new System.EventHandler(this.textBoxPaletteFind_TextChanged);
			// 
			// labelPalettePreview
			// 
			this.labelPalettePreview.Location = new System.Drawing.Point(16, 232);
			this.labelPalettePreview.Name = "labelPalettePreview";
			this.labelPalettePreview.Size = new System.Drawing.Size(48, 16);
			this.labelPalettePreview.TabIndex = 17;
			this.labelPalettePreview.Text = "Preview:";
			this.labelPalettePreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonLaunchPaletteEditor
			// 
			this.buttonLaunchPaletteEditor.Enabled = false;
			this.buttonLaunchPaletteEditor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonLaunchPaletteEditor.Image = ((System.Drawing.Image)(resources.GetObject("buttonLaunchPaletteEditor.Image")));
			this.buttonLaunchPaletteEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonLaunchPaletteEditor.Location = new System.Drawing.Point(304, 224);
			this.buttonLaunchPaletteEditor.Name = "buttonLaunchPaletteEditor";
			this.buttonLaunchPaletteEditor.Size = new System.Drawing.Size(136, 32);
			this.buttonLaunchPaletteEditor.TabIndex = 16;
			this.buttonLaunchPaletteEditor.Text = "Launch Palette Editor";
			this.buttonLaunchPaletteEditor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonLaunchPaletteEditor.Click += new System.EventHandler(this.buttonLaunchPaletteEditor_Click);
			// 
			// listViewPalettes
			// 
			this.listViewPalettes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeader7,
																							   this.columnHeader10});
			this.listViewPalettes.Location = new System.Drawing.Point(8, 8);
			this.listViewPalettes.MultiSelect = false;
			this.listViewPalettes.Name = "listViewPalettes";
			this.listViewPalettes.Size = new System.Drawing.Size(552, 208);
			this.listViewPalettes.TabIndex = 15;
			this.listViewPalettes.View = System.Windows.Forms.View.Details;
			this.listViewPalettes.DoubleClick += new System.EventHandler(this.listViewPalettes_DoubleClick);
			this.listViewPalettes.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewPalettes_ColumnClick);
			this.listViewPalettes.SelectedIndexChanged += new System.EventHandler(this.listViewPalettes_SelectedIndexChanged);
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Description";
			this.columnHeader7.Width = 459;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Address";
			this.columnHeader10.Width = 69;
			// 
			// tabPageSprites
			// 
			this.tabPageSprites.Controls.Add(this.buttonLaunchTileEditorSprites);
			this.tabPageSprites.Controls.Add(this.listViewSprites);
			this.tabPageSprites.Location = new System.Drawing.Point(4, 25);
			this.tabPageSprites.Name = "tabPageSprites";
			this.tabPageSprites.Size = new System.Drawing.Size(568, 299);
			this.tabPageSprites.TabIndex = 5;
			this.tabPageSprites.Text = "Sprites";
			// 
			// buttonLaunchTileEditorSprites
			// 
			this.buttonLaunchTileEditorSprites.Enabled = false;
			this.buttonLaunchTileEditorSprites.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonLaunchTileEditorSprites.Image = ((System.Drawing.Image)(resources.GetObject("buttonLaunchTileEditorSprites.Image")));
			this.buttonLaunchTileEditorSprites.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonLaunchTileEditorSprites.Location = new System.Drawing.Point(440, 261);
			this.buttonLaunchTileEditorSprites.Name = "buttonLaunchTileEditorSprites";
			this.buttonLaunchTileEditorSprites.Size = new System.Drawing.Size(120, 32);
			this.buttonLaunchTileEditorSprites.TabIndex = 19;
			this.buttonLaunchTileEditorSprites.Text = "Launch Tile Editor";
			this.buttonLaunchTileEditorSprites.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonLaunchTileEditorSprites.Click += new System.EventHandler(this.buttonLaunchTileEditorSprites_Click);
			// 
			// listViewSprites
			// 
			this.listViewSprites.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							  this.columnHeader4,
																							  this.columnHeader5,
																							  this.columnHeader6});
			this.listViewSprites.FullRowSelect = true;
			this.listViewSprites.HideSelection = false;
			this.listViewSprites.Location = new System.Drawing.Point(8, 8);
			this.listViewSprites.MultiSelect = false;
			this.listViewSprites.Name = "listViewSprites";
			this.listViewSprites.Size = new System.Drawing.Size(552, 248);
			this.listViewSprites.TabIndex = 18;
			this.listViewSprites.View = System.Windows.Forms.View.Details;
			this.listViewSprites.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewSprites_ColumnClick);
			this.listViewSprites.SelectedIndexChanged += new System.EventHandler(this.listViewSprites_SelectedIndexChanged);
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Description";
			this.columnHeader4.Width = 300;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Start Address";
			this.columnHeader5.Width = 119;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "End Address";
			this.columnHeader6.Width = 110;
			// 
			// tabPageText
			// 
			this.tabPageText.Controls.Add(this.labelFindText);
			this.tabPageText.Controls.Add(this.buttonFindTextPrevious);
			this.tabPageText.Controls.Add(this.buttonFindTextNext);
			this.tabPageText.Controls.Add(this.textBoxFindText);
			this.tabPageText.Controls.Add(this.listViewDialogText);
			this.tabPageText.Location = new System.Drawing.Point(4, 25);
			this.tabPageText.Name = "tabPageText";
			this.tabPageText.Size = new System.Drawing.Size(568, 299);
			this.tabPageText.TabIndex = 3;
			this.tabPageText.Text = "Text";
			this.tabPageText.ToolTipText = "Edit game text";
			// 
			// labelFindText
			// 
			this.labelFindText.Location = new System.Drawing.Point(24, 272);
			this.labelFindText.Name = "labelFindText";
			this.labelFindText.Size = new System.Drawing.Size(32, 20);
			this.labelFindText.TabIndex = 14;
			this.labelFindText.Text = "Find:";
			this.labelFindText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonFindTextPrevious
			// 
			this.buttonFindTextPrevious.Enabled = false;
			this.buttonFindTextPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonFindTextPrevious.Location = new System.Drawing.Point(488, 272);
			this.buttonFindTextPrevious.Name = "buttonFindTextPrevious";
			this.buttonFindTextPrevious.Size = new System.Drawing.Size(68, 20);
			this.buttonFindTextPrevious.TabIndex = 13;
			this.buttonFindTextPrevious.Text = "Previous";
			this.buttonFindTextPrevious.Click += new System.EventHandler(this.buttonFindTextPrevious_Click);
			// 
			// buttonFindTextNext
			// 
			this.buttonFindTextNext.Enabled = false;
			this.buttonFindTextNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonFindTextNext.Location = new System.Drawing.Point(416, 272);
			this.buttonFindTextNext.Name = "buttonFindTextNext";
			this.buttonFindTextNext.Size = new System.Drawing.Size(68, 20);
			this.buttonFindTextNext.TabIndex = 12;
			this.buttonFindTextNext.Text = "Next";
			this.buttonFindTextNext.Click += new System.EventHandler(this.buttonFindTextNext_Click);
			// 
			// textBoxFindText
			// 
			this.textBoxFindText.Enabled = false;
			this.textBoxFindText.Location = new System.Drawing.Point(64, 272);
			this.textBoxFindText.Name = "textBoxFindText";
			this.textBoxFindText.Size = new System.Drawing.Size(344, 20);
			this.textBoxFindText.TabIndex = 11;
			this.textBoxFindText.Text = "";
			this.textBoxFindText.TextChanged += new System.EventHandler(this.textBoxFindText_TextChanged);
			// 
			// listViewDialogText
			// 
			this.listViewDialogText.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								 this.columnDialogTextHeaderCurrentValue,
																								 this.columnDialogTextCategory,
																								 this.columnDialogTextAddress,
																								 this.columncolumnDialogTextLength});
			this.listViewDialogText.LabelEdit = true;
			this.listViewDialogText.Location = new System.Drawing.Point(8, 8);
			this.listViewDialogText.MultiSelect = false;
			this.listViewDialogText.Name = "listViewDialogText";
			this.listViewDialogText.Size = new System.Drawing.Size(552, 256);
			this.listViewDialogText.TabIndex = 6;
			this.listViewDialogText.View = System.Windows.Forms.View.Details;
			this.listViewDialogText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listViewDialogText_KeyPress);
			this.listViewDialogText.DoubleClick += new System.EventHandler(this.listViewDialogText_DoubleClick);
			this.listViewDialogText.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listViewDialogText_AfterLabelEdit);
			this.listViewDialogText.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewDialogText_ColumnClick);
			// 
			// columnDialogTextHeaderCurrentValue
			// 
			this.columnDialogTextHeaderCurrentValue.Text = "Current Value";
			this.columnDialogTextHeaderCurrentValue.Width = 220;
			// 
			// columnDialogTextCategory
			// 
			this.columnDialogTextCategory.Text = "Category";
			this.columnDialogTextCategory.Width = 188;
			// 
			// columnDialogTextAddress
			// 
			this.columnDialogTextAddress.Text = "Address";
			this.columnDialogTextAddress.Width = 51;
			// 
			// columncolumnDialogTextLength
			// 
			this.columncolumnDialogTextLength.Text = "Max Length";
			this.columncolumnDialogTextLength.Width = 73;
			// 
			// tabPageUnits
			// 
			this.tabPageUnits.Controls.Add(this.groupBoxUnits);
			this.tabPageUnits.Controls.Add(this.comboBoxSelectUnit);
			this.tabPageUnits.Controls.Add(this.labelSelectUnit);
			this.tabPageUnits.Location = new System.Drawing.Point(4, 25);
			this.tabPageUnits.Name = "tabPageUnits";
			this.tabPageUnits.Size = new System.Drawing.Size(568, 299);
			this.tabPageUnits.TabIndex = 2;
			this.tabPageUnits.Text = "Units";
			this.tabPageUnits.ToolTipText = "Edit units (vehicles and infantry)";
			// 
			// groupBoxUnits
			// 
			this.groupBoxUnits.Controls.Add(this.groupBoxUnitOrders);
			this.groupBoxUnits.Controls.Add(this.labelUnitCostHelp);
			this.groupBoxUnits.Controls.Add(this.textBoxUnitCost);
			this.groupBoxUnits.Controls.Add(this.labelUnitCost);
			this.groupBoxUnits.Controls.Add(this.labelUnitNameHelp);
			this.groupBoxUnits.Controls.Add(this.textBoxUnitName);
			this.groupBoxUnits.Controls.Add(this.labelUnitName);
			this.groupBoxUnits.Controls.Add(this.textBoxUnitAddress);
			this.groupBoxUnits.Controls.Add(this.labelUnitAddress);
			this.groupBoxUnits.Location = new System.Drawing.Point(16, 40);
			this.groupBoxUnits.Name = "groupBoxUnits";
			this.groupBoxUnits.Size = new System.Drawing.Size(416, 256);
			this.groupBoxUnits.TabIndex = 4;
			this.groupBoxUnits.TabStop = false;
			// 
			// groupBoxUnitOrders
			// 
			this.groupBoxUnitOrders.Controls.Add(this.checkBoxPWSS10);
			this.groupBoxUnitOrders.Controls.Add(this.checkBoxBA001C);
			this.groupBoxUnitOrders.Controls.Add(this.checkBoxAT101H);
			this.groupBoxUnitOrders.Controls.Add(this.checkBoxDFF02A);
			this.groupBoxUnitOrders.Controls.Add(this.checkBoxAT101);
			this.groupBoxUnitOrders.Controls.Add(this.checkBoxAF001A);
			this.groupBoxUnitOrders.Controls.Add(this.checkBoxBDF1SD);
			this.groupBoxUnitOrders.Location = new System.Drawing.Point(16, 104);
			this.groupBoxUnitOrders.Name = "groupBoxUnitOrders";
			this.groupBoxUnitOrders.Size = new System.Drawing.Size(392, 144);
			this.groupBoxUnitOrders.TabIndex = 21;
			this.groupBoxUnitOrders.TabStop = false;
			this.groupBoxUnitOrders.Text = "Available Orders";
			// 
			// checkBoxPWSS10
			// 
			this.checkBoxPWSS10.Location = new System.Drawing.Point(16, 120);
			this.checkBoxPWSS10.Name = "checkBoxPWSS10";
			this.checkBoxPWSS10.Size = new System.Drawing.Size(360, 16);
			this.checkBoxPWSS10.TabIndex = 6;
			this.checkBoxPWSS10.Text = "PW-SS10 (Supply)";
			this.checkBoxPWSS10.CheckedChanged += new System.EventHandler(this.checkBoxValidation);
			// 
			// checkBoxBA001C
			// 
			this.checkBoxBA001C.Location = new System.Drawing.Point(16, 104);
			this.checkBoxBA001C.Name = "checkBoxBA001C";
			this.checkBoxBA001C.Size = new System.Drawing.Size(360, 16);
			this.checkBoxBA001C.TabIndex = 5;
			this.checkBoxBA001C.Text = "BA-001C (Attack main enemy base)";
			this.checkBoxBA001C.CheckedChanged += new System.EventHandler(this.checkBoxValidation);
			// 
			// checkBoxAT101H
			// 
			this.checkBoxAT101H.Location = new System.Drawing.Point(16, 88);
			this.checkBoxAT101H.Name = "checkBoxAT101H";
			this.checkBoxAT101H.Size = new System.Drawing.Size(360, 16);
			this.checkBoxAT101H.TabIndex = 4;
			this.checkBoxAT101H.Text = "AT-101H (Enter nearest enemy base)";
			this.checkBoxAT101H.CheckedChanged += new System.EventHandler(this.checkBoxValidation);
			// 
			// checkBoxDFF02A
			// 
			this.checkBoxDFF02A.Location = new System.Drawing.Point(16, 72);
			this.checkBoxDFF02A.Name = "checkBoxDFF02A";
			this.checkBoxDFF02A.Size = new System.Drawing.Size(360, 16);
			this.checkBoxDFF02A.TabIndex = 3;
			this.checkBoxDFF02A.Text = "DF-F02A (Defend, attack on sight)";
			this.checkBoxDFF02A.CheckedChanged += new System.EventHandler(this.checkBoxValidation);
			// 
			// checkBoxAT101
			// 
			this.checkBoxAT101.Location = new System.Drawing.Point(16, 56);
			this.checkBoxAT101.Name = "checkBoxAT101";
			this.checkBoxAT101.Size = new System.Drawing.Size(360, 16);
			this.checkBoxAT101.TabIndex = 2;
			this.checkBoxAT101.Text = "AT-101 (Attack nearest enemy base)";
			this.checkBoxAT101.CheckedChanged += new System.EventHandler(this.checkBoxValidation);
			// 
			// checkBoxAF001A
			// 
			this.checkBoxAF001A.Location = new System.Drawing.Point(16, 40);
			this.checkBoxAF001A.Name = "checkBoxAF001A";
			this.checkBoxAF001A.Size = new System.Drawing.Size(360, 16);
			this.checkBoxAF001A.TabIndex = 1;
			this.checkBoxAF001A.Text = "AF-001A (Patrol)";
			this.checkBoxAF001A.CheckedChanged += new System.EventHandler(this.checkBoxValidation);
			// 
			// checkBoxBDF1SD
			// 
			this.checkBoxBDF1SD.Location = new System.Drawing.Point(16, 24);
			this.checkBoxBDF1SD.Name = "checkBoxBDF1SD";
			this.checkBoxBDF1SD.Size = new System.Drawing.Size(360, 16);
			this.checkBoxBDF1SD.TabIndex = 0;
			this.checkBoxBDF1SD.Text = "BDF-1SD (Defend, stay in place)";
			this.checkBoxBDF1SD.CheckedChanged += new System.EventHandler(this.checkBoxValidation);
			// 
			// labelUnitCostHelp
			// 
			this.labelUnitCostHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelUnitCostHelp.Location = new System.Drawing.Point(216, 72);
			this.labelUnitCostHelp.Name = "labelUnitCostHelp";
			this.labelUnitCostHelp.Size = new System.Drawing.Size(192, 21);
			this.labelUnitCostHelp.TabIndex = 20;
			this.labelUnitCostHelp.Text = "[1-65535], will be [10-65535] in game";
			this.labelUnitCostHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxUnitCost
			// 
			this.textBoxUnitCost.Location = new System.Drawing.Point(96, 72);
			this.textBoxUnitCost.MaxLength = 5;
			this.textBoxUnitCost.Name = "textBoxUnitCost";
			this.textBoxUnitCost.Size = new System.Drawing.Size(112, 20);
			this.textBoxUnitCost.TabIndex = 18;
			this.textBoxUnitCost.Text = "";
			this.textBoxUnitCost.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxUnitCost_Validating);
			// 
			// labelUnitCost
			// 
			this.labelUnitCost.Location = new System.Drawing.Point(16, 72);
			this.labelUnitCost.Name = "labelUnitCost";
			this.labelUnitCost.Size = new System.Drawing.Size(72, 21);
			this.labelUnitCost.TabIndex = 19;
			this.labelUnitCost.Text = "Cost:";
			this.labelUnitCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelUnitNameHelp
			// 
			this.labelUnitNameHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelUnitNameHelp.Location = new System.Drawing.Point(216, 48);
			this.labelUnitNameHelp.Name = "labelUnitNameHelp";
			this.labelUnitNameHelp.Size = new System.Drawing.Size(136, 21);
			this.labelUnitNameHelp.TabIndex = 17;
			this.labelUnitNameHelp.Text = "[A-Z] [0-9] [-] only";
			this.labelUnitNameHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxUnitName
			// 
			this.textBoxUnitName.Location = new System.Drawing.Point(96, 48);
			this.textBoxUnitName.MaxLength = 8;
			this.textBoxUnitName.Name = "textBoxUnitName";
			this.textBoxUnitName.Size = new System.Drawing.Size(112, 20);
			this.textBoxUnitName.TabIndex = 3;
			this.textBoxUnitName.Text = "";
			this.textBoxUnitName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxUnitName_Validating);
			// 
			// labelUnitName
			// 
			this.labelUnitName.Location = new System.Drawing.Point(16, 48);
			this.labelUnitName.Name = "labelUnitName";
			this.labelUnitName.Size = new System.Drawing.Size(72, 21);
			this.labelUnitName.TabIndex = 16;
			this.labelUnitName.Text = "Name:";
			this.labelUnitName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxUnitAddress
			// 
			this.textBoxUnitAddress.Location = new System.Drawing.Point(96, 24);
			this.textBoxUnitAddress.Name = "textBoxUnitAddress";
			this.textBoxUnitAddress.ReadOnly = true;
			this.textBoxUnitAddress.Size = new System.Drawing.Size(112, 20);
			this.textBoxUnitAddress.TabIndex = 2;
			this.textBoxUnitAddress.Text = "";
			// 
			// labelUnitAddress
			// 
			this.labelUnitAddress.Location = new System.Drawing.Point(16, 24);
			this.labelUnitAddress.Name = "labelUnitAddress";
			this.labelUnitAddress.Size = new System.Drawing.Size(72, 21);
			this.labelUnitAddress.TabIndex = 1;
			this.labelUnitAddress.Text = "Address: ";
			this.labelUnitAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxSelectUnit
			// 
			this.comboBoxSelectUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSelectUnit.Location = new System.Drawing.Point(96, 16);
			this.comboBoxSelectUnit.Name = "comboBoxSelectUnit";
			this.comboBoxSelectUnit.Size = new System.Drawing.Size(176, 21);
			this.comboBoxSelectUnit.TabIndex = 3;
			this.comboBoxSelectUnit.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectUnit_SelectedIndexChanged);
			// 
			// labelSelectUnit
			// 
			this.labelSelectUnit.Location = new System.Drawing.Point(16, 16);
			this.labelSelectUnit.Name = "labelSelectUnit";
			this.labelSelectUnit.Size = new System.Drawing.Size(72, 21);
			this.labelSelectUnit.TabIndex = 2;
			this.labelSelectUnit.Text = "Select Unit: ";
			this.labelSelectUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// openFileRomDialog
			// 
			this.openFileRomDialog.DefaultExt = "bin";
			this.openFileRomDialog.Filter = "Herzog Zwei ROM Images (*.bin)|*.bin";
			this.openFileRomDialog.Title = "Open Herzog Zwei ROM";
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Description";
			this.columnHeader1.Width = 213;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Start Address";
			this.columnHeader2.Width = 119;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "End Address";
			this.columnHeader3.Width = 110;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(682, 347);
			this.Controls.Add(this.tabControlMainContent);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.listViewNavigate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mainMenu;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "Eisfrei 1.1";
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
			this.tabControlMainContent.ResumeLayout(false);
			this.tabPageMain.ResumeLayout(false);
			this.tabPageGraphics.ResumeLayout(false);
			this.tabPageOrders.ResumeLayout(false);
			this.groupBoxOrder.ResumeLayout(false);
			this.tabPagePalettes.ResumeLayout(false);
			this.tabPageSprites.ResumeLayout(false);
			this.tabPageText.ResumeLayout(false);
			this.tabPageUnits.ResumeLayout(false);
			this.groupBoxUnits.ResumeLayout(false);
			this.groupBoxUnitOrders.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void errorHandler(String action,Exception x)
		{
			ErrorDialog errorDialog=new ErrorDialog(action,x);
			this.Cursor=Cursors.Default;
			errorDialog.ShowDialog(this);
			if(errorDialog.EndApplication)
			{
				this.Close();
			}
		}

		private void generateChecksum()
		{
			this.Cursor=Cursors.WaitCursor;
			this.statusBarPanel.Text="Regenerating checksum..";
			try
			{
				int newChecksum=this.romIO.generateChecksum();
				string newHexChecksum=" (0x"+newChecksum.ToString("X")+")";
				//write new value
				MDInteger mdInt=new MDInteger();
				mdInt.CurrentValue=newChecksum;
				mdInt.NumBytes=2;
				mdInt.Address=398;
				this.romIO.writeInt(mdInt);
				//update UI
				this.textBoxChecksum.Text=this.textBoxCalculatedChecksum.Text=newChecksum.ToString()+newHexChecksum;
				this.buttonChecksum.Enabled=false;
				this.statusBarPanel.Text="New checksum value ["+newChecksum+"] written to rom image";
			}
			catch(Exception x)
			{
				this.errorHandler("generate the checksum",x);
				this.statusBarPanel.Text="Error generating checksum: "+x.Message;
			}
			this.Cursor=Cursors.Default;
		}

		private void openRom()
		{
			this.Cursor=Cursors.WaitCursor;
			try
			{
				System.Windows.Forms.DialogResult result=this.openFileRomDialog.ShowDialog(this);
				if(result.Equals(System.Windows.Forms.DialogResult.OK))
				{
					String fileName=this.openFileRomDialog.FileName;
					this.statusBarPanel.Text="Opening "+fileName+"..";
					//update romIO
					if(this.romIO!=null){ this.romIO.Dispose(); }
					this.romIO=new MDBinaryRomIO(fileName);
					String romHeader=this.romIO.readString(256,138);
					//update UI
					this.textBoxRomPath.Text=fileName;
					this.statusBarPanel.Text="Opened "+fileName;
					this.textBoxRomHeader.Text=romHeader;
					this.tabControlMainContent.Enabled=true;
					this.listViewNavigate.Enabled=true;
					this.listViewDialogText.Items.Clear();
					this.comboBoxSelectOrder.Items.Clear();
					this.comboBoxSelectUnit.Items.Clear();
					this.menuItemRepairChecksum.Enabled=true;
					this.tabControlMainContent_SelectedIndexChanged(null,null);
					this.textBoxFindText.Enabled=true;
					this.textBoxPaletteFind.Enabled=true;
					//checksum stuff
					int storedChecksum=this.romIO.readInteger(398,2,ByteOrder.HighByteFirst);
					this.textBoxChecksum.Text=storedChecksum.ToString()+" (0x"+storedChecksum.ToString("X")+")";
					int calculatedChecksum=this.romIO.generateChecksum();
					this.textBoxCalculatedChecksum.Text=calculatedChecksum.ToString()+" (0x"+calculatedChecksum.ToString("X")+")";
					if(storedChecksum!=calculatedChecksum)
					{
						this.buttonChecksum.Enabled=true;
					}
					else
					{
						this.buttonChecksum.Enabled=false;
					}
				}
			}
			catch(Exception x)
			{
				this.errorHandler("open a rom",x);
				this.statusBarPanel.Text="Error opening rom: "+x.Message;
			}
			this.Cursor=Cursors.Default;
		}

		private void menuItemOpenRom_Click(object sender, System.EventArgs e)
		{
			this.openRom();
		}

		private void buttonOpenRom_Click(object sender, System.EventArgs e)
		{
			this.openRom();
		}

		private void menuItemRepairChecksum_Click(object sender, System.EventArgs e)
		{
			this.generateChecksum();
		}

		private void buttonChecksum_Click(object sender, System.EventArgs e)
		{
			this.generateChecksum();
		}

		private void menuItemHomepage_Click(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			try
			{
				System.Diagnostics.Process.Start(@"http://www.huguesjohnson.com/eisfrei/index.html");		
			}
			catch(Exception x)
			{
				this.errorHandler("going to the Eisfrei homepage",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void menuItemAbout_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.MessageBox.Show(this,"Eisfrei - Herzog Zwei ROM Editor\n\n(c) 2008-2009 Hugues Johnson\nhttp://www.huguesjohnson.com/\n\nChecksum code based off Calculate_Checksum method in Gens\nhttp://sourceforge.net/projects/gens/","About Eisfrei",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
		}

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void listViewNavigate_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			EisfreiUtils.sortListView(e,this.listViewNavigate);
		}

		private void listViewNavigate_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			try
			{
				//find tab that with name that matches selection
				if((listViewNavigate.SelectedItems!=null)&&(listViewNavigate.SelectedItems.Count>0))
				{
					ListViewItem item=listViewNavigate.SelectedItems[0];
					int tabIndex=-1;
					int counter=0;
					int size=this.tabControlMainContent.TabPages.Count;
					while((tabIndex<0)&&(counter<size))
					{
						if(this.tabControlMainContent.TabPages[counter].Text.Equals(item.Text))
						{
							tabIndex=counter;					
						}
						else
						{
							counter++;
						}
					}
					if((tabIndex>-1)&&(tabIndex<size)&&(tabIndex!=this.tabControlMainContent.SelectedIndex))
					{
						this.tabControlMainContent.SelectedIndex=tabIndex;
						this.listViewNavigate.Focus();
					}
				}
			}
			catch(Exception x)
			{
				this.errorHandler("changing the tab",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void tabControlMainContent_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			string newTabName=this.tabControlMainContent.SelectedTab.Text;
			//move to the selected tab - loading data if needed
			if(this.romIO!=null)
			{
				try
				{
					switch(newTabName)
					{
						case "Main":
							int calculatedChecksum=this.romIO.generateChecksum();
							this.textBoxCalculatedChecksum.Text=calculatedChecksum.ToString()+" (0x"+calculatedChecksum.ToString("X")+")";
							if(this.textBoxCalculatedChecksum.Text.Equals(this.textBoxChecksum.Text))
							{
								this.buttonChecksum.Enabled=false;
							}
							else
							{
								this.buttonChecksum.Enabled=true;
							}
							break;
						case "Text":
							if(this.listViewDialogText.Items.Count<1)
							{
								this.listViewDialogText.Items.AddRange(EisfreiUtils.getTextListItems(this.romIO));
								this.listViewDialogText.Items.AddRange(EisfreiUtils.getGeneralTextListItems(this.romIO));
							}
							break;
						case "Orders":
							if(this.comboBoxSelectOrder.Items.Count<1)
							{
								EisfreiUtils.loadLookupValues(this.comboBoxSelectOrder,"Orders");
							}
							break;
						case "Units":
							if(this.comboBoxSelectUnit.Items.Count<1)
							{
								EisfreiUtils.loadLookupValues(this.comboBoxSelectUnit,"Units");
							}
							break;
						case "Graphics":
							if(this.listViewGraphics.Items.Count<1)
							{
								this.listViewGraphics.Items.AddRange(EisfreiUtils.getGraphicsListItems("Graphics",this.romIO));
								this.listViewGraphics.Items[0].Selected=true;
								this.buttonLaunchTileEditor.Enabled=true;
							}
							break;
						case "Sprites":
							if(this.listViewSprites.Items.Count<1)
							{
								this.listViewSprites.Items.AddRange(EisfreiUtils.getGraphicsListItems("Sprites",this.romIO));
								this.listViewSprites.Items[0].Selected=true;
								this.buttonLaunchTileEditorSprites.Enabled=true;
							}
							break;
						case "Palettes":
							if(this.listViewPalettes.Items.Count<1)
							{
								EisfreiUtils.loadLookupValues(this.listViewPalettes,"Palette-Addresses");
								ColumnClickEventArgs args=new ColumnClickEventArgs(0);
								EisfreiUtils.sortListView(args,this.listViewPalettes);
							}
							break;
					}
				} 
				catch(Exception x)
				{
					this.errorHandler("changing to tab "+newTabName,x);
				}
			}
			//set the list view selection to the tab title
			int selectedIndex=-1;
			int count=this.listViewNavigate.Items.Count;
			int index=0;
			while((index<count)&&(selectedIndex==-1))
			{
				if(this.listViewNavigate.Items[index].Text.Equals(newTabName))
				{
					selectedIndex=index;
				}
				else
				{
					index++;
				}
			}
			if((selectedIndex>-1)&&(selectedIndex<count)&&(selectedIndex!=this.listViewNavigate.SelectedIndices[0]))
			{
				this.listViewNavigate.Items[selectedIndex].Selected=true;
			}
			this.Cursor=Cursors.Default;		
		}

		private void listViewDialogText_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			EisfreiUtils.sortListView(e,this.listViewDialogText);
		}

		private void listViewDialogText_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.listViewDialogText.SelectedItems.Count==1)
			{
				this.listViewDialogText.SelectedItems[0].BeginEdit();
			}
		}

		private void listViewDialogText_AfterLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
		{
			if(e.Label!=null)
			{
				this.Cursor=Cursors.WaitCursor;
				try
				{
					ListViewItem selectedItem=this.listViewDialogText.SelectedItems[0];
					MDString mdString=new MDString();
					mdString.CurrentValue=e.Label.Replace(" ","@");
					mdString.Address=int.Parse(selectedItem.SubItems[2].Text);
					mdString.NumBytes=int.Parse(selectedItem.SubItems[3].Text);
					bool valid=((mdString.CurrentValue.Length<=mdString.NumBytes)&&((new Regex(@"^[a-zA-Z0-9\s\.\,\!\?\@\:\;\<\>\=\[\]]+$")).IsMatch(mdString.CurrentValue)));
					if(valid)
					{
						//a few things that look hacky but prevent problems both in the game and when making future edits
						if(mdString.CurrentValue.Length<mdString.NumBytes)
						{
							mdString.CurrentValue=mdString.CurrentValue.PadRight(mdString.NumBytes,' ');
						}
						if(!selectedItem.SubItems[1].Text.Equals("General-Text"))
						{
							mdString.CurrentValue=mdString.CurrentValue+'\0';
						}
						//now write the value
						this.romIO.writeString(mdString);
						this.statusBarPanel.Text="Wrote "+mdString.CurrentValue+" to address "+mdString.Address.ToString();
					}
					else
					{
						MessageBox.Show(this,"The maximum length for this field is "+mdString.NumBytes+".\n\nIt can only contain alphanumeric characters and punctuation.","Validation Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
						e.CancelEdit=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save a dialog value",x);
				}
				this.Cursor=Cursors.Default;
			}		
		}

		private void listViewDialogText_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(this.listViewDialogText.SelectedItems.Count==1)
			{
				this.listViewDialogText.SelectedItems[0].BeginEdit();
			}
		}

		private void comboBoxSelectOrder_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue selectedItem=(LookupValue)this.comboBoxSelectOrder.SelectedItem;
				//address
				int address=selectedItem.IntValue;
				this.textBoxOrderAddress.Text=address.ToString();
				//name
				string name=EisfreiUtils.readName(address+(int)Enums.OrderOffsets.Name,(int)Enums.OrderLengths.Name,this.romIO);
				this.textBoxOrderName.Text=name;
				//cost
				int cost=this.romIO.readInteger(address+(int)Enums.OrderOffsets.Cost,(int)Enums.OrderLengths.Cost,ByteOrder.HighByteFirst);
				this.textBoxOrderCost.Text=cost.ToString();
			}
			catch(Exception x)
			{
				this.errorHandler("select an order",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxOrderName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectOrder.Text.Length<1)||(this.comboBoxSelectOrder.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				string newName=this.textBoxOrderName.Text;
				Regex regex=new Regex(@"^[A-Z0-9\-\s]+$");
				if(regex.IsMatch(newName))
				{
					int address=Convert.ToInt32(this.textBoxOrderAddress.Text)+(int)Enums.OrderOffsets.Name;
					EisfreiUtils.writeName(address,newName,this.romIO);
					this.statusBarPanel.Text="Wrote "+newName+" to address "+address;
				}
				else
				{
					MessageBox.Show(this,"Order names can only contain [A-Z] [0-9] [-] and spaces.","Validation Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
					this.statusBarPanel.Text="Validation failed for order name";
					e.Cancel=true;
				}
			}
			catch(Exception x)
			{
				this.errorHandler("save an order name",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxOrderCost_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.textBoxOrderCost.Text.Length<1)||(this.textBoxOrderAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxOrderCost.Text;
				mdInt.NumBytes=(int)Enums.OrderLengths.Cost;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxOrderAddress.Text))+(int)Enums.OrderOffsets.Cost;
				try
				{
					if(EisfreiUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						MessageBox.Show(this,"Order cost must be between 0-65535.","Validation Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
						this.statusBarPanel.Text="Validation failed for order cost";
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save an order cost",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				MessageBox.Show(this,"Order cost must be between 0-65535.","Validation Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
				this.statusBarPanel.Text="Validation failed for order cost";
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxSelectUnit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue selectedItem=(LookupValue)this.comboBoxSelectUnit.SelectedItem;
				//address
				int address=selectedItem.IntValue;
				this.textBoxUnitAddress.Text=address.ToString();
				//name
				string name=EisfreiUtils.readName(address+(int)Enums.UnitOffsets.Name,(int)Enums.UnitLengths.Name,this.romIO);
				this.textBoxUnitName.Text=name;
				//cost
				int cost=this.romIO.readInteger(address+(int)Enums.UnitOffsets.Cost,(int)Enums.UnitLengths.Cost,ByteOrder.HighByteFirst);
				this.textBoxUnitCost.Text=cost.ToString();
				//orders
				int orders=this.romIO.readInteger(address+(int)Enums.UnitOffsets.Orders,(int)Enums.UnitLengths.Orders,ByteOrder.HighByteFirst);
				this.disableCheckboxValidation=true;
				this.checkBoxAF001A.Checked=((orders&(int)Enums.OrderMatrix.AF001A)!=0);
				this.checkBoxAT101.Checked=((orders&(int)Enums.OrderMatrix.AT101)!=0);
				this.checkBoxAT101H.Checked=((orders&(int)Enums.OrderMatrix.AT101H)!=0);
				this.checkBoxBA001C.Checked=((orders&(int)Enums.OrderMatrix.BA001C)!=0);
				this.checkBoxBDF1SD.Checked=((orders&(int)Enums.OrderMatrix.BDF1SD)!=0);
				this.checkBoxDFF02A.Checked=((orders&(int)Enums.OrderMatrix.DFF02A)!=0);
				this.checkBoxPWSS10.Checked=((orders&(int)Enums.OrderMatrix.PWSS10)!=0);
				this.disableCheckboxValidation=false;
			}
			catch(Exception x)
			{
				this.errorHandler("select an order",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxUnitName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectUnit.Text.Length<1)||(this.comboBoxSelectUnit.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				string newName=this.textBoxUnitName.Text;
				Regex regex=new Regex(@"^[A-Z0-9\-\s]+$");
				if(regex.IsMatch(newName))
				{
					int address=Convert.ToInt32(this.textBoxUnitAddress.Text)+(int)Enums.UnitOffsets.Name;
					EisfreiUtils.writeName(address,newName,this.romIO);
					this.statusBarPanel.Text="Wrote "+newName+" to address "+address;
				}
				else
				{
					MessageBox.Show(this,"Unit names can only contain [A-Z] [0-9] [-] and spaces.","Validation Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
					this.statusBarPanel.Text="Validation failed for unit name";
					e.Cancel=true;
				}
			}
			catch(Exception x)
			{
				this.errorHandler("save an unit name",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxUnitCost_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.textBoxUnitCost.Text.Length<1)||(this.textBoxUnitAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxUnitCost.Text;
				mdInt.NumBytes=(int)Enums.UnitLengths.Cost;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxUnitAddress.Text))+(int)Enums.UnitOffsets.Cost;
				try
				{
					if(EisfreiUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						MessageBox.Show(this,"Unit cost must be between 0-65535.","Validation Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
						this.statusBarPanel.Text="Validation failed for unit cost";
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save an unit cost",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				MessageBox.Show(this,"Unit cost must be between 0-65535.","Validation Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
				this.statusBarPanel.Text="Validation failed for unit cost";
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void checkBoxValidation(object sender, System.EventArgs e)
		{
			if(this.disableCheckboxValidation){ return; }
			if(this.textBoxUnitAddress.Text.Length<1){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				//compute the orders
				int orders=0;
				if(this.checkBoxAF001A.Checked){ orders=orders|(int)Enums.OrderMatrix.AF001A; }
				if(this.checkBoxAT101.Checked){ orders=orders|(int)Enums.OrderMatrix.AT101; }
				if(this.checkBoxAT101H.Checked){ orders=orders|(int)Enums.OrderMatrix.AT101H; }
				if(this.checkBoxBA001C.Checked){ orders=orders|(int)Enums.OrderMatrix.BA001C; }
				if(this.checkBoxBDF1SD.Checked){ orders=orders|(int)Enums.OrderMatrix.BDF1SD; }
				if(this.checkBoxDFF02A.Checked){ orders=orders|(int)Enums.OrderMatrix.DFF02A; }
				if(this.checkBoxPWSS10.Checked){ orders=orders|(int)Enums.OrderMatrix.PWSS10; }
				//save to the rom
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=(int)Enums.UnitLengths.Orders;
				mdInt.CurrentValue=Convert.ToInt32(orders);
				mdInt.Address=(Convert.ToInt32(this.textBoxUnitAddress.Text))+(int)Enums.UnitOffsets.Orders;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}
			catch(Exception x)
			{
				this.errorHandler("save an unit order",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void listViewGraphics_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.listViewGraphics.SelectedIndices.Count!=1)
			{
				this.buttonLaunchTileEditor.Enabled=false;
			}
			else
			{
				this.buttonLaunchTileEditor.Enabled=true;
			}
		}

		private void buttonLaunchTileEditor_Click(object sender, System.EventArgs e)
		{
			if(this.listViewGraphics.SelectedIndices.Count!=1){ return; }
			try
			{
				this.Cursor=Cursors.WaitCursor;
				ListViewItem selectedItem=this.listViewGraphics.SelectedItems[0];
				TileEditorForm tileEditor=new TileEditorForm(this.romIO,Convert.ToInt32(selectedItem.SubItems[1].Text),Convert.ToInt32(selectedItem.SubItems[2].Text),EisfreiUtils.getLookupValueCollection("Palette-Addresses"));
				tileEditor.Text=tileEditor.Text+" - "+selectedItem.SubItems[0].Text;
				this.Cursor=Cursors.Default;
				tileEditor.ShowDialog(this);
			}
			catch(Exception x)
			{
				this.errorHandler("launch the tile editor",x);
			}
		}

		private void listViewGraphics_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			EisfreiUtils.sortListView(e,this.listViewGraphics);
		}

		private void listViewSprites_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			EisfreiUtils.sortListView(e,this.listViewSprites);
		}

		private void listViewSprites_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.listViewSprites.SelectedIndices.Count!=1)
			{
				this.buttonLaunchTileEditorSprites.Enabled=false;
			}
			else
			{
				this.buttonLaunchTileEditorSprites.Enabled=true;
			}
		}

		private void buttonLaunchTileEditorSprites_Click(object sender, System.EventArgs e)
		{
			if(this.listViewSprites.SelectedIndices.Count!=1){ return; }
			try
			{
				this.Cursor=Cursors.WaitCursor;
				ListViewItem selectedItem=this.listViewSprites.SelectedItems[0];
				TileEditorForm tileEditor=new TileEditorForm(this.romIO,Convert.ToInt32(selectedItem.SubItems[1].Text),Convert.ToInt32(selectedItem.SubItems[2].Text),EisfreiUtils.getLookupValueCollection("Palette-Addresses"));
				tileEditor.Text=tileEditor.Text+" - "+selectedItem.SubItems[0].Text;
				this.Cursor=Cursors.Default;
				tileEditor.ShowDialog(this);
			}
			catch(Exception x)
			{
				this.errorHandler("launch the tile editor",x);
			}
		}

		private void textBoxFindText_TextChanged(object sender, System.EventArgs e)
		{
			//search for the text
			string searchText=this.textBoxFindText.Text;
			if(searchText.Length<1)
			{
				this.listViewDialogText.Items[this.textFindIndex].SubItems[0].ResetStyle();
				this.listViewDialogText.Items[this.textFindIndex].Selected=false;
				this.buttonFindTextNext.Enabled=false;
				this.buttonFindTextPrevious.Enabled=false;
				this.textBoxFindText.BackColor=System.Drawing.SystemColors.Window;
				this.textFindIndex=0;
			}
			else
			{
				int newIndex=EisfreiUtils.findNextInListView(searchText,0,0,this.listViewDialogText);
				if((newIndex>=0)&&(newIndex<this.listViewDialogText.Items.Count))
				{
					this.listViewDialogText.Items[this.textFindIndex].SubItems[0].ResetStyle();
					this.listViewDialogText.Items[this.textFindIndex].Selected=false;
					this.textFindIndex=newIndex;				
					this.listViewDialogText.Items[this.textFindIndex].Selected=true;
					this.listViewDialogText.Items[this.textFindIndex].SubItems[0].BackColor=Color.LightBlue;
					this.listViewDialogText.EnsureVisible(this.textFindIndex);
					this.buttonFindTextNext.Enabled=true;
					this.buttonFindTextPrevious.Enabled=true;
					this.statusBarPanel.Text="";
					this.textBoxFindText.BackColor=System.Drawing.SystemColors.Window;
				}
				else
				{
					this.buttonFindTextNext.Enabled=false;
					this.buttonFindTextPrevious.Enabled=false;
					this.listViewDialogText.Items[this.textFindIndex].SubItems[0].ResetStyle();
					this.textFindIndex=0;
					this.statusBarPanel.Text=searchText+" not found.";
					this.textBoxFindText.BackColor=Color.Orange;
				}
			}
		}

		private void buttonFindTextNext_Click(object sender, System.EventArgs e)
		{
			//search for the text
			string searchText=this.textBoxFindText.Text;
			int newIndex=EisfreiUtils.findNextInListView(searchText,0,this.textFindIndex+1,this.listViewDialogText);
			if((newIndex>=0)&&(newIndex<this.listViewDialogText.Items.Count))
			{
				this.listViewDialogText.Items[this.textFindIndex].SubItems[0].ResetStyle();
				this.listViewDialogText.Items[this.textFindIndex].Selected=false;
				this.textFindIndex=newIndex;				
				this.listViewDialogText.Items[this.textFindIndex].Selected=true;
				this.listViewDialogText.Items[this.textFindIndex].SubItems[0].BackColor=Color.LightBlue;
				this.listViewDialogText.EnsureVisible(this.textFindIndex);
				this.statusBarPanel.Text="";
			}
			else
			{
				//search from the beginning
				this.textBoxFindText_TextChanged(null,null);
				this.statusBarPanel.Text="Reached end of list, continued from the top.";
			}
		}

		private void buttonFindTextPrevious_Click(object sender, System.EventArgs e)
		{
			//search for the text
			string searchText=this.textBoxFindText.Text;
			int newIndex=EisfreiUtils.findPreviousInListView(searchText,0,this.textFindIndex-1,this.listViewDialogText);
			if(newIndex<0)
			{
				newIndex=EisfreiUtils.findPreviousInListView(searchText,0,this.listViewDialogText.Items.Count-1,this.listViewDialogText);
				this.statusBarPanel.Text="Reached end of list, continued from the bottom.";
			}
			else
			{
				this.statusBarPanel.Text="";
			}
			if((newIndex>=0)&&(newIndex<this.listViewDialogText.Items.Count))
			{
				this.listViewDialogText.Items[this.textFindIndex].SubItems[0].ResetStyle();
				this.listViewDialogText.Items[this.textFindIndex].Selected=false;
				this.textFindIndex=newIndex;				
				this.listViewDialogText.Items[this.textFindIndex].Selected=true;
				this.listViewDialogText.Items[this.textFindIndex].SubItems[0].BackColor=Color.LightBlue;
				this.listViewDialogText.EnsureVisible(this.textFindIndex);
			}
		}

		private void listViewPalettes_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			EisfreiUtils.sortListView(e,this.listViewPalettes);
			this.Cursor=Cursors.Default;
		}

		private void listViewPalettes_DoubleClick(object sender, System.EventArgs e)
		{
			this.buttonLaunchPaletteEditor_Click(sender,null);
		}

		private void listViewPalettes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(listViewPalettes.SelectedItems.Count==1)
			{
				this.buttonLaunchPaletteEditor.Enabled=true;
				//update the preview
				this.pictureBoxPalettePreview.Refresh();
			}
			else
			{
				this.buttonLaunchPaletteEditor.Enabled=false;
			}
		}

		private void buttonLaunchPaletteEditor_Click(object sender, System.EventArgs e)
		{
			if(listViewPalettes.SelectedItems.Count!=1){ return; }
			try
			{
				this.Cursor=Cursors.WaitCursor;
				PaletteEditorForm paletteEditor=new PaletteEditorForm(this.romIO,int.Parse(listViewPalettes.SelectedItems[0].SubItems[1].Text));
				this.Cursor=Cursors.Default;
				paletteEditor.ShowDialog(this);
				//refresh the preview
				this.pictureBoxPalettePreview.Refresh();
			}
			catch(Exception x)
			{
				this.errorHandler("edit a palette",x);
			}
		}

		private void textBoxPaletteFind_TextChanged(object sender, System.EventArgs e)
		{
			//search for the text
			string searchText=this.textBoxPaletteFind.Text;
			if(searchText.Length<1)
			{
				this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].ResetStyle();
				this.listViewPalettes.Items[this.paletteFindIndex].Selected=false;
				this.buttonPaletteFind.Enabled=false;
				this.buttonPalettePrevious.Enabled=false;
				this.textBoxPaletteFind.BackColor=System.Drawing.SystemColors.Window;
				this.paletteFindIndex=0;
			}
			else
			{
				int newIndex=EisfreiUtils.findNextInListView(searchText,0,0,this.listViewPalettes);
				if((newIndex>=0)&&(newIndex<this.listViewPalettes.Items.Count))
				{
					this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].ResetStyle();
					this.listViewPalettes.Items[this.paletteFindIndex].Selected=false;
					this.paletteFindIndex=newIndex;				
					this.listViewPalettes.Items[this.paletteFindIndex].Selected=true;
					this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].BackColor=Color.LightBlue;
					this.listViewPalettes.EnsureVisible(this.paletteFindIndex);
					this.buttonPaletteFind.Enabled=true;
					this.buttonPalettePrevious.Enabled=true;
					this.statusBarPanel.Text="";
					this.textBoxPaletteFind.BackColor=System.Drawing.SystemColors.Window;
				}
				else
				{
					this.buttonPaletteFind.Enabled=false;
					this.buttonPalettePrevious.Enabled=false;
					this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].ResetStyle();
					this.paletteFindIndex=0;
					this.statusBarPanel.Text=searchText+" not found.";
					this.textBoxPaletteFind.BackColor=Color.Orange;
				}
			}
		}

		private void buttonPaletteFind_Click(object sender, System.EventArgs e)
		{
			//search for the text
			string searchText=this.textBoxPaletteFind.Text;
			int newIndex=EisfreiUtils.findNextInListView(searchText,0,this.paletteFindIndex+1,this.listViewPalettes);
			if((newIndex>=0)&&(newIndex<this.listViewPalettes.Items.Count))
			{
				this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].ResetStyle();
				this.listViewPalettes.Items[this.paletteFindIndex].Selected=false;
				this.paletteFindIndex=newIndex;				
				this.listViewPalettes.Items[this.paletteFindIndex].Selected=true;
				this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].BackColor=Color.LightBlue;
				this.listViewPalettes.EnsureVisible(this.paletteFindIndex);
				this.statusBarPanel.Text="";
			}
			else
			{
				//search from the beginning
				this.textBoxPaletteFind_TextChanged(null,null);
				this.statusBarPanel.Text="Reached end of list, continued from the top.";
			}
		}

		private void buttonPalettePrevious_Click(object sender, System.EventArgs e)
		{
			//search for the text
			string searchText=this.textBoxPaletteFind.Text;
			int newIndex=EisfreiUtils.findPreviousInListView(searchText,0,this.paletteFindIndex-1,this.listViewPalettes);
			if(newIndex<0)
			{
				newIndex=EisfreiUtils.findPreviousInListView(searchText,0,this.listViewPalettes.Items.Count-1,this.listViewPalettes);
				this.statusBarPanel.Text="Reached end of list, continued from the bottom.";
			}
			else
			{
				this.statusBarPanel.Text="";
			}
			if((newIndex>=0)&&(newIndex<this.listViewPalettes.Items.Count))
			{
				this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].ResetStyle();
				this.listViewPalettes.Items[this.paletteFindIndex].Selected=false;
				this.paletteFindIndex=newIndex;				
				this.listViewPalettes.Items[this.paletteFindIndex].Selected=true;
				this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].BackColor=Color.LightBlue;
				this.listViewPalettes.EnsureVisible(this.paletteFindIndex);
			}
		}

		private void pictureBoxPalettePreview_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(this.listViewPalettes.SelectedItems.Count!=1){ return; }
			Palette previewPalette=this.romIO.readPalette(int.Parse(this.listViewPalettes.SelectedItems[0].SubItems[1].Text));
			int width=pictureBoxPalettePreview.Width/16;
			int height=pictureBoxPalettePreview.Height;
			for(int x=0;x<16;x++)
			{
				int xc=(x*width);
				SolidBrush brush=new SolidBrush(previewPalette.Entries[x].ToRGB()); 
				e.Graphics.FillRectangle(brush,xc,0,width,height);
			}
		}
	}
}
