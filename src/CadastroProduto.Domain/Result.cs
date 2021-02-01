using System.Collections.Generic;

namespace CadastroProduto.Domain
{
    public class Result
    {
        private List<string> _errors;

        public Result()
        {
            _errors = new List<string>();
        }

        public Result(string error = null) => AddError(error ?? "Ocorreu um erro interno.");

        public List<string> Errors => _errors;

        public bool HasErrors => _errors.Count > 0;

        public void AddError(string error) => _errors.Add(error);

        public void AddErrors(IEnumerable<string> errors) => _errors.AddRange(errors);
        
        public object ObjetoRetorno { get; private set; }

        public void DefineObjetoRetorno(object obj)
        {
            ObjetoRetorno = obj;
        }
    }
}