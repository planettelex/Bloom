using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the role model class.
    /// </summary>
    [TestFixture]
    public class RoleModelTests
    {
        /// <summary>
        /// Tests the role create method.
        /// </summary>
        [Test]
        public void CreateRoleTest()
        {
            const string roleName = "Test Role";
            var role = Role.Create(roleName);

            Assert.AreNotEqual(role.Id, Guid.Empty);
            Assert.AreEqual(role.Name, roleName);
        }

        /// <summary>
        /// Tests the role to string method.
        /// </summary>
        [Test]
        public void RoleToStringTest()
        {
            const string roleName = "Test Role";
            var role = Role.Create(roleName);

            Assert.AreEqual(role.ToString(), roleName);
        }
    }
}