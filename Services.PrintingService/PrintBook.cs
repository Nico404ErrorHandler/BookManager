using System.Drawing.Printing;
using System.Drawing;

namespace De.HsFlensburg.ClientApp064.Services.PrintingService
{
    public class PrintBook
    {
        public PrintBook()
        {

        }

        private Image Cover { get; set; }
        private string[] BookDetails { get; set; }
        //This method gets all the selected printer settings that were made in the PrintWindow and gets
        //called by pressing the "Print" button. This method is called in the PrintWindowViewModel where
        //it passes the selected settings. In this method the selected settings gets apllied and prints
        //the selected book
        public void Print(string printerName, int selectedPaperSize, bool isLandscape, short copies, int fromPage,
                          int toPage, int selectedPrinterResolution, int selectedDuplex, string[] bookDetails, 
                          Image cover, int selectedColor)
        {
            Cover = cover;
            BookDetails = bookDetails;
            PrintDocument pd = new PrintDocument();

            pd.PrinterSettings.PrinterName = printerName;
            pd.DefaultPageSettings.Landscape = isLandscape;
            PrinterResolution printerResolution = pd.PrinterSettings.PrinterResolutions[selectedPrinterResolution];
            pd.DefaultPageSettings.PrinterResolution = printerResolution;
            PaperSize paperSize = pd.PrinterSettings.PaperSizes[selectedPaperSize];
            pd.DefaultPageSettings.PaperSize = paperSize;

            if (copies < 1)
            {
                copies = 1;
            }
            pd.PrinterSettings.Copies = copies;
            pd.PrinterSettings.FromPage = fromPage;
            pd.PrinterSettings.ToPage = toPage;
            pd.PrinterSettings.Duplex = selectedDuplex == 1 ? Duplex.Vertical : Duplex.Simplex;
            pd.DefaultPageSettings.Color = selectedColor == 1 ? true : false;

            pd.PrintPage += PrintPage;
            pd.Print();

        }
        //This method defines what is printed on the paper
        //It is split into textinformation and the cover
        private void PrintPage(object o, PrintPageEventArgs e)
        {
            float xOffset = 60;
            float yOffset = 60;
            Font font = new Font("Arial", 11, FontStyle.Bold);
            e.Graphics.DrawString("Title: " + BookDetails[1], font, Brushes.Black, new PointF(xOffset, 30F + yOffset));
            e.Graphics.DrawString("Author: " + BookDetails[2], font, Brushes.Black, new PointF(xOffset, 50F + yOffset));
            e.Graphics.DrawString("Style: " + BookDetails[3], font, Brushes.Black, new PointF(xOffset, 70F + yOffset));
            e.Graphics.DrawString("ISBN: " + BookDetails[4], font, Brushes.Black, new PointF(xOffset, 90F + yOffset));
            e.Graphics.DrawString("Publisher: " + BookDetails[5], font, Brushes.Black, new PointF(xOffset, 110F + yOffset));
            e.Graphics.DrawString("Language: " + BookDetails[6], font, Brushes.Black, new PointF(xOffset, 130F + yOffset));
            e.Graphics.DrawString("Price: " + BookDetails[7] + "€", font, Brushes.Black, new PointF(xOffset, 150F + yOffset));
            e.Graphics.DrawString("Edition: " + BookDetails[8], font, Brushes.Black, new PointF(xOffset, 170F + yOffset));
            e.Graphics.DrawString("Year: " + BookDetails[9], font, Brushes.Black, new PointF(xOffset, 190F + yOffset));
            e.Graphics.DrawString("Genre: " + BookDetails[10], font, Brushes.Black, new PointF(xOffset, 210F + yOffset));
            e.Graphics.DrawString("Location: " + BookDetails[11], font, Brushes.Black, new PointF(xOffset, 230F + yOffset));
            Image img = ResizeImage(Cover, new Size(600, 600));

            Point loc = new Point(60, 350);
            e.Graphics.DrawImage(img, loc);
        }
        //This method checks if the loaded cover is too big for the print, and if it's too big it gets
        //resized so that it fits on the paper
        private static Image ResizeImage(Image imgToResize, Size size)
        {
            int imgResizeWidth = imgToResize.Width;
            int imgResizeHeight = imgToResize.Height;
            if (imgResizeWidth > 400 || imgResizeHeight > 400)
            {
                float p;
                float pWidth;
                float pHeight;
                pWidth = ((float)size.Width / (float)imgResizeWidth);
                pHeight = ((float)size.Height / (float)imgResizeHeight);

                if (pHeight < pWidth)
                {
                    p = pHeight;
                }
                else
                {
                    p = pWidth;
                }

                int newWidth = (int)(imgResizeWidth * p);
                int newHeight = (int)(imgResizeHeight * p);
                Bitmap resizedImage = new Bitmap(newWidth, newHeight);
                Graphics g = Graphics.FromImage(resizedImage);
                g.DrawImage(imgToResize, 0, 0, newWidth, newHeight);
                g.Dispose();
                return resizedImage;
            }
            else
            {
                return imgToResize;
            }

        }
    }
}