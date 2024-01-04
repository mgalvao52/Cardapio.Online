using Cardapio.DB.Notifiers.Interface;

namespace Cardapio.DB.Notifiers
{
    public class ResponseMessage:IResponseMessage
    {
        public ResponseMessage()
        {
            Erros = new List<string>();
        }
        public List<string> Erros { get; }

        public bool IsValid => !Erros.Any();

        public void AddErros(string erro)
        {
            Erros.Add(erro);
        }
    }
}
