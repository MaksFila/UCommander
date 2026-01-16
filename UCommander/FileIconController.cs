using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace UCommander
{
    internal class FileIconController
    {
        private ImageList imageList = new ImageList();
        private Dictionary<string, int> iconCache = new Dictionary<string, int>();
        private ListView listView;

        private const uint SHGFI_ICON = 0x000000100; //Get icon
        private const uint SHGFI_SMALLICON = 0x000000001; // Small Icon
        private const uint SHGFI_LARGEICON = 0x000000000; // Big Icon
        private const uint SHGFI_USEFILEATTRIBUTES = 0x000000010; // Dont check if the file exists
        private const uint FILE_ATTRIBUTE_NORMAL = 0x00000080; // Normal File
        private const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010; //Catalouge

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SHGetFileInfo(
            string pszPath,
            uint dwFileAttributes,
            ref SHFILEINFO psfi,
            uint cbFileInfo,
            uint uFlags
        );

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyIcon(IntPtr hIcon);

        public FileIconController(ListView listView)
        {
            this.listView = listView ?? throw new ArgumentNullException(nameof(listView));
            ConfigureImageList();
        }

        private void ConfigureImageList()
        {
            listView.SmallImageList = imageList;
            int iconSize = listView.Font.Height;
            imageList.ImageSize = new Size(iconSize, iconSize);
        }

        public void SetItemIcon(ListViewItem item, string key)
        {
            if (!iconCache.ContainsKey(key))
            {
                Size targetSize = imageList.ImageSize;

                Image bitmap = TryToGetShellIcon(item, key, targetSize)
                    ?? TryExtractAssociatedIcon(item, targetSize)
                    ?? GetFallbackIcon(targetSize);

                imageList.Images.Add(bitmap);
                iconCache[key] = imageList.Images.Count - 1;
            }

            item.ImageIndex = iconCache[key];
        }
        private Image TryToGetShellIcon(ListViewItem item, string key, Size targetSize)
        {
            try
            {
                SHFILEINFO shellFileInfo = new SHFILEINFO();
                uint flags = SHGFI_ICON | SHGFI_SMALLICON | SHGFI_USEFILEATTRIBUTES;

                string path = key;
                uint fileAttributes = FILE_ATTRIBUTE_NORMAL;

                if (item.Tag is DirectoryInfo)
                {
                    fileAttributes = FILE_ATTRIBUTE_DIRECTORY;
                }
                else if (item.Tag is FileInfo)
                {
                    if (!key.StartsWith('.'))
                    {
                        path = "." + key;
                    }
                }

                IntPtr result = SHGetFileInfo(
                    path,
                    fileAttributes,
                    ref shellFileInfo,
                    (uint)Marshal.SizeOf(shellFileInfo),
                    flags);

                if (result != IntPtr.Zero && shellFileInfo.hIcon != IntPtr.Zero)
                {
                    Icon icon = Icon.FromHandle(shellFileInfo.hIcon);
                    Bitmap bitmapFromShell = new Bitmap(targetSize.Width, targetSize.Height);
                    using (Graphics graphics = Graphics.FromImage(bitmapFromShell))
                    {
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.DrawIcon(icon, new Rectangle(0, 0, bitmapFromShell.Width, bitmapFromShell.Height));
                    }
                    DestroyIcon(shellFileInfo.hIcon);

                    return bitmapFromShell;
                }
            }
            catch
            {
                // ignore and return null
            }
            return null;
        }
        private Image TryExtractAssociatedIcon(ListViewItem item, Size targetSize)
        {
            if (item.Tag is not FileSystemInfo fileSystemInfo)
            {
                return null;
            }

            try
            {
                Icon extracted = Icon.ExtractAssociatedIcon(fileSystemInfo.FullName);
                if (extracted != null)
                {
                    Bitmap bitmapFromExtracted = new Bitmap(targetSize.Width, targetSize.Height);
                    using (Graphics graphics = Graphics.FromImage(bitmapFromExtracted))
                    {
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.DrawIcon(extracted, new Rectangle(0, 0, bitmapFromExtracted.Width, bitmapFromExtracted.Height));
                    }
                    return bitmapFromExtracted;
                }
            }
            catch
            {
                // ignore
            }

            return null;
        }
        private Image GetFallbackIcon(Size targetSize)
        {
            Icon systemIcon = SystemIcons.Application;
            Bitmap bitmapFromSystemIcon = new Bitmap(targetSize.Width, targetSize.Height);
            using (Graphics graphics = Graphics.FromImage(bitmapFromSystemIcon))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawIcon(systemIcon, new Rectangle(0, 0, bitmapFromSystemIcon.Width, bitmapFromSystemIcon.Height));
            }
            return bitmapFromSystemIcon;
        }
    }
}

