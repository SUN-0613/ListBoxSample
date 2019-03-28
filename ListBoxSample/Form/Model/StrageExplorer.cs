using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace ListBoxSample.Form.Model
{

    /// <summary>
    /// StrageExplorer.Model
    /// </summary>
    public class StrageExplorer : IDisposable
    {

        #region ViewModel.Property

        /// <summary>
        /// フォルダ階層
        /// </summary>
        public ObservableCollection<Class.PathTree> Paths;

        /// <summary>
        /// 選択フォルダ
        /// </summary>
        private Class.PathTree _SelectedDirectory = null;

        /// <summary>
        /// 選択フォルダプロパティ
        /// </summary>
        public Class.PathTree SelectedDirectory
        {
            get { return _SelectedDirectory; }
            set
            {
                _SelectedDirectory = value;
                MakeFiles();
            }
        }

        /// <summary>
        /// 選択フォルダ内のファイル一覧
        /// </summary>
        public ObservableCollection<Class.FileItem> Files;

        /// <summary>
        /// 選択ファイル一覧
        /// </summary>
        public IList SelectedFiles;

        #endregion

        /// <summary>
        /// StrageExplorer.Model
        /// </summary>
        public StrageExplorer()
        {

            MakeDriveList();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (Paths != null)
            {
                
                for (int iLoop = 0; iLoop < Paths.Count; iLoop++)
                {
                    Paths[iLoop].Dispose();
                    Paths[iLoop] = null;
                }

                Paths.Clear();
                Paths = null;

            }

            if (Files != null)
            {

                for (int iLoop = 0; iLoop < Files.Count; iLoop++)
                {
                    Files[iLoop].Dispose();
                    Files[iLoop] = null;
                }

                Files.Clear();
                Files = null;

            }

        }

        /// <summary>
        /// ドライブ一覧取得
        /// </summary>
        private async void MakeDriveList()
        {

            // 初期化
            if (Paths == null)
            {
                Paths = new ObservableCollection<Class.PathTree>();
            }
            else
            {

                for (int iLoop = 0; iLoop < Paths.Count; iLoop++)
                {
                    Paths[iLoop].Dispose();
                    Paths[iLoop] = null;
                }

            }

            Paths.Clear();

            await Task.Run(() => 
            {

                try
                {

                    // ドライブ一覧取得
                    foreach (string drive in Environment.GetLogicalDrives())
                    {
                        Paths.Add(new Class.PathTree(drive));
                    }

                }
                catch { }

            });

        }

        /// <summary>
        /// 選択フォルダ内のファイル一覧取得
        /// </summary>
        private void MakeFiles()
        {

            // 初期化
            if (Files == null)
            {
                Files = new ObservableCollection<Class.FileItem>();
            }
            else
            {

                for (int iLoop = 0; iLoop < Files.Count; iLoop++)
                {
                    Files[iLoop].Dispose();
                    Files[iLoop] = null;
                }

            }

            Files.Clear();

            try
            {

                // ファイル一覧取得
                foreach (string filePath in Directory.GetFiles(SelectedDirectory.FullPath))
                {
                    Files.Add(new Class.FileItem(filePath));
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// 入力されたパスの検索
        /// </summary>
        /// <param name="inputPath">入力パス</param>
        /// <param name="paths">フォルダ階層</param>
        /// <param name="lastIndex">再帰用:前回検索パス開始位置</param>
        public Class.PathTree SelectPath(string inputPath, ObservableCollection<Class.PathTree> paths, int lastIndex = 0)
        {

            // 入力値は存在するか
            if (Directory.Exists(inputPath))
            {

                // 上位フォルダから検索していく
                int index = -1;

                if (inputPath.Length < lastIndex + 1)
                {
                    index = inputPath.IndexOf(@"\", lastIndex + 1);
                }

                string str = index.Equals(-1) ? inputPath : inputPath.Substring(0, index + 1);

                for (int iLoop = 0; iLoop < paths.Count; iLoop++)
                {

                    // 該当フォルダ発見
                    if (paths[iLoop].FullPath.Equals(str))
                    {

                        if (str.Equals(inputPath))
                        {
                            // 入力値と完全一致すればループ完了
                            // CallPropertyChanged()はSelectedDirectory内にて実行
                            return paths[iLoop];
                            
                        }
                        else
                        {
                            // 引き続き下位フォルダを検索する
                            // 該当Itemを発見するまで再帰
                            return SelectPath(inputPath, paths[iLoop].Child, index);
                        }

                    }

                }

            }

            // 見つからなかった
            return null;

        }



    }

}
