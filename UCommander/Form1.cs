using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace UCommander
{
    public partial class Form1 : Form
    {
        private FilePanelController _leftController;
        private FilePanelController _rightController;
        private FilePanelController? _activateController;
        public Form1()
        {
            InitializeComponent();

            KeyPreview = true;
            KeyDown += Form1_KeyDown1;

            _leftController = new FilePanelController(lvLeft, txtLeftPath);
            _rightController = new FilePanelController(lvRight, txtRightPath);

            // Bind dropdown buttons for drive selection
            _leftController.BindPathDropdown(btnLeftDrop);
            _rightController.BindPathDropdown(btnRightDrop);

            _leftController.Activated += (s, e) => _activateController = _leftController;
            _rightController.Activated += (s, e) => _activateController = _rightController;

            // Ensure UI shows current paths after controllers initialized
            if (!string.IsNullOrWhiteSpace(_leftController.CurrentPath))
                txtLeftPath.Text = _leftController.CurrentPath;
            if (!string.IsNullOrWhiteSpace(_rightController.CurrentPath))
                txtRightPath.Text = _rightController.CurrentPath;

            lvLeft.Enabled = true;
            lvRight.Enabled = true;
            txtLeftPath.Enabled = true;
            txtRightPath.Enabled = true;

            _activateController = null;
        }

        private FilePanelController? GetControllerForListView(ListView lv)
        {
            if (lv == _leftController.ListView) return _leftController;
            if (lv == _rightController.ListView) return _rightController;
            return null;
        }

        private void Form1_KeyDown1(object? sender, KeyEventArgs e)
        {
            Form1_KeyDown(sender, e);
        }

        private void ListView_ItemActivate(object? sender, EventArgs e)
        {
            if (sender is not ListView lv) return;

            FilePanelController? controller = GetControllerForListView(lv);
            if (controller == null) return;

            controller.OpenSelectedItem();
        }

        private void ListView_ColumnClick(object? sender, ColumnClickEventArgs e)
        {
            if (sender is not ListView listView) return;

            int columnIndex = e.Column;
            FilePanelController? controller = GetControllerForListView(listView);
            if (controller == null) return;

            controller.SortByColumn(columnIndex);
        }

        private void ListView_ItemDrag(object? sender, ItemDragEventArgs e)
        {
            if (sender is not ListView lv) return;

            FilePanelController? controller = GetControllerForListView(lv);
            if (controller == null) return;

            controller.StartDrag();
        }

        private void ListView_DragEnter(object? sender, DragEventArgs e)
        {
            if (sender is not ListView lv) return;

            FilePanelController? controller = GetControllerForListView(lv);
            if (controller == null) return;

            controller.HandleDragEnter(e);
        }

        private void ListView_DragDrop(object? sender, DragEventArgs e)
        {
            if (sender is not ListView lv) return;

            FilePanelController? controller = GetControllerForListView(lv);
            if (controller == null) return;

            controller.HandleDragDrop(e);
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e == null) return;

            if (e.KeyCode == Keys.F5)
            {
                if (_activateController == null)
                {
                    MessageBox.Show("No panel is active. Focus a panel and try again.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                FilePanelController source = _activateController;
                FilePanelController target = source == _leftController ? _rightController : _leftController;

                if (string.IsNullOrWhiteSpace(target.CurrentPath))
                {
                    MessageBox.Show("Target panel has no open directory. Open a folder in the target panel first.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                source.CopySelectedTo(target.CurrentPath);
                target.LoadDirectory(target.CurrentPath);
            }
            else if (e.KeyCode == Keys.F7)
            {
                if (_activateController == null)
                {
                    MessageBox.Show("No panel is active. Focus a panel and try again.", "Create Folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string? name = PromptForFolderName("Create Folder", "Enter folder name:");
                if (string.IsNullOrWhiteSpace(name)) return;

                _activateController.CreateFolder(name);
            }
            else if (e.KeyCode == Keys.F8)
            {
                if (_activateController == null)
                {
                    MessageBox.Show("No panel is active. Focus a panel and try again.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _activateController.DeleteSelected();
            }
        }

        private string? PromptForFolderName(string title, string prompt)
        {
            Form dialog = new Form();
            dialog.Text = title;
            dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.ClientSize = new System.Drawing.Size(420, 110);
            dialog.MinimizeBox = false;
            dialog.MaximizeBox = false;
            dialog.ShowInTaskbar = false;

            Label label = new Label();
            label.Text = prompt;
            label.Left = 10;
            label.Top = 10;
            label.AutoSize = true;

            TextBox textBox = new TextBox();
            textBox.Left = 10;
            textBox.Top = 30;
            textBox.Width = 400;

            Button ok = new Button();
            ok.Text = "OK";
            ok.DialogResult = DialogResult.OK;
            ok.Left = 240;
            ok.Top = 62;

            Button cancel = new Button();
            cancel.Text = "Cancel";
            cancel.DialogResult = DialogResult.Cancel;
            cancel.Left = 325;
            cancel.Top = 62;

            dialog.Controls.Add(label);
            dialog.Controls.Add(textBox);
            dialog.Controls.Add(ok);
            dialog.Controls.Add(cancel);
            dialog.AcceptButton = ok;
            dialog.CancelButton = cancel;

            DialogResult dr = dialog.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                return textBox.Text;
            }

            return null;
        }

        public void SetLeftPath(string path)
        {
            txtLeftPath.Text = path;
        }
        public void SetRightPath(string path)
        {
            txtRightPath.Text = path;
        }
        private void lvLeft_ItemActivate(object? sender, EventArgs e) => ListView_ItemActivate(sender, e);
        private void lvRight_ItemActivate(object? sender, EventArgs e) => ListView_ItemActivate(sender, e);
        private void lvLeft_ColumnClick(object? sender, ColumnClickEventArgs e) => ListView_ColumnClick(sender, e);
        private void lvRight_ColumnClick(object? sender, ColumnClickEventArgs e) => ListView_ColumnClick(sender, e);
        private void lvLeft_ItemDrag(object? sender, ItemDragEventArgs e) => ListView_ItemDrag(sender, e);
        private void lvRight_ItemDrag(object? sender, ItemDragEventArgs e) => ListView_ItemDrag(sender, e);
        private void lvLeft_DragEnter(object? sender, DragEventArgs e) => ListView_DragEnter(sender, e);
        private void lvRight_DragEnter(object? sender, DragEventArgs e) => ListView_DragEnter(sender, e);
        private void lvLeft_DragDrop(object? sender, DragEventArgs e) => ListView_DragDrop(sender, e);
        private void lvRight_DragDrop(object? sender, DragEventArgs e) => ListView_DragDrop(sender, e);
    }
}
