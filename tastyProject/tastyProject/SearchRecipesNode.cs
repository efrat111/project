using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tastyProject
{
    public class SearchRecipesNode
    {
        int recipeCode;
        int sameIngredientsNum;

        public SearchRecipesNode(int recipeCode, int sameIngredientsNum)
        {
            this.recipeCode = recipeCode;
            this.sameIngredientsNum = sameIngredientsNum;
        }

        public int RecipeCode { get => recipeCode; set => recipeCode = value; }
        public int SameIngredientsNum { get => sameIngredientsNum; set => sameIngredientsNum = value; }
    }
}
