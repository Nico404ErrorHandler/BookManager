using System;

namespace De.HsFlensburg.ClientApp064.Services.AddBib
{
    public class AnalyzeString
    {

        //categories parses the String Array for Books and prepares an Array für the Propeties of said Book
        public String[] categories(string[] lines, int index)
        {
            string author = "";
            string year = "";
            string publisher = "";
            string title = "";
            string edition = "";
            string isbn = "";
            int j = index;
            while (lines[j] != "" && lines[j][0] != '}')
            {
                string tempstring = "";
                tempstring = findProperty(lines, j, tempstring);

                Boolean begins = false;
                string valueString = "";
                valueString = findValue(lines, j, valueString, begins);

                switch (tempstring.ToLower())
                {
                    case "author":
                        author = valueString;
                        break;

                    case "year":
                        year = valueString;
                        break;

                    case "publisher":
                        publisher = valueString;
                        break;

                    case "title":
                        title = valueString;
                        break;

                    case "isbn":
                        isbn = valueString;
                        break;

                    case "edition":
                        edition = valueString;
                        break;
                }
                j++;
            }
            string[] book = new string[6];
            book[0] = author;
            book[1] = year;
            book[2] = publisher;
            book[3] = edition;
            book[4] = title;
            book[5] = isbn;

            return (book);
        }

        //findProperty parses the String of a line for the Property it contains and returns said property
        private string findProperty(string[] lines, int j, string tempstring)
        {

            for (int i = 0; i < lines[j + 1].Length - 2 && lines[j + 1][i] != '='; i++)
            {
                if (!Char.IsWhiteSpace(lines[j + 1][i]))
                {
                    tempstring = tempstring + lines[j + 1][i].ToString();
                }
            }
            return tempstring;
        }

        //findValue parses the String of a line for the contents of the String and returns them
        private string findValue(string[] lines, int j, string valueString, Boolean begins)
        {
            for (int i = 0; i < lines[j + 1].Length - 1; i++)
            {
                if (begins)
                {

                    valueString = valueString + lines[j + 1][i].ToString();

                }

                if (lines[j + 1][i] == '{' || lines[j + 1][i] == '"')
                {

                    begins = true;
                }

                if (lines[j + 1][i + 1] == '}' || lines[j + 1][i + 1] == '"')
                {
                    begins = false;

                }
            }
            return valueString;
        }
    }
}
