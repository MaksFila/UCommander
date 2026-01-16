# UCommander

**UCommander** is a dual-pane file manager for Windows, developed as a clone of Total Commander. It is built using **C#**, **Windows Forms**, and **.NET 10.0**.

The application provides a classic two-panel interface for file management, featuring drag-and-drop support, native Windows icon integration, and keyboard shortcuts for standard file operations.

## Key Features

* **Dual-Pane Interface:** Two independent file panels allow for simultaneous navigation of different directories.
* **File Operations:**
    * **Copy:** Copy files and directories recursively from the active panel to the target panel.
    * **Create Folder:** Create new directories within the current path.
    * **Delete:** Remove selected files or folders with a confirmation dialog.
* **Drag & Drop:** Support for dragging files between panels to initiate copy operations.
* **Shell Integration:**
    * Uses Win32 API (`shell32.dll`) to retrieve and display native system icons for files and folders.
    * Launches files in their default associated application upon activation.
* **Navigation & View:**
    * Drive selection menu showing available drives and their types.
    * Sort lists by Name, Type, Size, or Creation Date by clicking column headers.
    * Human-readable file size formatting (B, KB, MB, GB).

## Keyboard Shortcuts

The application implements standard file manager shortcuts:

| Key | Action | Description |
| :--- | :--- | :--- |
| **F5** | Copy | Copies selected items to the target panel's current directory. |
| **F7** | Create Folder | Opens a dialog to create a new folder in the active panel. |
| **F8** | Delete | Deletes selected items (requires confirmation). |

## Technical Details

* **Language:** C#
* **Framework:** .NET 10.0
* **UI Framework:** Windows Forms (WinForms)
* **Win32 API:** Implements P/Invoke (`SHGetFileInfo`, `DestroyIcon`) to handle system resource extraction.

## Project Structure

* **Form1.cs**: The main application window that manages the UI layout, initializes controllers, and handles global key events (F5, F7, F8).
* **FilePanelController.cs**: Encapsulates the logic for a single file panel, including directory enumeration, sorting logic, drag-and-drop handling, and file system manipulation.
* **FileIconController.cs**: Manages the retrieval of icons from the Windows Shell and maintains an image cache to optimize performance.

## Requirements

* **OS:** Windows (due to dependencies on `shell32.dll` and `user32.dll`).
* **Runtime:** .NET 10.0.
