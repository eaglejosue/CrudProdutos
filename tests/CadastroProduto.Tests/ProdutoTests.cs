using CadastroProduto.Domain.Commands;
using CadastroProduto.Domain.Entities;
using CadastroProduto.Domain.Notifications;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CadastroProduto.Tests
{
    [TestClass]
    public class ProdutoTests
    {
        public Mock<IMediator> MoqMediator;
        public Mock<INotificationHandler<Notification>> MoqNotifications;

        public void ArrangeMoqs()
        {
            MoqMediator = MoqValidationTests.NewMediator();
            MoqValidationTests.MediatorSetup(MoqMediator);

            MoqNotifications = MoqValidationTests.NewNotificationHandler();
            MoqValidationTests.NotificationSetup(MoqNotifications);
        }

        [TestMethod]
        public async void CreateProdutoCommandValido()
        {
            //Arrange
            ArrangeMoqs();

            //Act
            var command = new CreateProdutoCommand(new Produto("Produto A", 10m));
            var commandValid = command.IsValid();
            var result = await MoqMediator.Object.Send(command);

            //Assert
            MoqValidationTests.MediatorVerify(MoqMediator, Times.Once);
            MoqValidationTests.NotificationVerify(MoqNotifications, Times.Never);
            Assert.IsTrue(commandValid);
            Assert.IsFalse(result.HasErrors);
        }

        [TestMethod]
        public async void CreateProdutoCommandNomeInvalido()
        {
            //Arrange
            ArrangeMoqs();

            //Act
            var command = new CreateProdutoCommand(new Produto("", 1m));
            var commandValid = command.IsValid();
            var result = await MoqMediator.Object.Send(command);

            //Assert
            MoqValidationTests.MediatorVerify(MoqMediator, Times.Once);
            MoqValidationTests.NotificationVerify(MoqNotifications, Times.AtLeastOnce);
            Assert.IsFalse(commandValid);
            Assert.IsTrue(result.HasErrors);
        }

        [TestMethod]
        public async void CreateProdutoCommandValorInvalido()
        {
            //Arrange
            ArrangeMoqs();

            //Act
            var command = new CreateProdutoCommand(new Produto("Produto B", 0m));
            var commandValid = command.IsValid();
            var result = await MoqMediator.Object.Send(command);

            //Assert
            MoqValidationTests.MediatorVerify(MoqMediator, Times.Once);
            MoqValidationTests.NotificationVerify(MoqNotifications, Times.AtLeastOnce);
            Assert.IsFalse(commandValid);
            Assert.IsTrue(result.HasErrors);
        }

        [TestMethod]
        public async void UpdateProdutoCommandValido()
        {
            //Arrange
            ArrangeMoqs();

            //Act
            var command = new UpdateProdutoCommand(new Produto(3, "Produto C", 1.5m));
            var commandValid = command.IsValid();
            var result = await MoqMediator.Object.Send(command);

            //Assert
            MoqValidationTests.MediatorVerify(MoqMediator, Times.Once);
            MoqValidationTests.NotificationVerify(MoqNotifications, Times.Never);
            Assert.IsTrue(commandValid);
            Assert.IsFalse(result.HasErrors);
        }

        [TestMethod]
        public async void UpdateProdutoCommandNomeInvalido()
        {
            //Arrange
            ArrangeMoqs();

            //Act
            var command = new UpdateProdutoCommand(new Produto(1, "", 150m));
            var commandValid = command.IsValid();
            var result = await MoqMediator.Object.Send(command);

            //Assert
            MoqValidationTests.MediatorVerify(MoqMediator, Times.Once);
            MoqValidationTests.NotificationVerify(MoqNotifications, Times.AtLeastOnce);
            Assert.IsFalse(commandValid);
            Assert.IsTrue(result.HasErrors);
        }

        [TestMethod]
        public async void UpdateProdutoCommandValorInvalido()
        {
            //Arrange
            ArrangeMoqs();

            //Act
            var command = new UpdateProdutoCommand(new Produto(2, "Produto B", 0m));
            var commandValid = command.IsValid();
            var result = await MoqMediator.Object.Send(command);

            //Assert
            MoqValidationTests.MediatorVerify(MoqMediator, Times.Once);
            MoqValidationTests.NotificationVerify(MoqNotifications, Times.AtLeastOnce);
            Assert.IsFalse(commandValid);
            Assert.IsTrue(result.HasErrors);
        }

        [TestMethod]
        public async void DeleteProdutoCommandValido()
        {
            //Arrange
            ArrangeMoqs();

            //Act
            var command = new DeleteProdutoCommand(1);
            var commandValid = command.IsValid();
            var result = await MoqMediator.Object.Send(command);

            //Assert
            MoqValidationTests.MediatorVerify(MoqMediator, Times.Once);
            MoqValidationTests.NotificationVerify(MoqNotifications, Times.Never);
            Assert.IsTrue(commandValid);
            Assert.IsFalse(result.HasErrors);
        }

        [TestMethod]
        public async void DeleteProdutoCommandInvalido()
        {
            //Arrange
            ArrangeMoqs();

            //Act
            var command = new DeleteProdutoCommand(0);
            var commandValid = command.IsValid();
            var result = await MoqMediator.Object.Send(command);

            //Assert
            MoqValidationTests.MediatorVerify(MoqMediator, Times.Once);
            MoqValidationTests.NotificationVerify(MoqNotifications, Times.AtLeastOnce);
            Assert.IsFalse(commandValid);
            Assert.IsTrue(result.HasErrors);
        }
    }
}
