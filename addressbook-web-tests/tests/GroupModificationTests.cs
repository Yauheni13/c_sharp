using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Threading;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : LoginTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            if (! app.Grouphelper.IsThereAnyGroup())
            {
                app.Grouphelper.CreateGroup(new GroupData("AnyGroup"));
            }
            List<GroupData> oldgroups = app.Grouphelper.GetGroupList();

            GroupData olddata = oldgroups[0];

            GroupData newgroup = new GroupData();
            newgroup.Groupname = "new";
            newgroup.Groupfooter = null;

            app.Grouphelper.Modify(0, newgroup);

            Assert.AreEqual(oldgroups.Count, app.Grouphelper.GetGroupCount());

            List<GroupData> newgroups = app.Grouphelper.GetGroupList();
            oldgroups[0].Groupname = newgroup.Groupname;
            newgroups.Sort();
            oldgroups.Sort();
            Assert.AreEqual(oldgroups, newgroups);

            foreach (GroupData group in newgroups)
            {
                if (group.Id == olddata.Id)
                {
                    Assert.AreEqual(newgroup.Groupname, group.Groupname);
                }
            }
        }
    }
}
