using System;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ookii.Dialogs.Wpf;
using System.Collections.ObjectModel;
using System.IO;

namespace PDFREnamer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        string folderPath = "";
        ObservableCollection<string> paths;
        int position;
        private DispatcherTimer aTimer;


        public MainWindow() {
            InitializeComponent();

            paths = new ObservableCollection<string>();
            PDFList.ItemsSource = paths;

            var uc = new PDFView();

            this.windowsFormsHost.Child = uc;

            // Create a timer with a two second interval.
            aTimer = new DispatcherTimer();

            aTimer.Interval = new TimeSpan(0, 0, 1);

            aTimer.Tick += OnTimedEvent;


        }


        private void Load_New_Folder_Click(object sender, RoutedEventArgs e) {
            VistaFolderBrowserDialog cofd = new VistaFolderBrowserDialog();
            
            cofd.ShowDialog();

            folderPath = cofd.SelectedPath;

            paths.Clear();

            textBox.Focusable = true;

            DirectoryInfo d = new DirectoryInfo(folderPath);

            FileInfo[] files = d.GetFiles("*.pdf");

            foreach ( FileInfo file in files ) {
                paths.Add(file.Name);
            }

            position = 0;

            PDFList.SelectedIndex = position;



        }

        private void PDFList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if (PDFList.SelectedIndex>-1) {

                var uri = new System.Uri(folderPath + "\\" + PDFList.SelectedItem.ToString());

                PDFDock.IsEnabled = false;
                windowsFormsHost.IsEnabled = false;

                aTimer.Start();

                PDFView handl = (PDFView)windowsFormsHost.Child;
                handl.PdfFilePath  = uri.LocalPath;

                textBox.Text = PDFList.SelectedItem.ToString().Substring(0, PDFList.SelectedItem.ToString().Length - 4);

                position = PDFList.SelectedIndex;
            } 
        }

        private void Previous_Button_Click(object sender, RoutedEventArgs e) {
            if (position == 0) {
                position = paths.Count-1;
            } else  {
                position--;
            }

            PDFList.SelectedIndex = position;
        }

        private void Next_button_Click(object sender, RoutedEventArgs e) {
            if (position == paths.Count - 1) {
                position = 0;

            } else {
                position ++;

            }

            PDFList.SelectedIndex = position;
        }


        private void textBox_KeyUp(object sender, KeyEventArgs e) {
               
            if (e.Key== Key.Return && !(paths[position].Equals(textBox.Text + ".pdf"))) {

                var sUri = new System.Uri(folderPath + "\\" + paths[position]);

                var dUri = new System.Uri(folderPath + "\\" + textBox.Text + ".pdf");

                string sFile = sUri.LocalPath;

                string dFile = dUri.LocalPath;


                try {
                    File.Move(sFile, dFile);

                    paths[position] = textBox.Text + ".pdf";

                    Next_button_Click(sender, e);
                }
                catch {
                    MessageBox.Show("RenameFailed");
                }

                textBox.Focus();
                textBox.SelectAll();
            } else if (e.Key== Key.Return) {

                Next_button_Click(sender,e);
                textBox.Focus();
                textBox.SelectAll();
            }
        }

        private void OnTimedEvent(object sender, EventArgs e) {
            this.PDFDock.IsEnabled = true;
            this.windowsFormsHost.IsEnabled = true;
            aTimer.Stop();
        }

    }
}
