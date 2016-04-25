﻿using System;

namespace GKUI.Dialogs
{
	partial class SourceCitEditDlg
	{
		private System.Windows.Forms.Button btnAccept;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label Label1;
		private System.Windows.Forms.TextBox EditPage;
		private System.Windows.Forms.Label Label2;
		private System.Windows.Forms.Button btnSourceAdd;
		private System.Windows.Forms.Label Label3;
		private System.Windows.Forms.ComboBox EditCertainty;
		private System.Windows.Forms.ComboBox cbSource;

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceCitEditDlg));
			this.btnAccept = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.Label1 = new System.Windows.Forms.Label();
			this.EditPage = new System.Windows.Forms.TextBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.btnSourceAdd = new System.Windows.Forms.Button();
			this.Label3 = new System.Windows.Forms.Label();
			this.EditCertainty = new System.Windows.Forms.ComboBox();
			this.cbSource = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// btnAccept
			// 
			this.btnAccept.Image = ((System.Drawing.Image)(resources.GetObject("btnAccept.Image")));
			this.btnAccept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnAccept.Location = new System.Drawing.Point(216, 204);
			this.btnAccept.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(101, 31);
			this.btnAccept.TabIndex = 7;
			this.btnAccept.Text = "Принять";
			this.btnAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCancel.Location = new System.Drawing.Point(325, 204);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(101, 31);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Отменить";
			this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Label1
			// 
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(15, 70);
			this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(108, 17);
			this.Label1.TabIndex = 3;
			this.Label1.Text = "Лист/Страница";
			// 
			// EditPage
			// 
			this.EditPage.Location = new System.Drawing.Point(15, 90);
			this.EditPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.EditPage.Name = "EditPage";
			this.EditPage.Size = new System.Drawing.Size(410, 24);
			this.EditPage.TabIndex = 4;
			// 
			// Label2
			// 
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(15, 10);
			this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(71, 17);
			this.Label2.TabIndex = 0;
			this.Label2.Text = "Источник";
			// 
			// btnSourceAdd
			// 
			this.btnSourceAdd.AccessibleDescription = "Выбрать персональную запись";
			this.btnSourceAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnSourceAdd.Image")));
			this.btnSourceAdd.Location = new System.Drawing.Point(391, 24);
			this.btnSourceAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnSourceAdd.Name = "btnSourceAdd";
			this.btnSourceAdd.Size = new System.Drawing.Size(35, 35);
			this.btnSourceAdd.TabIndex = 2;
			this.btnSourceAdd.TabStop = false;
			this.btnSourceAdd.Click += new System.EventHandler(this.btnSourceAdd_Click);
			// 
			// Label3
			// 
			this.Label3.AutoSize = true;
			this.Label3.Location = new System.Drawing.Point(15, 130);
			this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(117, 17);
			this.Label3.TabIndex = 5;
			this.Label3.Text = "Оценка качества";
			// 
			// EditCertainty
			// 
			this.EditCertainty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.EditCertainty.Location = new System.Drawing.Point(15, 150);
			this.EditCertainty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.EditCertainty.Name = "EditCertainty";
			this.EditCertainty.Size = new System.Drawing.Size(410, 25);
			this.EditCertainty.TabIndex = 6;
			// 
			// cbSource
			// 
			this.cbSource.Location = new System.Drawing.Point(15, 30);
			this.cbSource.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cbSource.Name = "cbSource";
			this.cbSource.Size = new System.Drawing.Size(368, 25);
			this.cbSource.Sorted = true;
			this.cbSource.TabIndex = 1;
			this.cbSource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbSource_KeyDown);
			this.cbSource.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbSource_KeyUp);
			// 
			// SourceCitEditDlg
			// 
			this.AcceptButton = this.btnAccept;
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(441, 250);
			this.Controls.Add(this.btnAccept);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.EditPage);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.btnSourceAdd);
			this.Controls.Add(this.Label3);
			this.Controls.Add(this.EditCertainty);
			this.Controls.Add(this.cbSource);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SourceCitEditDlg";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "SourceCitEdit";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
	}
}