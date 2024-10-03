namespace Finnce_Api.Services
{
    public class CategoryService
    {
        private readonly RepositoryContext context;

        public CategoryService(RepositoryContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            if (this.context.Categories != null)
            {
                var categories = this.context.Categories.Select(categories => new Category
                {
                    Id = categories.Id,
                    Name = categories.Name,
                    IdUser = categories.IdUser,
                    User = categories.User
                }).ToList();
                return categories;
            }

            return null;
        }


        public async Task<List<Category>> GetAllCategoryForUser(string userId)
        {
            var categoryList = context.Categories
                .Where(t => t.IdUser == userId)
                .ToList();
            if (categoryList != null)
            {
                return categoryList;
            }

            return null;
        }


        public async Task<List<Category>> GetCategoriesById(string IdUser)
        {
            return await context.Categories
                .Where(category => category.IdUser == IdUser)
                .ToListAsync();
        }
        public Category UpdateCategory(CategoryModel category)
        {

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            var existingCategory = context.Categories.FirstOrDefault(c => c.Id == category.IdCategoria);

            if (existingCategory == null)
            {
                throw new DirectoryNotFoundException("Category not found");
            }

            // Update properties 
            existingCategory.Name = category.name;

            context.Categories.Update(existingCategory);




            context.SaveChanges();
            return existingCategory;

        }

        public Category CreateCategory(CategoryModel categoryModel)
        {
            if (categoryModel == null)
            {
                throw new ArgumentNullException(nameof(categoryModel));
            }

            var userById = context.Users.FirstOrDefault(u => u.Id == categoryModel.IdUser);
            var newCategory = new Category
            {
                Name = categoryModel.name,
                IdUser = userById.Id
            };

            context.Categories.Add(newCategory);
            context.SaveChanges();
            return newCategory;
        }

        public bool CreateCategoryGeneric(string idUser)
        {
            if (idUser == null)
            {
                return false;
            }


            var newCategoryGen = new Category
            {
                Name = "Categoria Genérica",
                IdUser = idUser,
                SearchTerms = "GenericCategory"
            };


            context.Categories.Add(newCategoryGen);


            var newCategoryFood = new Category
            {
                Name = "Alimentação",
                IdUser = idUser,
                SearchTerms = "FoodCategory"
            };


            context.Categories.Add(newCategoryFood);

            var newCategoryHealth = new Category
            {
                Name = "Saude",
                IdUser = idUser,
                SearchTerms = "HealthCategory"
            };


            context.Categories.Add(newCategoryHealth);

            var newCategoryTransp = new Category
            {
                Name = "Transporte",
                IdUser = idUser,
                SearchTerms = "TransportCategory"
            };


            context.Categories.Add(newCategoryTransp);

            var newCategoryLeisure = new Category
            {
                Name = "Lazer",
                IdUser = idUser,
                SearchTerms = "LeisureCategory"
            };


            context.Categories.Add(newCategoryLeisure);

            var newCategoryEducation = new Category
            {
                Name = "Educação",
                IdUser = idUser,
                SearchTerms = "EducationCategory"
            };


            context.Categories.Add(newCategoryEducation);

            context.SaveChanges();
            return true;
        }
        public Category CreateCategoryInTink(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if (SearchCategory != null)
            {
                return null;
            }

            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        }
        public async Task<Guid> SearchCategoryGeneric(string idUser)
        {
            var returnidCategory = await context.Categories.FirstOrDefaultAsync(category => category.SearchTerms == "GenericCategory" && category.IdUser == idUser);


            if (returnidCategory != null)
            {
                return returnidCategory.Id;
            }
            else
            {
                return Guid.Empty;
            }
        }

        private async Task<string> SearchCategory(string idCategory)
        {
            var returnidCategory = context.Categories.FirstOrDefault(category => category.IdInternal == idCategory);
            if (returnidCategory != null)
            {
                return returnidCategory.IdInternal;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Cria uma view da categoria na BD 
        /// </summary>
        /// <param name="categoriaModel"></param>
        /// <returns></returns>
        private Category MapCreateCategoryViewToCategory(CategoryModel categoriaModel)
        {
            var CategoryById = context.Categories.FirstOrDefault(u => u.Id == categoriaModel.IdCategoria);
            return new Category
            {
                Name = categoriaModel.name,
                IdUser = categoriaModel.IdUser
            };

        }

    }
}