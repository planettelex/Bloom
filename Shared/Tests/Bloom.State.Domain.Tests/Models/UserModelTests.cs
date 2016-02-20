using System;
using Bloom.Domain.Models;
using Bloom.State.Domain.Models;
using NUnit.Framework;

namespace Bloom.State.Domain.Tests.Models
{
    /// <summary>
    /// Tests the user model class.
    /// </summary>
    [TestFixture]
    public class UserModelTests
    {
        const string PersonName = "Test Person";

        /// <summary>
        /// Tests the user create method.
        /// </summary>
        [Test]
        public void CreateUserTest()
        {
            var person = Person.Create(PersonName);
            person.SetProfileImage("c:\\images\\profile.png");
            person.BornOn = DateTime.Parse("07/06/80");
            person.Twitter = "@person";

            var user = User.Create(person);

            Assert.AreEqual(person.Id, user.PersonId);
            Assert.AreEqual(PersonName, user.Name);
            Assert.AreEqual("c:\\images\\profile.png", user.ProfileImagePath);
            Assert.AreEqual(DateTime.Parse("07/06/80"), user.Birthday);
            Assert.AreEqual("@person", user.Twitter);
        }

        /// <summary>
        /// Tests the as person method.
        /// </summary>
        [Test]
        public void UserAsPersonTest()
        {
            var person = Person.Create(PersonName);
            person.SetProfileImage("c:\\images\\profile.png");
            person.BornOn = DateTime.Parse("07/06/80");
            person.Twitter = "@person";

            var user = User.Create(person);
            var asPerson = user.AsPerson();

            Assert.AreEqual(person.Id, asPerson.Id);
            Assert.AreEqual(person.Name, asPerson.Name);
            Assert.AreEqual(person.BornOn, asPerson.BornOn);
            Assert.AreEqual(person.Twitter, asPerson.Twitter);
            Assert.AreEqual(person.ProfileImage.FilePath, asPerson.ProfileImage.FilePath);
        }
    }
}