using AYam.Common.MVVM;
using System;
using System.Drawing;
using System.IO;

namespace ListBoxSample.Class
{

    /// <summary>
    /// 指定フォルダのファイル情報
    /// </summary>
    public class FileItem : ViewModelBase, IDisposable
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
        /// ファイル名
        /// </summary>
        private string _Name = "";

        /// <summary>
        /// ファイル名プロパティ
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
        /// ファイルサイズ
        /// </summary>
        private long _Size = 0;

        /// <summary>
        /// ファイルサイズプロパティ
        /// </summary>
        public string Size
        {
            get { return _Size.ToString("#,0"); }
            set
            {

                if (long.TryParse(value, out long numValue))
                {
                    _Size = numValue;
                }
                else
                {
                    _Size = 0;
                }

                CallPropertyChanged();

            }
        }

        /// <summary>
        /// 作成日時
        /// </summary>
        private DateTime _CreationTime = new DateTime(1900, 1, 1);

        /// <summary>
        /// 作成日時プロパティ
        /// </summary>
        public string CreationTime
        {
            get
            { return _CreationTime.ToString("yyyy/MM/dd HH:mm:ss"); }
            set
            {

                if (DateTime.TryParse(value, out DateTime dtValue))
                {
                    _CreationTime = dtValue;
                }
                else
                {
                    _CreationTime = new DateTime(1900, 1, 1);
                }

                CallPropertyChanged();

            }
        }

        /// <summary>
        /// ファイルアイコン画像イメージ
        /// </summary>
        private Bitmap _BitmapImage = null;

        /// <summary>
        /// ファイルアイコン画像イメージプロパティ
        /// </summary>
        public Bitmap BitmapImage
        {
            get { return _BitmapImage; }
            set
            {
                _BitmapImage = value;
                CallPropertyChanged();
            }
        }


        #endregion

        /// <summary>
        /// ファイルアイコン
        /// </summary>
        private Icon _Icon = null;

        /// <summary>
        /// 指定フォルダのファイル情報
        /// </summary>
        /// <param name="fileFullPath">ファイルフルパス</param>
        public FileItem(string fileFullPath)
        {

            FullPath = fileFullPath;
            SetFileInfo();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            _Icon.Dispose();
            BitmapImage.Dispose();

        }

        /// <summary>
        /// ファイル情報のセット
        /// </summary>
        private void SetFileInfo()
        {

            if (File.Exists(FullPath))
            {

                var fileInfo = new FileInfo(FullPath);
                Name = fileInfo.Name;
                Size = fileInfo.Length.ToString();
                CreationTime = fileInfo.CreationTime.ToString("yyyy/MM/dd HH:mm:ss");
                _Icon = Icon.ExtractAssociatedIcon(FullPath);
                BitmapImage = _Icon.ToBitmap();

            }

        }

    }

}
