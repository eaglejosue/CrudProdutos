using CadastroProduto.Domain.Commands;
using CadastroProduto.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CadastroProduto.Tests
{
    [TestClass]
    public class ProdutoTests
    {
        [TestMethod]
        public async Task CreateProdutoCommandValido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var command = new CreateProdutoCommand(new Produto("Produto A", 10m));
            var result = await handler.Handle(command);

            //Assert
            Assert.IsTrue(command.IsValid());
            Assert.IsFalse(result.HasErrors);
        }

        [TestMethod]
        public async Task CreateProdutoCommandNomeInvalido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var command = new CreateProdutoCommand(new Produto("", 1m));
            var result = await handler.Handle(command);

            //Assert
            Assert.IsFalse(command.IsValid());
            Assert.IsTrue(result.HasErrors);
        }

        [TestMethod]
        public async Task CreateProdutoCommandValorInvalido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var command = new CreateProdutoCommand(new Produto("Produto B", 0m));
            var result = await handler.Handle(command);

            //Assert
            Assert.IsFalse(command.IsValid());
            Assert.IsTrue(result.HasErrors);
        }

        [TestMethod]
        public async Task UpdateProdutoCommandValido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var command = new UpdateProdutoCommand(new Produto(3, "Produto C", 1.5m));
            var result = await handler.Handle(command);

            //Assert
            Assert.IsTrue(command.IsValid());
            Assert.IsFalse(result.HasErrors);
        }

        [TestMethod]
        public async Task UpdateProdutoCommandNomeInvalido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var command = new UpdateProdutoCommand(new Produto(1, "", 150m));
            var result = await handler.Handle(command);

            //Assert
            Assert.IsFalse(command.IsValid());
            Assert.IsTrue(result.HasErrors);
        }

        [TestMethod]
        public async Task UpdateProdutoCommandValorInvalido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var command = new UpdateProdutoCommand(new Produto(2, "Produto B", 0m));
            var result = await handler.Handle(command);

            //Assert
            Assert.IsFalse(command.IsValid());
            Assert.IsTrue(result.HasErrors);
        }

        [TestMethod]
        public async Task DeleteProdutoCommandValido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var command = new DeleteProdutoCommand(1);
            var result = await handler.Handle(command);

            //Assert
            Assert.IsTrue(command.IsValid());
            Assert.IsFalse(result.HasErrors);
        }

        [TestMethod]
        public async Task DeleteProdutoCommandInvalido()
        {
            //Arrange
            var moqMediator = MoqValidationTests.NewMediator();
            var moqProdutoRepository = MoqValidationTests.NewProdutoRepository();

            //Act
            var handler = new Handler(moqMediator.Object, moqProdutoRepository.Object);
            var command = new DeleteProdutoCommand(0);
            var result = await handler.Handle(command);

            //Assert
            Assert.IsFalse(command.IsValid());
            Assert.IsTrue(result.HasErrors);
        }
    }
}
