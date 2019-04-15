using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace ListBoxSample.Custom.UserControls
{

    /// <summary>
    /// ファイル情報表示用ユーザコントロール
    /// </summary>
    public partial class ListViewItem : UserControl
    {

        #region DependencyProperty

        /// <summary>
        /// Size依存プロパティ
        /// </summary>
        public static readonly DependencyProperty SizeProperty
                                        = DependencyProperty.Register(
                                            nameof(Size)
                                            , typeof(string)
                                            , typeof(ListBoxSample.Custom.UserControls.ListViewItem)
                                            , new UIPropertyMetadata(string.Empty));

        /// <summary>
        /// 作成日時依存プロパティ
        /// </summary>
        public static readonly DependencyProperty CreationProperty
                                        = DependencyProperty.Register(
                                            nameof(Creation)
                                            , typeof(string)
                                            , typeof(ListBoxSample.Custom.UserControls.ListViewItem)
                                            , new UIPropertyMetadata(string.Empty));

        /// <summary>
        /// ファイルパス依存プロパティ
        /// </summary>
        public static readonly DependencyProperty PathProperty
                                        = DependencyProperty.Register(
                                            nameof(Path)
                                            , typeof(string)
                                            , typeof(ListBoxSample.Custom.UserControls.ListViewItem)
                                            , new UIPropertyMetadata(string.Empty));

        /// <summary>
        /// アイコン画像依存プロパティ
        /// </summary>
        public static readonly DependencyProperty BitmapImageProperty
                                        = DependencyProperty.Register(
                                            nameof(BitmapImage)
                                            , typeof(Bitmap)
                                            , typeof(ListBoxSample.Custom.UserControls.ListViewItem)
                                            , new FrameworkPropertyMetadata(null));

        #endregion

        #region Property

        /// <summary>
        /// Sizeプロパティ
        /// </summary>
        public string Size
        {
            get { return (string)GetValue(SizeProperty); }
            set
            {
                SetValue(SizeProperty, value);
                
            }
        }

        /// <summary>
        /// 作成日時プロパティ
        /// </summary>
        public string Creation
        {
            get { return (string)GetValue(CreationProperty); }
            set
            {
                SetValue(CreationProperty, value);
            }
        }

        /// <summary>
        /// ファイルパスプロパティ
        /// </summary>
        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set
            {
                SetValue(PathProperty, value);
            }
        }

        /// <summary>
        /// アイコン画像プロパティ
        /// </summary>
        public Bitmap BitmapImage
        {
            get { return (Bitmap)GetValue(BitmapImageProperty); }
            set
            {
                SetValue(BitmapImageProperty, value);
            }
        }

        #endregion

        /// <summary>
        /// ファイル情報表示用ユーザコントロール
        /// </summary>
        public ListViewItem()
        {
            InitializeComponent();
        }

    }
}
