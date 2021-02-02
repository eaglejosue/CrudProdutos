using CadastroProduto.Domain.Commands;
using CadastroProduto.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CadastroProduto.Tests
{
    [TestClass]
    public class ProdutoTests
    {
        [TestMethod]
        public void CreateProdutoCommandValido()
        {
            //Arrange
            var moqNotifications = MoqValidationTests.NewNotificationHandler();
            MoqValidationTests.MockSetup(moqNotifications);

            //Act
            var command = new CreateProdutoCommand(new Produto("Produto A", 10m));
            var commandValid = command.IsValid();

            //Assert
            MoqValidationTests.VerifyTimes(moqNotifications, Times.Never);
            Assert.IsTrue(commandValid);
        }

        [TestMethod]
        public void CreateProdutoCommandNomeInvalido()
        {
            //Arrange
            var moqNotifications = MoqValidationTests.NewNotificationHandler();
            MoqValidationTests.MockSetup(moqNotifications);

            //Act
            var command = new CreateProdutoCommand(new Produto("", 1m));
            var commandValid = command.IsValid();

            //Assert
            MoqValidationTests.VerifyTimes(moqNotifications, Times.AtLeastOnce);
            Assert.IsFalse(commandValid);
        }

        [TestMethod]
        public void CreateProdutoCommandValorInvalido()
        {
            //Arrange
            var moqNotifications = MoqValidationTests.NewNotificationHandler();
            MoqValidationTests.MockSetup(moqNotifications);

            //Act
            var command = new CreateProdutoCommand(new Produto("Produto B", 0m));
            var commandValid = command.IsValid();

            //Assert
            MoqValidationTests.VerifyTimes(moqNotifications, Times.AtLeastOnce);
            Assert.IsFalse(commandValid);
        }

        [TestMethod]
        public void UpdateProdutoCommandValido()
        {
            //Arrange
            var moqNotifications = MoqValidationTests.NewNotificationHandler();
            MoqValidationTests.MockSetup(moqNotifications);

            //Act
            var command = new UpdateProdutoCommand(new Produto("Produto C", 1.5m));
            var commandValid = command.IsValid();

            //Assert
            MoqValidationTests.VerifyTimes(moqNotifications, Times.Never);
            Assert.IsTrue(commandValid);
        }

        [TestMethod]
        public void DeleteProdutoCommandValido()
        {
            //Arrange
            var moqNotifications = MoqValidationTests.NewNotificationHandler();
            MoqValidationTests.MockSetup(moqNotifications);

            //Act
            var command = new DeleteProdutoCommand(1);
            var commandValid = command.IsValid();

            //Assert
            MoqValidationTests.VerifyTimes(moqNotifications, Times.Never);
            Assert.IsTrue(commandValid);
        }
    }
}
