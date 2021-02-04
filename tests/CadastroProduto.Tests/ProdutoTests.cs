using CadastroProduto.Domain.Commands;
using CadastroProduto.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CadastroProduto.Tests
{
    [TestClass]
    public class ProdutoTests
    {
        [TestMethod]
        public async void CreateProdutoCommandValido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var command = new CreateProdutoCommand(new Produto("Produto A", 10m));
            var commandValid = command.IsValid();
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var result = await handler.Handle(command);

            //Assert
            Assert.IsTrue(commandValid);
            Assert.IsFalse(result.HasErrors);
        }

        [TestMethod]
        public async void CreateProdutoCommandNomeInvalido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var command = new CreateProdutoCommand(new Produto("", 1m));
            var commandValid = command.IsValid();
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var result = await handler.Handle(command);

            //Assert
            Assert.IsFalse(commandValid);
            Assert.IsTrue(result.HasErrors);
        }

        [TestMethod]
        public async void CreateProdutoCommandValorInvalido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var command = new CreateProdutoCommand(new Produto("Produto B", 0m));
            var commandValid = command.IsValid();
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var result = await handler.Handle(command);

            //Assert
            Assert.IsFalse(commandValid);
            Assert.IsTrue(result.HasErrors);
        }

        [TestMethod]
        public async void UpdateProdutoCommandValido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var command = new UpdateProdutoCommand(new Produto(3, "Produto C", 1.5m));
            var commandValid = command.IsValid();
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var result = await handler.Handle(command);

            //Assert
            Assert.IsTrue(commandValid);
            Assert.IsFalse(result.HasErrors);
        }

        [TestMethod]
        public async void UpdateProdutoCommandNomeInvalido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var command = new UpdateProdutoCommand(new Produto(1, "", 150m));
            var commandValid = command.IsValid();
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var result = await handler.Handle(command);

            //Assert
            Assert.IsFalse(commandValid);
            Assert.IsTrue(result.HasErrors);
        }

        [TestMethod]
        public async void UpdateProdutoCommandValorInvalido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var command = new UpdateProdutoCommand(new Produto(2, "Produto B", 0m));
            var commandValid = command.IsValid();
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var result = await handler.Handle(command);

            //Assert
            Assert.IsFalse(commandValid);
            Assert.IsTrue(result.HasErrors);
        }

        [TestMethod]
        public async void DeleteProdutoCommandValido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var command = new DeleteProdutoCommand(1);
            var commandValid = command.IsValid();
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var result = await handler.Handle(command);

            //Assert
            Assert.IsTrue(commandValid);
            Assert.IsFalse(result.HasErrors);
        }

        [TestMethod]
        public async void DeleteProdutoCommandInvalido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var command = new DeleteProdutoCommand(0);
            var commandValid = command.IsValid();
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var result = await handler.Handle(command);

            //Assert
            Assert.IsFalse(commandValid);
            Assert.IsTrue(result.HasErrors);
        }
    }
}
