using System.Xml.Linq;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using RisohEditorWinUI3Blank.DataManagement;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RisohEditorWinUI3Blank.UIExtend
{
    public sealed class EditorTab : TabViewItem
    {
        public string UniqueKey = "";

        public EditorTab()
        {
            this.Content = new Grid
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 39, 39, 39)),
            };

            this.Resources = new ResourceDictionary
            {
                { "TabViewItemHeaderBackgroundSelected", new SolidColorBrush(Color.FromArgb(255, 50, 50, 50)) },
                { "TabViewItemHeaderBackgroundPointerOver", new SolidColorBrush(Color.FromArgb(255, 50, 50, 50)) }
            };
        }

      
    }
    public class TabManager
    {
        public string BlankName = "about:blank";
        public TabView ?Parent = null;

        public TabClick? Tab_Click = null;
        public delegate void TabClick(string UniqueKey);
           
        public TabManager(TabView Parent)
        { 
            this.Parent = Parent;

            EditorTab EmptyTab = new EditorTab { Header = BlankName, UniqueKey = "", Style = (Style)Application.Current.Resources["EditorTabStyle"] };
            EmptyTab.Tapped += Tab_Tapped;
            EmptyTab.CloseRequested += Remove;

            this.Parent.TabItems.Add(EmptyTab);
        }
        public EditorTab? KeyToEditorTab(string UniqueKey)
        {
            if(Parent!=null)
            foreach (var GetChild in Parent.TabItems)
            {
                if (GetChild is EditorTab)
                {
                    EditorTab CurrentTab = (EditorTab)GetChild;
                    if (CurrentTab.UniqueKey.Equals(UniqueKey))
                    {
                        return CurrentTab;
                    }
                }
            }

            return null;
        }
        private void Tab_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string? GetKey = ((EditorTab)sender).UniqueKey;

            if (GetKey != null)
            {
                Tab_Click?.Invoke(GetKey);
            }
        } 
        public EditorTab? Add(string UniqueKey,string FileName)
        {
            if (Parent == null) return null;

            var CheckTab = KeyToEditorTab(UniqueKey);
            if (CheckTab == null)
            {
                EditorTab Tab = new EditorTab { Header = FileName, UniqueKey = UniqueKey, Style = (Style)Application.Current.Resources["EditorTabStyle"] };
                Tab.Tapped += Tab_Tapped;
                Tab.CloseRequested += Remove;

                if (GetCount() > 0)
                {
                    Parent?.TabItems.Add(Tab);
                }
                else
                {
                    var TabHandle = GetBlankTab();

                    if (TabHandle != null)
                    {
                        TabHandle.Header = FileName;
                        TabHandle.UniqueKey = UniqueKey;
                    }
                    else
                    {
                        Parent?.TabItems.Add(Tab);
                    }
                }
            }
            else
            {
                CheckTab.IsSelected = true;//Active Tab
                return CheckTab;
            }

            return null;
        }
        public void Remove(EditorTab Tab)
        {
            Remove(Tab, null);
        }
        private void Remove(TabViewItem sender, TabViewTabCloseRequestedEventArgs args)
        {
            if (GetCount() == 0) return;

            ((TabViewListView)sender.Parent).Items.Remove(sender);

            if (GetCount() == 0)
            {
                Add(string.Empty, BlankName);
            }
        }
        public int GetCount()
        {
            if (Parent != null)
            {
                int Count = 0;
                foreach (var GetChildControl in Parent.TabItems)
                {
                    if (GetChildControl is EditorTab)
                    {
                        string GetName = ConvertHelper.ObjToStr(((EditorTab)GetChildControl).Header);

                        if (GetName != BlankName)
                        {
                            Count++;
                        }
                    }
                }
                return Count;
            }

            return -1;
        }

        public EditorTab? GetBlankTab()
        {
            foreach (var GetChildControl in Parent.TabItems)
            {
                if (GetChildControl is EditorTab)
                {
                    string GetName = ConvertHelper.ObjToStr(((EditorTab)GetChildControl).Header);

                    if (GetName == BlankName)
                    {
                        return (EditorTab)GetChildControl;
                    }
                }
            }

           return null;
        }
    }
}
