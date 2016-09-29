using System;
using System.IO;
using System.Linq;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the label repository class.
    /// </summary>
    [TestFixture]
    public class LabelRepositoryTests
    {
        private const string TestFileName = "LabelRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private IRoleRepository _roleRepository;
        private IPersonRepository _personRepository;
        private ILabelRepository _labelRepository;
        private Guid _capitolId;
        private Guid _emiId;
        private Guid _harvestId;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);
            var photoRepository = new PhotoRespository();
            _roleRepository = new RoleRepository();
            _personRepository = new PersonRepository(photoRepository);
            _labelRepository = new LabelRepository(_roleRepository, _personRepository);

            var testFolder = Settings.TestsDataPath;
            if (!Directory.Exists(testFolder))
                Directory.CreateDirectory(testFolder);

            var testFilePath = Path.Combine(testFolder, TestFileName);
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);

            _dataSource.Create(testFilePath);
            PopulateDataSource();
        }

        /// <summary>
        /// Populates the data source.
        /// </summary>
        private void PopulateDataSource()
        {
            var founder = Role.Create("Founder");
            _roleRepository.AddRole(_dataSource, founder);
            var director = Role.Create("Director");
            _roleRepository.AddRole(_dataSource, director);
            var president = Role.Create("President");
            _roleRepository.AddRole(_dataSource, president);
            var ceo = Role.Create("CEO");
            _roleRepository.AddRole(_dataSource, ceo);

            var capitol = Label.Create("Capitol");
            _capitolId = capitol.Id;
            capitol.Bio = "Capitol Bio";
            capitol.FoundedOn = DateTime.Parse("2/2/1942");
            capitol.LogoFilePath = "c:\\images\\capitol.jpg";
            _labelRepository.AddLabel(_dataSource, capitol);
            var johnnyMercer = Person.Create("Johnny Mercer");
            _personRepository.AddPerson(_dataSource, johnnyMercer);
            var johnnyPersonnel = LabelPersonnel.Create(capitol, johnnyMercer, 1);
            _labelRepository.AddLabelPersonnel(_dataSource, johnnyPersonnel);
            _labelRepository.AddLabelPersonnelRole(_dataSource, johnnyPersonnel, founder);
            _labelRepository.AddLabelPersonnelRole(_dataSource, johnnyPersonnel, president);
            var buddyDeSylva = Person.Create("Buddy DeSylva");
            _personRepository.AddPerson(_dataSource, buddyDeSylva);
            var buddyPersonnel = LabelPersonnel.Create(capitol, buddyDeSylva, 2);
            _labelRepository.AddLabelPersonnel(_dataSource, buddyPersonnel);
            _labelRepository.AddLabelPersonnelRole(_dataSource, buddyPersonnel, director);

            var emi = Label.Create("EMI");
            _emiId = emi.Id;
            emi.FoundedOn = DateTime.Parse("3/1/1931");
            emi.ClosedOn = DateTime.Parse("9/28/2012");
            _labelRepository.AddLabel(_dataSource, emi);

            var harvest = Label.Create("Harvest");
            _harvestId = harvest.Id;
            harvest.FoundedOn = DateTime.Parse("1/1/1969");
            harvest.ParentLabelId = emi.Id;
            _labelRepository.AddLabel(_dataSource, harvest);
        }

        /// <summary>
        /// Tests the get label method.
        /// </summary>
        [Test]
        public void GetLabelTest()
        {
            var label = _labelRepository.GetLabel(_dataSource, _capitolId);
            Assert.NotNull(label);
            Assert.AreEqual(_capitolId, label.Id);
            Assert.AreEqual("Capitol", label.Name);
            Assert.AreEqual("Capitol Bio", label.Bio);
            Assert.AreEqual(DateTime.Parse("2/2/1942"), label.FoundedOn);
            Assert.IsNull(label.ClosedOn);
            Assert.AreEqual("c:\\images\\capitol.jpg", label.LogoFilePath);

            Assert.NotNull(label.Personnel);
            Assert.AreEqual(2, label.Personnel.Count);
            Assert.NotNull(label.Personnel[0].Person);
            Assert.AreEqual("Johnny Mercer", label.Personnel[0].Person.Name);
            Assert.NotNull(label.Personnel[0].Roles);
            Assert.AreEqual(2, label.Personnel[0].Roles.Count);
            Assert.AreEqual("Founder", label.Personnel[0].Roles[0].Name);
            Assert.AreEqual("President", label.Personnel[0].Roles[1].Name);
            Assert.NotNull(label.Personnel[1].Person);
            Assert.AreEqual("Buddy DeSylva", label.Personnel[1].Person.Name);
            Assert.NotNull(label.Personnel[1].Roles);
            Assert.AreEqual(1, label.Personnel[1].Roles.Count);
            Assert.AreEqual("Director", label.Personnel[1].Roles[0].Name);

            label = _labelRepository.GetLabel(_dataSource, _emiId);
            Assert.NotNull(label);
            Assert.AreEqual(_emiId, label.Id);
            Assert.AreEqual("EMI", label.Name);
            Assert.AreEqual(DateTime.Parse("3/1/1931"), label.FoundedOn);
            Assert.AreEqual(DateTime.Parse("9/28/2012"), label.ClosedOn);

            label = _labelRepository.GetLabel(_dataSource, _harvestId);
            Assert.NotNull(label);
            Assert.AreEqual(_harvestId, label.Id);
            Assert.AreEqual("Harvest", label.Name);
            Assert.AreEqual(_emiId, label.ParentLabelId);
        }

        /// <summary>
        /// Tests the list labels method.
        /// </summary>
        [Test]
        public void ListLabelsTest()
        {
            var labels = _labelRepository.ListLabels(_dataSource);
            Assert.NotNull(labels);
            Assert.AreEqual(3, labels.Count);

            Assert.AreEqual(_capitolId, labels[0].Id);
            Assert.AreEqual("Capitol", labels[0].Name);
            Assert.AreEqual("Capitol Bio", labels[0].Bio);
            Assert.AreEqual(DateTime.Parse("2/2/1942"), labels[0].FoundedOn);
            Assert.IsNull(labels[0].ClosedOn);
            Assert.AreEqual("c:\\images\\capitol.jpg", labels[0].LogoFilePath);

            Assert.AreEqual(_emiId, labels[1].Id);
            Assert.AreEqual("EMI", labels[1].Name);
            Assert.AreEqual(DateTime.Parse("3/1/1931"), labels[1].FoundedOn);
            Assert.AreEqual(DateTime.Parse("9/28/2012"), labels[1].ClosedOn);

            Assert.AreEqual(_harvestId, labels[2].Id);
            Assert.AreEqual("Harvest", labels[2].Name);
            Assert.AreEqual(_emiId, labels[2].ParentLabelId);
        }

        /// <summary>
        /// Tests the delete label method.
        /// </summary>
        [Test]
        public void DeleteLabelTest()
        {
            var ceo = _roleRepository.GetRole(_dataSource, "CEO");
            var epic = Label.Create("Epic");
            epic.Bio = "Epic Bio";
            epic.FoundedOn = DateTime.Parse("6/1/1953");
            epic.LogoFilePath = "c:\\images\\epic.jpg";
            _labelRepository.AddLabel(_dataSource, epic);
            var laReid = Person.Create("L.A. Reid");
            _personRepository.AddPerson(_dataSource, laReid);
            var laPersonnel = LabelPersonnel.Create(epic, laReid, 1);
            _labelRepository.AddLabelPersonnel(_dataSource, laPersonnel);
            _labelRepository.AddLabelPersonnelRole(_dataSource, laPersonnel, ceo);

            var label = _labelRepository.GetLabel(_dataSource, epic.Id);
            Assert.NotNull(label);
            Assert.AreEqual("Epic", label.Name);
            Assert.NotNull(label.Personnel);
            Assert.NotNull(label.Personnel[0].Roles);

            _labelRepository.DeleteLabel(_dataSource, label);

            var deletedLabel = _labelRepository.GetLabel(_dataSource, epic.Id);
            Assert.Null(deletedLabel);
        }

        /// <summary>
        /// Tests the delete label personnel role method.
        /// </summary>
        [Test]
        public void DeleteLabelPersonnelRoleTest()
        {
            var capitol = _labelRepository.GetLabel(_dataSource, _capitolId);
            var johnnyPersonnel = capitol.Personnel[0];
            Assert.AreEqual(2, johnnyPersonnel.Roles.Count);
            var ceo = _roleRepository.GetRole(_dataSource, "CEO");
            Assert.NotNull(ceo);
            _labelRepository.AddLabelPersonnelRole(_dataSource, johnnyPersonnel, ceo);

            capitol = _labelRepository.GetLabel(_dataSource, _capitolId);
            johnnyPersonnel = capitol.Personnel[0];
            Assert.AreEqual(3, johnnyPersonnel.Roles.Count);
            ceo = johnnyPersonnel.Roles.SingleOrDefault(r => r.Name == "CEO");
            Assert.NotNull(ceo);
            _labelRepository.DeleteLabelPersonnelRole(_dataSource, johnnyPersonnel, ceo);

            capitol = _labelRepository.GetLabel(_dataSource, _capitolId);
            johnnyPersonnel = capitol.Personnel[0];
            Assert.AreEqual(2, johnnyPersonnel.Roles.Count);
            ceo = johnnyPersonnel.Roles.SingleOrDefault(r => r.Name == "CEO");
            Assert.IsNull(ceo);
        }

        /// <summary>
        /// Tests the delete label personnel method.
        /// </summary>
        [Test]
        public void DeleteLabelPersonnelTest()
        {
            var capitol = _labelRepository.GetLabel(_dataSource, _capitolId);
            var glennWallichs = Person.Create("Glenn Wallichs");
            _personRepository.AddPerson(_dataSource, glennWallichs);
            var glennPersonnel = LabelPersonnel.Create(capitol, glennWallichs, 3);
            _labelRepository.AddLabelPersonnel(_dataSource, glennPersonnel);
            var ceo = _roleRepository.GetRole(_dataSource, "CEO");
            _labelRepository.AddLabelPersonnelRole(_dataSource, glennPersonnel, ceo);

            capitol = _labelRepository.GetLabel(_dataSource, _capitolId);
            Assert.AreEqual(3, capitol.Personnel.Count);
            glennPersonnel = capitol.Personnel[2];
            Assert.AreEqual(glennWallichs.Id, glennPersonnel.PersonId);
            Assert.NotNull(glennPersonnel.Roles);
            Assert.AreEqual(1, glennPersonnel.Roles.Count);
            Assert.AreEqual("CEO", glennPersonnel.Roles[0].Name);

            _labelRepository.DeleteLabelPersonnel(_dataSource, glennPersonnel);

            capitol = _labelRepository.GetLabel(_dataSource, _capitolId);
            Assert.AreEqual(2, capitol.Personnel.Count);
        }
    }
}
