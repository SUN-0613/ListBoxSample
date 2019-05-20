using AYam.Common.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace ListBoxSample.Class
{

    /// <summary>
    /// フォルダ階層
    /// </summary>
    public class PathTree : ViewModelBase, IDisposable
    {


        #region Property

        /// <summary>
        /// フルパス
        /// </summary>
        private string _FullPath = "";

        /// <summary>
        /// フルパスプロパティ
        /// </summary>
        public string FullPath
        {
            get { return _FullPath; }
            set
            {
                if (!_FullPath.Equals(value))
                {
                    _FullPath = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// フォルダ名
        /// </summary>
        private string _Name = "";

        /// <summary>
        /// フォルダ名プロパティ
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                if (!_Name.Equals(value))
                {
                    _Name = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 子階層
        /// </summary>
        public ObservableCollection<PathTree> Child { get; set; }

        #endregion

        /// <summary>
        /// フォルダ階層
        /// </summary>
        /// <param name="fullPath">フルパス</param>
        /// <param name="parent">親パス情報</param>
        public PathTree(string fullPath)
        {

            FullPath = fullPath;

            Name = Path.GetFileNameWithoutExtension(fullPath);
            if (Name.Length.Equals(0))
            {
                Name = FullPath;
            }

            MakeChildPath();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (Child != null)
            {

                for (int iLoop = 0; iLoop < Child.Count; iLoop++)
                {
                    Child[iLoop].Dispose();
                    Child[iLoop] = null;
                }

                Child.Clear();
                Child = null;

            }

        }

        /// <summary>
        /// 子階層のフォルダ一覧を取得
        /// </summary>
        private async void MakeChildPath()
        {

            // 初期化
            if (Child == null)
            {
                Child = new ObservableCollection<PathTree>();
            }

            Child.Clear();

            await Task.Run(() => 
            {

                try
                {

                    // サブフォルダ取得
                    if (FullPath != null && Directory.Exists(FullPath))
                    {

                        IEnumerable<string> paths = Directory.EnumerateDirectories(FullPath, "*", SearchOption.TopDirectoryOnly);

                        foreach (string path in paths)
                        {
                            Child.Add(new PathTree(path));
                        }

                    }

                }
                catch
                {
                    
                }

            });

        }

    }

}
