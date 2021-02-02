using CadastroProduto.Domain;
using CadastroProduto.Domain.Commands;
using CadastroProduto.Domain.Interfaces.Repositories;
using CadastroProduto.Domain.Notifications;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroProduto.Tests
{
    public class MoqValidationTests
    {
        public static Mock<INotificationHandler<Notification>> NewNotificationHandler() => new Mock<INotificationHandler<Notification>>();

        public static void NotificationSetup(Mock<INotificationHandler<Notification>> mock) =>
            mock.Setup(s => s.Handle(It.IsAny<Notification>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        public static void NotificationVerify(Mock<INotificationHandler<Notification>> mock, Func<Times> times) =>
            mock.Verify(f => f.Handle(It.IsAny<Notification>(), It.IsAny<CancellationToken>()), times);
        
        public static Mock<IMediator> NewMediator() => new Mock<IMediator>();

        public static void MediatorSetup(Mock<IMediator> mock) =>
            mock.Setup(s => s.Send(It.IsAny<ProdutoCommand>(), It.IsAny<CancellationToken>())).Returns((Task<Result>)Task.CompletedTask);

        public static void MediatorVerify(Mock<IMediator> mock, Func<Times> times) =>
            mock.Verify(v => v.Send(It.IsAny<ProdutoCommand>(), It.IsAny<CancellationToken>()), times);

        public static Mock<IProdutoRepository> NewProdutoRepository() => new Mock<IProdutoRepository>();
    }
}
