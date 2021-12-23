using De.HsFlensburg.ClientApp064.Business.Model.BusinessObjects;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Xml;


namespace De.HsFlensburg.ClientApp064.Services.ExportInXml
{
    public class ExportFileHandler
    {
        //-----Convert BitMapImage to Base64 String-----//
        private string convertImageToBase64(Image img)
        {
            MemoryStream stream = new MemoryStream();
            img.Save(stream, ImageFormat.Jpeg);
            byte[] imageBytes = stream.ToArray();

            return Convert.ToBase64String(imageBytes);
        }

        //-----Export BookModel to XML under given Path-----//
        public void WriteModelToFile(string path, Book book)
        {
            try
            {
                //The exported book will get created as a XML-File at the given path
                XmlWriter xmlWriter = XmlWriter.Create(path);

                //Writing the document-start
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Book");

                //Writing all propertys of book in the XML-File
                xmlWriter.WriteStartElement("Title");
                xmlWriter.WriteString(book.Title);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Author");
                xmlWriter.WriteString(book.Author);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Style");
                xmlWriter.WriteString(book.Style);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("ISBN");
                xmlWriter.WriteString(book.ISBN);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Year");
                xmlWriter.WriteString(book.Year.ToString());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Publisher");
                xmlWriter.WriteString(book.Publisher.Name);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Language");
                xmlWriter.WriteString(book.Language);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Price");
                xmlWriter.WriteString(book.Price.ToString());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Edition");
                xmlWriter.WriteString(book.Edition.ToString());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Genre");
                xmlWriter.WriteString(book.Genre);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Location");
                xmlWriter.WriteString(book.Location);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Image");
                xmlWriter.WriteString(convertImageToBase64(book.Cover));
                xmlWriter.WriteEndElement();

                //Write the end of the XML-File and close the document
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();

                //Display information if book was exported successfully
                MessageBox.Show("The choosen book was exported at the given filepath.", "Export succeed");
            }

            //ERRORHANDLING: Display error if a filepath wasn't valid
            catch
            {
                MessageBox.Show("Directory not found. Please use a valid filepath !", "Filepath not found");
            }
            
        }
    }
}
