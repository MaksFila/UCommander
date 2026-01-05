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
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Size = new Size(1531, 636);
            splitContainer1.SplitterDistance = 741;
            splitContainer1.TabIndex = 0;

            //
            // Instance Controls
            //
            lvLeft = new ListView();
            chNameLeft = new ColumnHeader();
            chTypeLeft = new ColumnHeader();
            chSizeLeft = new ColumnHeader();
            chCreationLeft = new ColumnHeader();
            txtLeftPath = new TextBox();

            lvRight = new ListView();
            chNameRight = new ColumnHeader();
            chTypeRight = new ColumnHeader();
            chSizeRight = new ColumnHeader();
            chCreationRight = new ColumnHeader();
            txtRightPath = new TextBox();
            //
            // lvLeft
            //
            lvLeft.Columns.AddRange(new ColumnHeader[]
                {
                    chNameLeft,
                    chSizeLeft,
                    chTypeLeft,
                    chCreationLeft,
                });
            lvLeft.Dock = DockStyle.Fill;
            lvLeft.FullRowSelect = true;
            lvLeft.HideSelection = false;
            lvLeft.View = View.Details;
            lvLeft.AllowDrop = true;
            lvLeft.UseCompatibleStateImageBehavior = false;
            lvLeft.Name = "lvLeft";

            chNameLeft.Text = "Name";
            chNameLeft.Width = 420;
            chTypeLeft.Text = "Type";
            chTypeLeft.Width = 100;
            chSizeLeft.Text = "Size";
            chSizeLeft.Width = 100;
            chCreationLeft.Text = "Creation Date";
            chCreationLeft.Width = 200;

            //Left Path TextBox
            txtLeftPath.Dock = DockStyle.Top;
            txtLeftPath.ReadOnly = true;
            txtLeftPath.BorderStyle = BorderStyle.FixedSingle;
            txtLeftPath.Height = 24;
            txtLeftPath.Name = "txtLeftPath";

            splitContainer1.Panel1.Controls.Add(lvLeft);
            splitContainer1.Panel1.Controls.Add(txtLeftPath);

            lvLeft.ItemActivate += new EventHandler(lvLeft_ItemActivate);
            lvLeft.ColumnClick += new ColumnClickEventHandler(lvLeft_ColumnClick);
            lvLeft.ItemDrag += new ItemDragEventHandler(lvLeft_ItemDrag);
            lvLeft.DragEnter += new DragEventHandler(lvLeft_DragEnter);
            lvLeft.DragDrop += new DragEventHandler(lvLeft_DragDrop);
            //
            // lvRight
            //
            lvRight.Columns.AddRange(new ColumnHeader[]
                {
                    chNameRight,
                    chTypeRight,
                    chSizeRight,
                    chCreationRight,
                 });

            lvRight.Dock = DockStyle.Fill;
            lvRight.FullRowSelect = true;
            lvRight.HideSelection = false;
            lvRight.View = View.Details;
            lvRight.AllowDrop = true;
            lvRight.UseCompatibleStateImageBehavior = false;
            lvRight.Name = "lvRight";

            chNameRight.Text = "Name";
            chNameRight.Width = 420;
            chTypeRight.Text = "Type";
            chTypeRight.Width = 100;
            chSizeRight.Text = "Size";
            chSizeRight.Width = 100;
            chCreationRight.Text = "Creation Date";
            chCreationRight.Width = 200;

            //Right Path TextBox
            txtRightPath.Dock = DockStyle.Top;
            txtRightPath.ReadOnly = true;
            txtRightPath.BorderStyle = BorderStyle.FixedSingle;
            txtRightPath.Height = 24;
            txtRightPath.Name = "txtRightPath";

            splitContainer1.Panel2.Controls.Add(lvRight);
            splitContainer1.Panel2.Controls.Add(txtRightPath);

            lvRight.ItemActivate += new EventHandler(lvRight_ItemActivate);
            lvRight.ColumnClick += new ColumnClickEventHandler(lvRight_ColumnClick);
            lvRight.ItemDrag += new ItemDragEventHandler(lvRight_ItemDrag);
            lvRight.DragEnter += new DragEventHandler(lvRight_DragEnter);
            lvRight.DragDrop += new DragEventHandler(lvRight_DragDrop);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1531, 636);
            Controls.Add(splitContainer1);
            KeyDown += Form1_KeyDown;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
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

    }
}
