using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WebAddressbookTests;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_testdata_generator
{
    class Program
    {
        static void Main(string[] args)
        {
            string datatype = args[0];
            int count = Convert.ToInt32(args[1]);
            string[] filename = args[2].Split('.');
            StreamWriter writer = new StreamWriter(args[2]);
            string format = filename[1];
            List<GroupData> groups = new List<GroupData>();
            List<ContactData> contacts = new List<ContactData>();
            if (datatype == "groups")
            {
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData()
                    {
                        Groupname = TestBase.GenerateRandomString(10),
                        Groupheader = TestBase.GenerateRandomString(50),
                        Groupfooter = TestBase.GenerateRandomString(50)
                    });
                }
                //if (format == "csv") WriteGroupsToCSVFile(writer, groups);
                if (format == "xml") WriteGroupsToXMLFile(writer, groups);
                else if (format == "json") WriteGroupsToJSONFile(writer, groups);
                else if (format == "xlsx") WriteGroupsToExelFile(args[2], groups);
                else Console.Write("wrong file format");
            }
            if (datatype == "contacts")
            {
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData()
                    {
                        Firstname = TestBase.GenerateRandomString(20),
                        Lastname = TestBase.GenerateRandomString(20),
                        Middlename = TestBase.GenerateRandomString(20),
                        Nickname = TestBase.GenerateRandomString(10),
                        Bday = Convert.ToInt32(TestBase.rnd.NextDouble() * 30),
                        Bmonth = Convert.ToInt32(TestBase.rnd.NextDouble() * 12),
                        Byear = Convert.ToString(Convert.ToInt32(TestBase.rnd.NextDouble() * 10)) +
                        Convert.ToString(Convert.ToInt32(TestBase.rnd.NextDouble() * 10)) +
                        Convert.ToString(Convert.ToInt32(TestBase.rnd.NextDouble() * 10)) +
                        Convert.ToString(Convert.ToInt32(TestBase.rnd.NextDouble() * 10)),
                        Company = TestBase.GenerateRandomString(30),
                        Email2 = TestBase.GenerateRandomString(30),
                        Homephone = TestBase.GenerateRandomString(10),
                        Homepage = TestBase.GenerateRandomString(40),
                        Notes = TestBase.GenerateRandomString(100),
                        Fax = TestBase.GenerateRandomString(10)
                    });
                }
                //if (format == "csv") WriteGroupsToCSVFile(writer, groups);
                if (format == "xml") WriteContactsToXMLFile(writer, contacts);
                else if (format == "json") WriteContactToJSONFile(writer, contacts);
                else Console.Write("wrong file format");
            }
            writer.Close();
        }

        public static void WriteGroupsToExelFile(string filename, List<GroupData> groups)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;
            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Groupname;
                sheet.Cells[row, 2] = group.Groupheader;
                sheet.Cells[row, 3] = group.Groupfooter;
                row++;
            }
            string fullpath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            //File.Delete(fullpath);
            wb.SaveAs(fullpath);
            wb.Close();
        }

        public static void WriteContactToJSONFile(StreamWriter writer, List<ContactData> contacts)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

        public static void WriteContactsToXMLFile(StreamWriter writer, List<ContactData> contacts)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        public static void WriteGroupsToJSONFile(StreamWriter writer, List<GroupData> groups)
        {
            writer.Write(JsonConvert.SerializeObject(groups,  Newtonsoft.Json.Formatting.Indented));
        }

        public static void WriteGroupsToXMLFile(StreamWriter writer, List<GroupData> groups)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        public static void WriteGroupsToCSVFile(StreamWriter writer, List<GroupData> groups)
        {
            for (int i =0; i < groups.Count; i++)
            {
                writer.WriteLine(groups[i].Groupname + "," + groups[i].Groupheader + "," + groups[i].Groupfooter);
            }
        }
    }
}
