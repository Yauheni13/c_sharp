﻿using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : LoginTestBase
    {

        public static IEnumerable<GroupData> CSVFileGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] allstrings = File.ReadAllLines(@"groups.csv");
            foreach (string s in allstrings)
            {
                string[] parts = s.Split(',');
                groups.Add(new GroupData()
                {
                    Groupname = parts[0],
                    Groupfooter = parts[1],
                    Groupheader = parts[2]
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> XMLFileGroupDataProvider()
        {
            return (List<GroupData>)new XmlSerializer(typeof(List<GroupData>)).
                Deserialize(new StreamReader(@"groups.xml"));
        }

        public static IEnumerable<GroupData> JSONFileGroupDataProvider()
        {
            return JsonConvert.DeserializeObject< List<GroupData>>(
                File.ReadAllText(@"groups.json"));
        }

        public static IEnumerable<GroupData> EXCELFileGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"groups.xlsx"));
            Excel.Worksheet sheet = wb.ActiveSheet;
            Excel.Range range = sheet.UsedRange;
            for (int i=1; i<=range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Groupname = range.Cells[i, 1].Value,
                    Groupheader = range.Cells[i,2].Value,
                    Groupfooter = range.Cells[i,3].Value
                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return groups;
        }


        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData()
                {
                    Groupname = GenerateRandomString(20),
                    Groupfooter = GenerateRandomString(50),
                    Groupheader = GenerateRandomString(50)
                });
            }
            return groups;
        }


        [Test, TestCaseSource("EXCELFileGroupDataProvider")]
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> oldgroups = app.Grouphelper.GetGroupList();

            app.Grouphelper.CreateGroup(group);

            Assert.AreEqual(oldgroups.Count + 1, app.Grouphelper.GetGroupCount());

            List<GroupData> newgroups = app.Grouphelper.GetGroupList();


            Assert.AreEqual(newgroups.Count, oldgroups.Count + 1);
            oldgroups.Add(group);
            newgroups.Sort();
            oldgroups.Sort();
            Assert.AreEqual(oldgroups, newgroups);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            List<GroupData> oldgroups = app.Grouphelper.GetGroupList();

            GroupData group = new GroupData("");
            group.Groupheader = "";
            group.Groupfooter = "";
            app.Grouphelper.CreateGroup(group);

            Assert.AreEqual(oldgroups.Count + 1, app.Grouphelper.GetGroupCount());

            List<GroupData> newgroups = app.Grouphelper.GetGroupList();
            Assert.AreEqual(newgroups.Count, oldgroups.Count + 1);
            oldgroups.Add(group);
            newgroups.Sort();
            oldgroups.Sort();
            Assert.AreEqual(oldgroups, newgroups);

        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            List<GroupData> oldgroups = app.Grouphelper.GetGroupList();


            GroupData group = new GroupData("a'a");
            group.Groupheader = "";
            group.Groupfooter = "";
            app.Grouphelper.CreateGroup(group);

            Assert.AreEqual(oldgroups.Count + 1, app.Grouphelper.GetGroupCount());

            List<GroupData> newgroups = app.Grouphelper.GetGroupList();
            Assert.AreEqual(newgroups.Count, oldgroups.Count + 1);
            oldgroups.Add(group);
            newgroups.Sort();
            oldgroups.Sort();
            Assert.AreEqual(oldgroups, newgroups);

        }

    }
}