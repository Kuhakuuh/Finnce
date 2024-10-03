using Finnce_Api.Models.EntityModelDto;

namespace Finnce_Api.Services
{
    public class EntityService
    {
        private readonly RepositoryContext context;

        public EntityService(RepositoryContext context)
        {
            this.context = context;
        }

        public IEnumerable<EntityCore> GetAllEntitys()
        {
            if (this.context.Entities != null)
            {
                var entitys = this.context.Entities.Select(entitys => new EntityCore
                {
                    Id = entitys.Id,
                    Name = entitys.Name,
                    IdUser = entitys.IdUser,
                }).ToList();
                return entitys;
            }
            return null;
        }

        public async Task<List<EntityCore>> GetEntityByUserId(string IdUser)
        {
            return await context.Entities
                .Where(entity => entity.IdUser == IdUser)
                .ToListAsync();
        }

        public async Task<List<EntityCore>> GetAllEntityForUser(string userId)
        {
            var entityList = context.Entities
                .Where(t => t.IdUser == userId)
                .ToList();
            if (entityList != null)
            {
                return entityList;
            }

            return null;
        }


        public EntityCore CreateEntity(EntityModel entityModel, String idUser)
        {
            if (entityModel == null)
            {
                throw new ArgumentNullException(nameof(entityModel));
            }
            var userById = context.Users.FirstOrDefault(user => user.Id == entityModel.IdUser);
            var newEntity = new EntityCore
            {
                Id = entityModel.Id,
                Name = entityModel.Name,
                IdUser = idUser,
            };

            context.Entities.Add(newEntity);
            context.SaveChanges();
            return newEntity;
        }


        public bool DeleteEntity(String entityId)
        {
            try
            {
                var entityToDelete = context.Entities.FirstOrDefault(e => e.Id.ToString() == entityId);

                if (entityToDelete == null)
                {
                    return false;
                }
                context.Entities.Remove(entityToDelete);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir a entidade", ex);
            }
        }

        public EntityCore UpdateEntity(EntityModel entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var existingEntity = context.Entities.FirstOrDefault(c => c.Id == entity.Id);

            if (existingEntity == null)
            {
                throw new DirectoryNotFoundException("Category not found");
            }
            // Update properties 
            existingEntity.Name = entity.Name;

            context.Entities.Update(existingEntity);
            context.SaveChanges();
            return existingEntity;

        }

        public async Task<EntityCore> CreateEntityInTink(EntityCore entityModel)
        {
            if (entityModel == null)
            {
                throw new ArgumentNullException(nameof(entityModel));
            }

            EntityCore returnEntety = await SearchByName(entityModel);

            if (returnEntety == null)
            {
                context.Entities.Add(entityModel);
                context.SaveChanges();
                return entityModel;
            }
            else
            {
                return returnEntety;


            }


        }

        private async Task<EntityCore> SearchByName(EntityCore entityModel)
        {
            EntityCore resultEntity = await context.Entities
                .FirstOrDefaultAsync(e => e.Name == entityModel.Name && e.IdUser == entityModel.IdUser);

            return resultEntity;
        }
    }
}
