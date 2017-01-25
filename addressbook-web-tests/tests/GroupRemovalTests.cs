using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : LoginTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            if (!app.Grouphelper.IsThereAnyGroup())
            {
                app.Grouphelper.CreateGroup(new GroupData("AnyGroup"));
            }
            List<GroupData> oldgroups = app.Grouphelper.GetGroupList();

            app.Grouphelper.Remove(0);

            Assert.AreEqual(oldgroups.Count - 1, app.Grouphelper.GetGroupCount());

            List<GroupData> newgroups = app.Grouphelper.GetGroupList();
            Assert.AreEqual(newgroups.Count, oldgroups.Count - 1);

            foreach (GroupData group in newgroups)
            {
                Assert.AreNotEqual(group.Id, oldgroups[0].Id);
            }

            oldgroups.RemoveAt(0);
            oldgroups.Sort();
            newgroups.Sort();
            Assert.AreEqual(newgroups, oldgroups);

            
        }


    }
}
