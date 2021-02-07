using System.Collections.Generic;

namespace CadastroProduto.Domain
{
    public class Result
    {
        public List<string> Errors { get; private set; }

        public Result()
        {
            Errors = new List<string>();
        }

        public Result(string error = null)
        {
            Errors = new List<string>();
            AddError(!string.IsNullOrWhiteSpace(error) ? error : "Ocorreu um erro interno.");
        }

        public Result(object obj)
        {
            Errors = new List<string>();
            DefineObjetoRetorno(obj);
        }


        public bool HasErrors => Errors.Count > 0;

        public void AddError(string error) => Errors.Add(!string.IsNullOrWhiteSpace(error) ? error : "Ocorreu um erro interno.");

        public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
        
        public object ObjetoRetorno { get; private set; }

        public void DefineObjetoRetorno(object obj)
        {
            ObjetoRetorno = obj;
        }
    }
}