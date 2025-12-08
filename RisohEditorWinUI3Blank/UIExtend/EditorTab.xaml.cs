
using System.Xml.Serialization;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RisohEditorWinUI3Blank.UIExtend
{
    public sealed partial class EditorTab : UserControl
    {
        public delegate void FileClose(string FileName);
        public static FileClose? OneFileClose = null;

        public delegate void Click(string FileName);
        public static Click? OneFileClick = null;

       
        public EditorTab(string UniqueKey,string FileName)
        {
            this.InitializeComponent();
            this.UniqueKey = UniqueKey;
            this.FileName = FileName;
        }
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set
            {
                SetValue(FileNameProperty, value);
                SetFileName(value);
            }
        }

        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register(
                nameof(FileName),
                typeof(string),
                typeof(EditorTab),
                new PropertyMetadata(string.Empty)
        );

        private string UniqueKey
        {
            get { return (string)GetValue(UniqueKeyProperty); }
            set
            {
                SetValue(UniqueKeyProperty, value);
            }
        }

        public static readonly DependencyProperty UniqueKeyProperty =
         DependencyProperty.Register(
             nameof(UniqueKey),
             typeof(string),
             typeof(EditorTab),
             new PropertyMetadata(string.Empty)
        );

        public static string ActiveTabName = "";
        public EditorTab()
        {
            this.InitializeComponent();
        }

        public void SetFileName(string FileName)
        {
            this.LFileName.Text = FileName;
        }

        private void FontIcon_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (this.Parent is StackPanel Panel)
            {
                Panel.Children.Remove(this);
            }

            if (EditorTab.OneFileClose!=null)
            EditorTab.OneFileClose(this.UniqueKey);
        }

        public Color EnterColor = Color.FromArgb(255, 50, 50, 50);
        public Color LeaveColor = Color.FromArgb(255, 39, 39, 39);

        private Border? LastEnterBorder;
        private void Border_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Border SelfBorder = ((Border)sender);
            SelfBorder.Background = new SolidColorBrush(EnterColor);
            LastEnterBorder = SelfBorder;
        }

        public void Border_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Close();
        }

        private void Close()
        {
            if (EditorTab.ActiveTabName != this.UniqueKey)
                MainBorder.Background = new SolidColorBrush(LeaveColor);
        }

        private void Border_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            EditorTab.ActiveTabName = this.UniqueKey;

            if (LastEnterBorder != null)
                LastEnterBorder.Background = new SolidColorBrush(EnterColor);

            ResetColor();

            if (EditorTab.OneFileClick != null)
            {
                EditorTab.OneFileClick(this.UniqueKey);
            }
        }

        public void ResetColor()
        {
            if (this.Parent is StackPanel ParentPanel)
            {
                foreach (var Child in ParentPanel.Children)
                {
                    ((EditorTab)Child).Close();
                }
            }
        }
    }
}
