using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevWinUI;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;
namespace RisohEditorWinUI3Blank.DataManagement
{
    public class DataHelper
    {

        public static async Task<string> OpenFile(object Handle,List<string> Filters)
        {
            var picker = new FileOpenPicker()
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.Desktop
            };

            // 按需添加过滤器
            picker.FileTypeFilter.Clear();
            picker.FileTypeFilter.AddRange(Filters);

            // 将 WinUI 窗口句柄传入 Picker（必要）
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(Handle);
            InitializeWithWindow.Initialize(picker, hwnd);

            string SetFilePath = "";
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // 如果需要绝对路径（桌面应用通常可用），可用 file.Path
                SetFilePath = file.Path;
               
            }

            return SetFilePath;
        }
    }
}
