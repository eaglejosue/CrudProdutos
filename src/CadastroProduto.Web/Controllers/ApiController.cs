using CadastroProduto.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CadastroProduto.Web.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected void AddModelStateErrorsToResult(Result result)
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                result.AddError(erroMsg);
            }
        }

        protected IActionResult Result(Result result = null)
        {
            if (result == null)
                return UnprocessableEntity(new { sucess = false });

            if (result.HasErrors)
                return BadRequest(new { sucess = false, errors = result.Errors });

            return Ok(new { sucess = true, obj = result.ObjetoRetorno });
        }
    }
}
