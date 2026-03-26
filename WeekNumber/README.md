# WPF Tray App

## Overview
This is a **WPF application** that runs **without a visible window** and exists only as a **tray icon** in the Windows taskbar.  

<img width="248" height="47" alt="grafik" src="https://github.com/user-attachments/assets/e598c17d-53cd-4282-968a-4f1ec0fe1360" />

**Features:**
- Minimal ;)
- Tray icon with dynamic display of the current **calendar week (CW)**.
- MVVM-compatible using **[Hardcodet.NotifyIcon.Wpf](https://www.nuget.org/packages/Hardcodet.NotifyIcon.Wpf/)**.
- Automatic updates of tooltip and icon (e.g., when the week changes).

---

## Requirements
- **.NET 8 / .NET Core WPF**
- NuGet packages:
  - `Hardcodet.NotifyIcon.Wpf`
  - `CommunityToolkit.Mvvm`
- Windows OS (taskbar icons are Windows-specific)

---

## Structure

### App.xaml
Defines the **TaskbarIcon** as a resource with a context menu. No startup window is used (`ShutdownMode="OnExplicitShutdown"`).

```xml
<Application x:Class="TrayApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:tb="http://www.hardcodet.net/taskbar"
             ShutdownMode="OnExplicitShutdown">

    <Application.Resources>
        <tb:TaskbarIcon x:Key="TrayIcon"
                        IconSource="Icons/app.ico"
                        ToolTipText="Tray App">
            <tb:TaskbarIcon.ContextMenu>
                <ContextMenu>
                    <MenuItem Header="Exit" Click="Exit_Click"/>
                </ContextMenu>
            </tb:TaskbarIcon.ContextMenu>
        </tb:TaskbarIcon>
    </Application.Resources>
</Application>
