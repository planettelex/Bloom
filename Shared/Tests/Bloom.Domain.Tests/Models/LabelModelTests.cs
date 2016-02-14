using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the label model class.
    /// </summary>
    [TestFixture]
    public class LabelModelTests
    {
        const string LabelName = "Test Label";
        const string AlbumName = "Test Album";

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
        /// Tests adding personel to a label.
        /// </summary>
        [Test]
        public void AddPersonelToLabelTest()
        {
            const string personName = "Test Person";
            var label = Label.Create(LabelName);
            var person = Person.Create(personName);
            // todo

            Assert.AreEqual(label.Personnel.Count, 1);
        }

        /// <summary>
        /// Tests adding personel with roles to a label.
        /// </summary>
        [Test]
        public void AddPersonelWithRolesToLabelTest()
        {
            const string personName = "Test Person";
            var label = Label.Create(LabelName);
            var person = Person.Create(personName);
            //todo
            const string role1Name = "Accountant";
            const string role2Name = "Chief Financial Officer";
            var role1 = Role.Create(role1Name);
            var role2 = Role.Create(role2Name);


            Assert.AreEqual(label.Personnel.Count, 1);
        }

        
    }
}