using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CadastroProduto.Domain.Entities;
using CadastroProduto.Domain.Interfaces.Repositories;
using CadastroProduto.Domain.Notifications;
using MediatR;

namespace CadastroProduto.Domain.Commands
{
    public class Handler : IRequestHandler<CreateProdutoCommand, Result>,
                           IRequestHandler<UpdateProdutoCommand, Result>,
                           IRequestHandler<DeleteProdutoCommand, Result>
    {
        private readonly IMediator _mediator;
        private readonly IProdutoRepository _produtoRepository;

        public Handler(IMediator mediator, IProdutoRepository produtoRepository)
        {
            _mediator = mediator;
            _produtoRepository = produtoRepository;
        }

        private IEnumerable<string> GetErrors(ProdutoCommand request) =>
            request.ValidationResult.Errors.Select(err => err.ErrorMessage);

        public async Task<Result> Handle(CreateProdutoCommand request, CancellationToken cancellationToken = default)
        {
            var result = new Result();

            if (request.IsValid())
            {
                var produto = await _produtoRepository.Add(new Produto(request.Nome, request.Valor, request.ImagemURL));
                result.DefineObjetoRetorno(produto);
            }
            else
            {
                await _mediator.Publish(new Notification(request.ValidationResult), cancellationToken);
                result.AddErrors(GetErrors(request));
            }

            return result;
        }

        public async Task<Result> Handle(UpdateProdutoCommand request, CancellationToken cancellationToken = default)
        {
            var result = new Result();

            if (request.IsValid())
            {
                var produto = await _produtoRepository.GetById(request.Id);
                if (produto != null)
                {
                    produto.Nome = request.Nome;
                    produto.Valor = request.Valor;
                    produto.ImagemURL = request.ImagemURL;
                    produto = await _produtoRepository.Update(produto);
                    result.DefineObjetoRetorno(produto);
                }
                else
                {
                    var message = "O Produto não foi encontrado.";
                    await _mediator.Publish(new Notification(message), cancellationToken);
                    result.AddError(message);
                }
            }
            else
            {
                await _mediator.Publish(new Notification(request.ValidationResult), cancellationToken);
                result.AddErrors(GetErrors(request));
            }

            return result;
        }

        public async Task<Result> Handle(DeleteProdutoCommand request, CancellationToken cancellationToken = default)
        {
            var result = new Result();

            if (request.IsValid())
            {
                var produto = await _produtoRepository.GetById(request.Id);
                if (produto != null)
                    await _produtoRepository.Remove(produto);
                else
                {
                    var message = "O Produto não foi encontrado.";
                    await _mediator.Publish(new Notification(message), cancellationToken);
                    result.AddError(message);
                }
            }
            else
            {
                await _mediator.Publish(new Notification(request.ValidationResult), cancellationToken);
                result.AddErrors(GetErrors(request));
            }

            return result;
        }
    }
}