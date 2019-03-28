using AYam.Common.ViewModel;
using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace ListBoxSample.Form.ViewModel
{

    /// <summary>
    /// StrageExplorer.ViewModel
    /// </summary>
    public class StrageExplorer : VMBase, IDisposable
    {

        #region Property

        /// <summary>
        /// ウィンドウ：タイトルプロパティ
        /// </summary>
        public string WindowTitle { get { return "エクスプローラ"; } }

        /// <summary>
        /// メニュー：ファイルプロパティ
        /// </summary>
        public string MenuFile { get { return "ファイル(_F)"; } }

        /// <summary>
        /// メニュー：編集プロパティ
        /// </summary>
        public string MenuEdit { get { return "編集(_E)"; } }

        /// <summary>
        /// メニュー：オプションプロパティ
        /// </summary>
        public string MenuOption { get { return "オプション(_O)"; } }

        /// <summary>
        /// フォルダ階層プロパティ
        /// </summary>
        public ObservableCollection<Class.PathTree> Paths
        {
            get { return _Model.Paths; }
            set
            {
                _Model.Paths = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 選択フォルダプロパティ
        /// </summary>
        public Class.PathTree SelectedDirectory
        {
            get { return _Model.SelectedDirectory; }
            set
            {
                _Model.SelectedDirectory = value;
                CallPropertiesChanged();
            }
        }

        /// <summary>
        /// 選択パスプロパティ
        /// </summary>
        public string SelectedPath
        {
            get
            {
                if (_Model.SelectedDirectory == null)
                {
                    return "";
                }
                else
                {

                    int lastIndex = _Model.SelectedDirectory.FullPath.LastIndexOf(@"\");
                    int length = _Model.SelectedDirectory.FullPath.Length;

                    // ドライブ名は"\"がデフォルトで付与されているが
                    // 下位フォルダは付与されていないので"\"を付与する
                    return _Model.SelectedDirectory.FullPath + (lastIndex.Equals(length - 1) ? "" : @"\");

                }
            }
            set
            {

                var returnValue = _Model.SelectPath(value, Paths);

                if (returnValue != null)
                {
                    _Model.SelectedDirectory = returnValue;
                }
                else
                {
                    CallPropertiesChanged();
                }

            }
        }

        /// <summary>
        /// 選択フォルダ内のファイル一覧プロパティ
        /// </summary>
        public ObservableCollection<Class.FileItem> Files
        {
            get { return _Model.Files; }
            set
            {
                _Model.Files = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 選択フォルダ内のファイル一覧プロパティ
        /// </summary>
        public IList SelectedFiles
        {
            get { return _Model.SelectedFiles; }
            set
            {
                _Model.SelectedFiles = value;
                CallPropertyChanged();
            }
        }

        #endregion

        /// <summary>
        /// StrageExplorer.Model
        /// </summary>
        private Model.StrageExplorer _Model;

        /// <summary>
        /// StrageExplorer.ViewModel
        /// </summary>
        public StrageExplorer()
        {

            _Model = new Model.StrageExplorer();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            _Model.Dispose();
            _Model = null;
        }

        /// <summary>
        /// 複数のプロパティチェンジ
        /// </summary>
        private void CallPropertiesChanged()
        {

            CallPropertyChanged(nameof(SelectedDirectory));
            CallPropertyChanged(nameof(SelectedPath));
            CallPropertyChanged(nameof(Files));

        }

    }
}