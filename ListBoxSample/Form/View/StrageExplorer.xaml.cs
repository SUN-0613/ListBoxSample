using System;
using System.ComponentModel;
using System.Windows;

namespace ListBoxSample.Form.View
{
    /// <summary>
    /// StrageExplorer.xaml の相互作用ロジック
    /// </summary>
    public partial class StrageExplorer : Window, IDisposable
    {

        /// <summary>
        /// StrageExplorer.View
        /// </summary>
        public StrageExplorer()
        {

            InitializeComponent();

            if (DataContext is ViewModel.StrageExplorer viewModel)
            {
                viewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (DataContext is ViewModel.StrageExplorer viewModel)
            {

                viewModel.PropertyChanged -= OnPropertyChanged;

                viewModel.Dispose();
                viewModel = null;

            }

        }

        /// <summary>
        /// ViewModelプロパティ変更イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {
                default:
                    break;
            }

        }

    }
}
