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

        public static void MockSetup(Mock<INotificationHandler<Notification>> mock) =>
            mock.Setup(s => s.Handle(It.IsAny<Notification>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        public static void VerifyTimes(Mock<INotificationHandler<Notification>> mock, Func<Times> times) =>
            mock.Verify(f => f.Handle(It.IsAny<Notification>(), It.IsAny<CancellationToken>()), times);
    }
}
