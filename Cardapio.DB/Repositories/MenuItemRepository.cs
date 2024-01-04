using Cardapio.DB.Entiites;
using Cardapio.DB.Enums;
using Cardapio.DB.Repositories.Interfaces;

namespace Cardapio.DB.Repositories
{
    public class MenuItemRepository : BaseRepository<MenuItem>, IMenuItemRepository
    {
        private readonly CardapioContext context;

        public MenuItemRepository(CardapioContext context) : base(context)
        {
            this.context = context;
        }

        public override bool Validate(MenuItem entity, OperationType operationType)
        {
            var temp = context.MenuItem.Where(s => s.Name.ToLower() == entity.Name.ToLower())
                .AsQueryable();
            if (operationType == OperationType.Update)
            {
                if (entity.Id <= 0) 
                {
                    responseMessage.AddErros("Item não encontrado!");
                }
                temp = temp.Where(s => s.Id != entity.Id);
            }
            if (temp.Any())
            {
                responseMessage.AddErros($"Nome '{entity.Name}' já existe");
            }
            return responseMessage.IsValid;
                
        }
    }
}
