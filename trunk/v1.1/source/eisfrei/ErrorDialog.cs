/*
Eisfrei: Herzog Zwei ROM Editor
 Original code from Aridia: Phantasy Star III ROM Editor 
 Modifications of original code noted in comments
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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.huguesjohnson.eisfrei
{
	/// <summary>
	/// Summary description for ErrorDialog.
	/// </summary>
	public class ErrorDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonTerminate;
		private System.Windows.Forms.Button buttonReturn;
		private System.Windows.Forms.TextBox textBoxStackTrace;
		private System.Windows.Forms.Label labelSorry;
		private System.Windows.Forms.Label labelStackTrace;
		private System.Windows.Forms.Label labelAction;
		private bool endApplication;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ErrorDialog(String action,Exception x)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.endApplication=false;
			this.labelAction.Text=action;
			this.textBoxStackTrace.Text=x.Message+"\n"+x.StackTrace;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ErrorDialog));
			this.buttonTerminate = new System.Windows.Forms.Button();
			this.buttonReturn = new System.Windows.Forms.Button();
			this.textBoxStackTrace = new System.Windows.Forms.TextBox();
			this.labelSorry = new System.Windows.Forms.Label();
			this.labelStackTrace = new System.Windows.Forms.Label();
			this.labelAction = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonTerminate
			// 
			this.buttonTerminate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonTerminate.Location = new System.Drawing.Point(368, 176);
			this.buttonTerminate.Name = "buttonTerminate";
			this.buttonTerminate.Size = new System.Drawing.Size(120, 32);
			this.buttonTerminate.TabIndex = 13;
			this.buttonTerminate.Text = "End the application";
			this.buttonTerminate.Click += new System.EventHandler(this.buttonTerminate_Click);
			// 
			// buttonReturn
			// 
			this.buttonReturn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonReturn.Location = new System.Drawing.Point(232, 176);
			this.buttonReturn.Name = "buttonReturn";
			this.buttonReturn.Size = new System.Drawing.Size(120, 32);
			this.buttonReturn.TabIndex = 12;
			this.buttonReturn.Text = "Return to Eisfrei";
			this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
			// 
			// textBoxStackTrace
			// 
			this.textBoxStackTrace.Location = new System.Drawing.Point(8, 64);
			this.textBoxStackTrace.Multiline = true;
			this.textBoxStackTrace.Name = "textBoxStackTrace";
			this.textBoxStackTrace.ReadOnly = true;
			this.textBoxStackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxStackTrace.Size = new System.Drawing.Size(480, 104);
			this.textBoxStackTrace.TabIndex = 11;
			this.textBoxStackTrace.Text = "";
			// 
			// labelSorry
			// 
			this.labelSorry.Location = new System.Drawing.Point(8, 8);
			this.labelSorry.Name = "labelSorry";
			this.labelSorry.Size = new System.Drawing.Size(200, 24);
			this.labelSorry.TabIndex = 10;
			this.labelSorry.Text = "Sorry, an error occurred while trying to:";
			// 
			// labelStackTrace
			// 
			this.labelStackTrace.Location = new System.Drawing.Point(8, 40);
			this.labelStackTrace.Name = "labelStackTrace";
			this.labelStackTrace.Size = new System.Drawing.Size(104, 24);
			this.labelStackTrace.TabIndex = 9;
			this.labelStackTrace.Text = "Error Stack Trace:";
			// 
			// labelAction
			// 
			this.labelAction.Location = new System.Drawing.Point(200, 8);
			this.labelAction.Name = "labelAction";
			this.labelAction.Size = new System.Drawing.Size(280, 24);
			this.labelAction.TabIndex = 14;
			this.labelAction.Text = "<action>";
			// 
			// ErrorDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(498, 215);
			this.Controls.Add(this.labelAction);
			this.Controls.Add(this.buttonTerminate);
			this.Controls.Add(this.buttonReturn);
			this.Controls.Add(this.textBoxStackTrace);
			this.Controls.Add(this.labelSorry);
			this.Controls.Add(this.labelStackTrace);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ErrorDialog";
			this.Text = "Error";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonReturn_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonTerminate_Click(object sender, System.EventArgs e)
		{
			this.endApplication=true;
			this.Close();
		}

		public bool EndApplication
		{
			get
			{
				return(this.endApplication);
			}
		}
	}
}
