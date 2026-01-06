using System;
using System.Drawing;
using System.Windows.Forms;
namespace UCommander
   
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            lvLeft = new ListView();
            chNameLeft = new ColumnHeader();
            chTypeLeft = new ColumnHeader();
            chSizeLeft = new ColumnHeader();
            chCreationLeft = new ColumnHeader();
            panelLeftPath = new Panel();
            txtLeftPath = new TextBox();
            btnLeftDrop = new Button();
            lvRight = new ListView();
            chNameRight = new ColumnHeader();
            chTypeRight = new ColumnHeader();
            chSizeRight = new ColumnHeader();
            chCreationRight = new ColumnHeader();
            panelRightPath = new Panel();
            txtRightPath = new TextBox();
            btnRightDrop = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panelLeftPath.SuspendLayout();
            panelRightPath.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lvLeft);
            splitContainer1.Panel1.Controls.Add(panelLeftPath);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(lvRight);
            splitContainer1.Panel2.Controls.Add(panelRightPath);
            splitContainer1.Size = new Size(1531, 636);
            splitContainer1.SplitterDistance = 741;
            splitContainer1.TabIndex = 0;
            // 
            // lvLeft
            // 
            lvLeft.AllowDrop = true;
            lvLeft.Columns.AddRange(new ColumnHeader[] { chNameLeft, chTypeLeft, chSizeLeft, chCreationLeft });
            lvLeft.Dock = DockStyle.Fill;
            lvLeft.FullRowSelect = true;
            lvLeft.Location = new Point(0, 24);
            lvLeft.Name = "lvLeft";
            lvLeft.Size = new Size(741, 612);
            lvLeft.TabIndex = 0;
            lvLeft.UseCompatibleStateImageBehavior = false;
            lvLeft.View = View.Details;
            lvLeft.ColumnClick += lvLeft_ColumnClick;
            lvLeft.ItemActivate += lvLeft_ItemActivate;
            lvLeft.ItemDrag += lvLeft_ItemDrag;
            lvLeft.DragDrop += lvLeft_DragDrop;
            lvLeft.DragEnter += lvLeft_DragEnter;
            // 
            // chNameLeft
            // 
            chNameLeft.Text = "Name";
            chNameLeft.Width = 420;
            // 
            // chTypeLeft
            // 
            chTypeLeft.Text = "Type";
            chTypeLeft.Width = 100;
            // 
            // chSizeLeft
            // 
            chSizeLeft.Text = "Size";
            chSizeLeft.Width = 100;
            // 
            // chCreationLeft
            // 
            chCreationLeft.Text = "Creation Date";
            chCreationLeft.Width = 200;
            // 
            // panelLeftPath
            // 
            panelLeftPath.Controls.Add(txtLeftPath);
            panelLeftPath.Controls.Add(btnLeftDrop);
            panelLeftPath.Dock = DockStyle.Top;
            panelLeftPath.Location = new Point(0, 0);
            panelLeftPath.Name = "panelLeftPath";
            panelLeftPath.Size = new Size(741, 24);
            panelLeftPath.TabIndex = 1;
            // 
            // txtLeftPath
            // 
            txtLeftPath.BorderStyle = BorderStyle.FixedSingle;
            txtLeftPath.Dock = DockStyle.Fill;
            txtLeftPath.Location = new Point(0, 0);
            txtLeftPath.Name = "txtLeftPath";
            txtLeftPath.ReadOnly = true;
            txtLeftPath.Size = new Size(717, 23);
            txtLeftPath.TabIndex = 0;
            // 
            // btnLeftDrop
            // 
            btnLeftDrop.Dock = DockStyle.Right;
            btnLeftDrop.Location = new Point(717, 0);
            btnLeftDrop.Name = "btnLeftDrop";
            btnLeftDrop.Size = new Size(24, 24);
            btnLeftDrop.TabIndex = 1;
            btnLeftDrop.Text = "▾";
            // 
            // lvRight
            // 
            lvRight.AllowDrop = true;
            lvRight.Columns.AddRange(new ColumnHeader[] { chNameRight, chTypeRight, chSizeRight, chCreationRight });
            lvRight.Dock = DockStyle.Fill;
            lvRight.FullRowSelect = true;
            lvRight.Location = new Point(0, 24);
            lvRight.Name = "lvRight";
            lvRight.Size = new Size(786, 612);
            lvRight.TabIndex = 0;
            lvRight.UseCompatibleStateImageBehavior = false;
            lvRight.View = View.Details;
            lvRight.ColumnClick += lvRight_ColumnClick;
            lvRight.ItemActivate += lvRight_ItemActivate;
            lvRight.ItemDrag += lvRight_ItemDrag;
            lvRight.DragDrop += lvRight_DragDrop;
            lvRight.DragEnter += lvRight_DragEnter;
            // 
            // chNameRight
            // 
            chNameRight.Text = "Name";
            chNameRight.Width = 420;
            // 
            // chTypeRight
            // 
            chTypeRight.Text = "Type";
            chTypeRight.Width = 100;
            // 
            // chSizeRight
            // 
            chSizeRight.Text = "Size";
            chSizeRight.Width = 100;
            // 
            // chCreationRight
            // 
            chCreationRight.Text = "Creation Date";
            chCreationRight.Width = 200;
            // 
            // panelRightPath
            // 
            panelRightPath.Controls.Add(txtRightPath);
            panelRightPath.Controls.Add(btnRightDrop);
            panelRightPath.Dock = DockStyle.Top;
            panelRightPath.Location = new Point(0, 0);
            panelRightPath.Name = "panelRightPath";
            panelRightPath.Size = new Size(786, 24);
            panelRightPath.TabIndex = 1;
            // 
            // txtRightPath
            // 
            txtRightPath.BorderStyle = BorderStyle.FixedSingle;
            txtRightPath.Dock = DockStyle.Fill;
            txtRightPath.Location = new Point(0, 0);
            txtRightPath.Name = "txtRightPath";
            txtRightPath.ReadOnly = true;
            txtRightPath.Size = new Size(762, 23);
            txtRightPath.TabIndex = 0;
            // 
            // btnRightDrop
            // 
            btnRightDrop.Dock = DockStyle.Right;
            btnRightDrop.Location = new Point(762, 0);
            btnRightDrop.Name = "btnRightDrop";
            btnRightDrop.Size = new Size(24, 24);
            btnRightDrop.TabIndex = 1;
            btnRightDrop.Text = "▾";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1531, 636);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Form1";
            KeyDown += Form1_KeyDown;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panelLeftPath.ResumeLayout(false);
            panelLeftPath.PerformLayout();
            panelRightPath.ResumeLayout(false);
            panelRightPath.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private ListView lvLeft;
        private ColumnHeader chNameLeft;
        private ColumnHeader chCreationLeft;
        private ColumnHeader chCreationRight;
        private ColumnHeader chTypeLeft;
        private ColumnHeader chSizeLeft;
        private ColumnHeader chTypeRight;
        private ColumnHeader chSizeRight;
        private ListView lvRight;
        private ColumnHeader chNameRight;
        private TextBox txtLeftPath;
        private TextBox txtRightPath;
        private Panel panelLeftPath;
        private Button btnLeftDrop;
        private Panel panelRightPath;
        private Button btnRightDrop;

    }
}
