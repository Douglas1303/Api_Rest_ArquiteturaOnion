using Bogus;
using Poc.Domain.Entities;
using System.Collections.Generic;

namespace Poc.Test.ObjectsFakers.Entities
{
    public static class CategoryModelFaker 
    {
        public static CategoryModel GetModelValid()
        {
            return new Faker<CategoryModel>("pt_BR")
                .CustomInstantiator(f => new CategoryModel(
                    f.Lorem.Sentence(3)
                    )).Generate(); 
        }

        public static List<CategoryModel> GetListModelValid()
        {
            return new Faker<CategoryModel>("pt_BR")
                .CustomInstantiator(f => new CategoryModel(
                    f.Lorem.Sentence(3)
                    )).Generate(3);
        }
    }
}