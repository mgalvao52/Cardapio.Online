namespace Cardapio.DB.Notifiers.Interface
{
    public interface IResponseMessage
    {
        List<string> Erros { get; }
        bool IsValid { get; }
        void AddErros(string erro);
    }
}
