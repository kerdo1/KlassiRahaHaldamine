﻿#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

using Microsoft.EntityFrameworkCore;
using KlassiRahaHaldamine.Data;

namespace KlassiRahaHaldamine
{
    public partial class App : Application
    {
        const int WindowWidth = 540;
        const int WindowHeight = 1000;
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage()); // Ensure a NavigationPage is used
            Application.Current.UserAppTheme = AppTheme.Light;

#if WINDOWS
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new SizeInt32(WindowWidth,WindowHeight));
        });
#endif

            MainPage = new AppShell();
        }
    }
}
