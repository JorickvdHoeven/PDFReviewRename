using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Integration;
using System.Windows;

namespace PDFREnamer {
    class PDFViewerHost : System.Windows.Forms.Integration.WindowsFormsHost {
        public static readonly DependencyProperty PdfPathProperty = DependencyProperty.Register("PdfPath", typeof(string), typeof(PDFViewerHost), new PropertyMetadata(PdfPathPropertyChanged));

        private readonly PDFView wrappedControl;

        public PDFViewerHost() {
            wrappedControl = new PDFView();
            Child = wrappedControl;
        }

        public string PdfPath {
            get {
                return (string)GetValue(PdfPathProperty);
            }
            set {
                SetValue(PdfPathProperty, value);
            }
        }

        private static void PdfPathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            PDFViewerHost host = (PDFViewerHost)d;
            host.wrappedControl.PdfFilePath = (string)e.NewValue;
        }

    }
}
