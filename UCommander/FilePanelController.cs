using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Drawing;

namespace UCommander
{
    internal class FilePanelController
    {
        public ListView ListView { get; }
        public TextBox PathTextBox { get; }
        public string CurrentPath { get; private set; } = string.Empty;

        public EventHandler? Activated;

        public FilePanelController(ListView listView, TextBox pathTextBox)
        {
            ListView = listView ?? throw new ArgumentNullException(nameof(listView));
            PathTextBox = pathTextBox ?? throw new ArgumentNullException(nameof(pathTextBox));

            ConfigureListView();

            ListView.Enter += (object? s, EventArgs e) => Activated?.Invoke(this, EventArgs.Empty);

            string defaultPath = @"C:\";

            if (Directory.Exists(defaultPath))
            {
                LoadDirectory(defaultPath);
            }
        }

        private void ConfigureListView()
        {
            ListView.AllowDrop = true;
            ListView.View = View.Details;
            ListView.FullRowSelect = true;

        }
        public void BindPathDropdown(Button dropdownButton)
        {
            if (dropdownButton == null) throw new ArgumentNullException(nameof(dropdownButton));

            ContextMenuStrip menu = new ContextMenuStrip();

            // Prevent multiple bindings
            if (dropdownButton.Tag as string == "Bound") return;
            dropdownButton.Tag = "Bound";

            dropdownButton.Click += (s, e) =>
            {
                menu.Items.Clear();

                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo d in drives.OrderBy(d => d.Name, StringComparer.OrdinalIgnoreCase))
                {
                    string display = d.IsReady ? $"{d.Name} ({d.DriveType})" : d.Name;
                    ToolStripMenuItem item = new ToolStripMenuItem(display)
                    {
                        Tag = d
                    };
                    item.Click += (_, __) =>
                    {
                        try
                        {
                            string path = d.IsReady ? d.RootDirectory.FullName : d.Name;
                            if (Directory.Exists(path))
                            {
                                LoadDirectory(path);
                            }
                            else
                            {
                                MessageBox.Show($"Drive not ready: {path}", "Open", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Cannot open drive: {ex.Message}", "Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };
                    menu.Items.Add(item);
                }

                menu.Show(dropdownButton, new Point(0, dropdownButton.Height));
            };
        }

        public void LoadDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return;
            if (!Directory.Exists(path))
            {
                MessageBox.Show($"Directory does not exist: {path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ListView.BeginUpdate();
            try
            {
                ListView.Items.Clear();
                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                if (directoryInfo.Parent != null)
                {
                    ListViewItem parentItem = new ListViewItem("..") { Tag = directoryInfo.Parent };
                    // Ensure columns: Type, Size, Creation Date - add empty placeholders then creation date if available
                    parentItem.SubItems.Add(string.Empty); // Type
                    parentItem.SubItems.Add(string.Empty); // Size
                    parentItem.SubItems.Add(string.Empty); // Creation Date
                    ListView.Items.Add(parentItem);
                }
                IOrderedEnumerable<DirectoryInfo> directories = directoryInfo.EnumerateDirectories().OrderBy(d => d.Name, StringComparer.OrdinalIgnoreCase);
                foreach (DirectoryInfo d in directories)
                {
                    ListViewItem item = new ListViewItem(d.Name) { Tag = d };
                    item.SubItems.Add("Folder"); // Type
                    item.SubItems.Add(string.Empty); // Size
                    item.SubItems.Add(d.CreationTime.ToString("g")); // Creation Date
                    ListView.Items.Add(item);
                }
                IOrderedEnumerable<FileInfo> files = directoryInfo.EnumerateFiles().OrderBy(f => f.Name, StringComparer.OrdinalIgnoreCase);
                {
                    foreach (FileInfo f in files)
                    {
                        ListViewItem item = new ListViewItem(f.Name) { Tag = f };
                        string ext = string.IsNullOrEmpty(f.Extension) ? "File" : f.Extension.TrimStart('.');
                        item.SubItems.Add(ext); // Type (extension)
                        item.SubItems.Add(FormatSize(f.Length)); // Size
                        item.SubItems.Add(f.CreationTime.ToString("g")); // Creation Date
                        ListView.Items.Add(item);
                    }

                    CurrentPath = path;
                    // Update path textbox via BeginInvoke to ensure UI update when called during construction
                    try
                    {
                        if (PathTextBox.IsHandleCreated)
                        {
                            PathTextBox.BeginInvoke((Action)(() => PathTextBox.Text = path));
                        }
                        else
                        {
                            PathTextBox.Text = path;
                        }
                    }
                    catch
                    {
                        // ignore invoke errors
                        PathTextBox.Text = path;
                    }
                }
            }
            finally
            {
                ListView.EndUpdate();
            }
        }

        public string[] GetSelectedPaths()
        {
            List<string> result = new List<string>();
            foreach (ListViewItem item in ListView.SelectedItems)
            { 
                if (item.Tag is FileInfo fileInfo)
                {
                    result.Add(fileInfo.FullName);
                }
                else if (item.Tag is DirectoryInfo directoryInfo)
                {
                    result.Add(directoryInfo.FullName);
                }
            }

            return result.ToArray();
        }

        internal void OpenSelectedItem()
        {
            if (ListView.SelectedItems.Count == 0) return;
            ListViewItem item = ListView.SelectedItems[0];

            switch (item)
            {
                case { Text: "..", Tag: DirectoryInfo parent }:
                    LoadDirectory(parent.FullName);
                    break;

                case { Tag: DirectoryInfo directoryInfo }:
                    LoadDirectory(directoryInfo.FullName);
                    break;

                case { Tag: FileInfo file }:
                    try
                    {
                        Process.Start(new ProcessStartInfo(file.FullName) { UseShellExecute = true });
                    }
                    catch ( Exception ex )
                    {
                        MessageBox.Show($"Cannot open file: {ex.Message}", "Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                default:
                    // Do nothing
                    break;
            }
        }

        public void StartDrag()
        {
            string[] selectedPaths = GetSelectedPaths();
            if (selectedPaths.Length == 0) return;
            DataObject dataObject = new DataObject(DataFormats.FileDrop, selectedPaths);
            ListView.DoDragDrop(dataObject, DragDropEffects.Copy);
        }

        public void HandleDragEnter(DragEventArgs e)
        {
            bool accept = e.Data.GetDataPresent(DataFormats.FileDrop);
            e.Effect = accept ? DragDropEffects.Copy : DragDropEffects.None;
        }

        public void HandleDragDrop(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            object? raw = e.Data.GetData(DataFormats.FileDrop);
            if (raw is not string[] sources) return;

            if (string.IsNullOrWhiteSpace(CurrentPath))
            {
                MessageBox.Show("Target path is empty. Open a folder in the target panel first.", "Drop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                foreach (string src in sources)
                {
                    // avoid copying into itself
                    string name = Path.GetFileName(src.TrimEnd(Path.DirectorySeparatorChar));
                    string dest = Path.Combine(CurrentPath, name);

                    // If source equals destination (same file/folder) skip
                    if (string.Equals(Path.GetFullPath(src).TrimEnd(Path.DirectorySeparatorChar), Path.GetFullPath(dest).TrimEnd(Path.DirectorySeparatorChar), StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (Directory.Exists(src))
                    {
                        CopyDirectory(src, dest);
                    }
                    else if (File.Exists(src))
                    {
                        try
                        {
                            if (File.Exists(dest))
                            {
                                // skip existing file
                                continue;
                            }

                            File.Copy(src, dest, overwrite: false);
                        }
                        catch (Exception ex)
                        {
                            // continue copying other files; optionally log or show a message
                            Debug.WriteLine($"Failed to copy file '{src}' -> '{dest}': {ex.Message}");
                        }
                    }
                }

                LoadDirectory(CurrentPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during drop/copy: {ex.Message}", "Drop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SortByColumn(int column)
        {
            SortOrder current = ListView.Sorting;
            SortOrder next = current == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            ListView.Sorting = next;
            ListView.ListViewItemSorter = new ListViewItemComparer(column, next);
            ListView.Sort();
        }

        public void CopySelectedTo(string targetDirectory)
        {
            if (string.IsNullOrWhiteSpace(targetDirectory))
            {
                MessageBox.Show("Target directory is empty.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string[] selected = GetSelectedPaths();
            if (selected.Length == 0) return;

            try
            {
                foreach (string src in selected)
                {
                    string name = Path.GetFileName(src.TrimEnd(Path.DirectorySeparatorChar));
                    string dest = Path.Combine(targetDirectory, name);

                    if (Directory.Exists(src))
                    {
                        CopyDirectory(src, dest);
                    }
                    else if (File.Exists(src))
                    {
                        try
                        {
                            if (File.Exists(dest))
                            {
                                // skip existing
                                continue;
                            }
                            File.Copy(src, dest, overwrite: false);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Copy failed for '{src}' -> '{dest}': {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Copy failed: {ex.Message}", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CreateFolder(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return;
            if (string.IsNullOrWhiteSpace(CurrentPath))
            {
                MessageBox.Show("Active panel has no open directory. Open a folder first.", "Create Folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string newPath = Path.Combine(CurrentPath, name);
                Directory.CreateDirectory(newPath);
                LoadDirectory(CurrentPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create folder: {ex.Message}", "Create Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeleteSelected()
        {
            string[] selected = GetSelectedPaths();
            if (selected.Length == 0) return;

            DialogResult confirm = MessageBox.Show("Delete selected items? This cannot be undone.", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                foreach (string p in selected)
                {
                    if (Directory.Exists(p))
                    {
                        Directory.Delete(p, true);
                    }
                    else if (File.Exists(p))
                    {
                        File.Delete(p);
                    }
                }

                if (!string.IsNullOrWhiteSpace(CurrentPath))
                {
                    LoadDirectory(CurrentPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete failed: {ex.Message}", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void CopyDirectory(string sourceDirectory, string destinationDirectory)
        {
            // Create destination if needed
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            string[] files = Array.Empty<string>();
            try
            {
                files = Directory.GetFiles(sourceDirectory);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to enumerate files in '{sourceDirectory}': {ex.Message}");
            }

            foreach (string file in files)
            {
                try
                {
                    string destFile = Path.Combine(destinationDirectory, Path.GetFileName(file));
                    if (File.Exists(destFile))
                    {
                        continue;
                    }
                    File.Copy(file, destFile, overwrite: false);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to copy file '{file}': {ex.Message}");
                }
            }

            string[] dirs = Array.Empty<string>();
            try
            {
                dirs = Directory.GetDirectories(sourceDirectory);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to enumerate directories in '{sourceDirectory}': {ex.Message}");
            }

            foreach (string dir in dirs)
            {
                try
                {
                    string destSubDir = Path.Combine(destinationDirectory, Path.GetFileName(dir));
                    CopyDirectory(dir, destSubDir);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to copy directory '{dir}': {ex.Message}");
                }
            }
        }

        private static string FormatSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes} B";
            double kb = bytes / 1024.0;
            if (kb < 1024) return $"{kb:F1} KB";
            double mb = kb / 1024.0;
            if (mb < 1024) return $"{mb:F1} MB";
            double gb = mb / 1024.0;
            return $"{gb:F1} GB";
        }
    }

    internal class  ListViewItemComparer : System.Collections.IComparer
    {
        private readonly int columnIndex;
        private readonly SortOrder sortOrder;

        public ListViewItemComparer(int column, SortOrder order)
        {
            columnIndex = column;
            sortOrder = order;
        }

        public int Compare(object? x, object? y)
        {
            if (x is not ListViewItem a || y is not ListViewItem b) return 0;

            int result;
            if (columnIndex == 0)
            {
                result = string.Compare(a.Text, b.Text, StringComparison.OrdinalIgnoreCase);
            }
            else if (columnIndex == 3)
            {
                DateTime ta = GetCreationDateTime(a);
                DateTime tb = GetCreationDateTime(b);
                result = DateTime.Compare(ta, tb);
            }
            else if (columnIndex == 2)
            {
                // Size column
                long sa = GetSize(a);
                long sb = GetSize(b);
                result = sa.CompareTo(sb);
            }
            else
            {
                // Type or other text columns
                string ta = a.SubItems.Count > columnIndex ? a.SubItems[columnIndex].Text : string.Empty;
                string tb = b.SubItems.Count > columnIndex ? b.SubItems[columnIndex].Text : string.Empty;
                result = string.Compare(ta, tb, StringComparison.OrdinalIgnoreCase);
            }
            return sortOrder == SortOrder.Ascending ? result : -result;
        }
        private static DateTime GetCreationDateTime(ListViewItem item)
        {
            // Prefer the Creation Date subitem at index 3 (Name=0, Type=1, Size=2, Creation=3)
            if (item.SubItems.Count > 3 && DateTime.TryParse(item.SubItems[3].Text, out DateTime parsed)) return parsed;
            // fallback to tag
            return item.Tag switch
            {
                FileInfo fileInfo => fileInfo.CreationTime,
                DirectoryInfo dirInfo => dirInfo.CreationTime,
                _ => DateTime.MinValue
            };
        }

        private static long GetSize(ListViewItem item)
        {
            if (item.Tag is FileInfo fi) return fi.Length;
            return 0;
        }
    }
} 
            

