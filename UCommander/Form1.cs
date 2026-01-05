using System.Drawing.Text;

namespace UCommander
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += Form1_KeyDown1;
        }

        private void Form1_KeyDown1(object? sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ListView_ItemActivate(object? sender, EventArgs e)
        {
            MessageBox.Show("item activated, TODO: Implement navigation later");
        }
        private void ListView_ColumnClick(object? sender, ColumnClickEventArgs e)
        {
            MessageBox.Show($"column clicked, TODO: Implement sorting later");
        }
        private void ListView_ItemDrag(object? sender, ItemDragEventArgs e)
        {
            MessageBox.Show($"item drag started, TODO: Implement drag and drop later");
        }
        private void ListView_DragEnter(object? sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            MessageBox.Show($"item drag enter, TODO: Implement drag and drop later");
        }
        private void ListView_DragDrop(object? sender, DragEventArgs e)
        {
            MessageBox.Show($"item dropped, TODO: Implement drag and drop later");
        }
        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                MessageBox.Show("F5 pressed, TODO: Implement file copy later");
            }
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
