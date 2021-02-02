using CadastroProduto.Domain.Commands;
using CadastroProduto.Domain.Entities;
using CadastroProduto.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using CadastroProduto.Domain;

namespace CadastroProduto.Web.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : ApiController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository produtoRepository, IMediator mediator, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Método assíncrono responsável por obter uma lista de Produtos
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>IActionResult</returns>
        /// <example>GET: api/Produto</example>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var produtos = await _produtoRepository.GetAll();
                if (produtos.Count() == 0) return NotFound();

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return Result(new Result(ex.Message));
            }
        }

        /// <summary>
        /// Método assíncrono responsável por obter um Produto pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>IActionResult</returns>
        /// <example>GET: api/Produto/1</example>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id == 0) return NotFound();

                var produto = await _produtoRepository.GetById(id);
                if (produto == null) return NotFound();

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return Result(new Result(ex.Message));
            }
        }

        /// <summary>
        /// Método assíncrono responsável por criar um Produto
        /// </summary>
        /// <param name="produto"></param>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>IActionResult</returns>
        /// <example>POST: api/Produto</example>
        [HttpPost]
        public async Task<IActionResult> Post(Produto produto)
        {
            try
            {
                var result = new Result();

                if (ModelState.IsValid == false)
                {
                    AddModelStateErrorsToResult(result);
                    return Result(result);
                }

                var command = _mapper.Map<CreateProdutoCommand>(produto);
                result = await _mediator.Send(command);

                return Result(result);
            }
            catch (Exception ex)
            {
                return Result(new Result(ex.Message));
            }
        }

        /// <summary>
        /// Método assíncrono responsável por alterar um Produto
        /// </summary>
        /// <param name="produto"></param>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>IActionResult</returns>
        /// <example>PUT: api/Produto</example>
        [HttpPut]
        public async Task<IActionResult> Put(Produto produto)
        {
            try
            {
                var result = new Result();

                if (ModelState.IsValid == false)
                {
                    AddModelStateErrorsToResult(result);
                    return Result(result);
                }

                if (produto == null) return NotFound();

                var produtoBd = await _produtoRepository.GetById(produto.Id);
                if (produtoBd == null) return NotFound();

                var command = _mapper.Map<UpdateProdutoCommand>(produto);
                result = await _mediator.Send(command);

                return Result(result);
            }
            catch (Exception ex)
            {
                return Result(new Result(ex.Message));
            }
        }

        /// <summary>
        /// Método assíncrono responsável por deletar um Produto
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>IActionResult</returns>
        /// <example>DELETE: api/Produto/1</example>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var request = new DeleteProdutoCommand(id);
                var result = await _mediator.Send(request);
                return Result(result);
            }
            catch (Exception ex)
            {
                return Result(new Result(ex.Message));
            }
        }
    }
}
