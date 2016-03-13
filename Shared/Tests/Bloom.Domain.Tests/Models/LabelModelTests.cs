using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the label model classes.
    /// </summary>
    [TestFixture]
    public class LabelModelTests
    {
        const string LabelName = "Test Label";

        /// <summary>
        /// Tests the label create method.
        /// </summary>
        [Test]
        public void CreateLabelTest()
        {
            var label = Label.Create(LabelName);

            Assert.AreNotEqual(label.Id, Guid.Empty);
            Assert.AreEqual(label.Name, LabelName);
        }

        /// <summary>
        /// Tests the label properties.
        /// </summary>
        [Test]
        public void LabelPropertiesTest()
        {
            var id = Guid.NewGuid();
            var label = new Label
            {
                Id = id,
                Name = LabelName,
                Bio = "Bio",
                LogoFilePath = "c:\\images\\label.jpg",
                FoundedOn = DateTime.Parse("2/4/1990"),
                ClosedOn = DateTime.Parse("9/9/2009")
            };

            Assert.AreEqual(label.Id, id);
            Assert.AreEqual(label.Name, LabelName);
            Assert.AreEqual(label.Bio, "Bio");
            Assert.AreEqual(label.LogoFilePath, "c:\\images\\label.jpg");
            Assert.AreEqual(label.FoundedOn, DateTime.Parse("2/4/1990"));
            Assert.AreEqual(label.ClosedOn, DateTime.Parse("9/9/2009"));
        }

        /// <summary>
        /// Tests the label personnel create method.
        /// </summary>
        [Test]
        public void CreateLabelPersonnelTest()
        {
            var label = Label.Create(LabelName);
            var person = Person.Create("Person");
            var labelPersonnel = LabelPersonnel.Create(label, person, 1);

            Assert.AreNotEqual(labelPersonnel.Id, Guid.Empty);
            Assert.AreEqual(labelPersonnel.LabelId, label.Id);
            Assert.AreEqual(labelPersonnel.PersonId, person.Id);
            Assert.AreEqual(labelPersonnel.Person.Name, "Person");
        }

        /// <summary>
        /// Tests the label personnel role create method.
        /// </summary>
        [Test]
        public void CreateLabelPersonnelRoleTest()
        {
            var label = Label.Create(LabelName);
            var person = Person.Create("Personnel");
            var labelPersonnel = LabelPersonnel.Create(label, person, 1);
            var role = Role.Create("Role");
            var labelPersonnelRole = LabelPersonnelRole.Create(labelPersonnel, role);

            Assert.AreEqual(labelPersonnelRole.LabelPersonelId, labelPersonnel.Id);
            Assert.AreEqual(labelPersonnelRole.RoleId, role.Id);
        }
    }
}