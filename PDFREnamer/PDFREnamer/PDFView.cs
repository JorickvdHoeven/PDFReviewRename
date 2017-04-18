using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFREnamer {
    public partial class PDFView : System.Windows.Forms.UserControl {
        private string pdfFilePath;

        public PDFView() {
            InitializeComponent();
            acrobatViewer.setShowToolbar(false);
            acrobatViewer.setView("FitH");
            acrobatViewer.setPageMode("none");
            this.Dock = DockStyle.Fill;

        }

        public string PdfFilePath {
            get {
                return pdfFilePath;
            }

            set {
                if (pdfFilePath != value) {
                    pdfFilePath = value;
                    ChangeCurrentDisplayedPDF();
                }
            }
        }

        private void ChangeCurrentDisplayedPDF() {
            acrobatViewer.LoadFile(pdfFilePath);
            acrobatViewer.src = pdfFilePath;
            acrobatViewer.setViewScroll("FitH", 0);
        }
    }


}
