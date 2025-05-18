using Microsoft.EntityFrameworkCore;
using ProtosInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtosInterface
{
    internal class ListFill
    {
        private dbDataLoader loader;
        private AppDbContext _context;
        public List<string> ItemOperations(MenuItem item)
        {
            List<string> result = new List<string>();
            IQueryable operations = _context.Operations.Include(o => o.OperationType).Where(o => o.ProductId == item.itemId);
            foreach (Operation operation in operations)
            {
                if (operation.OperationType != null)
                {
                    if (operation.Code < 10)
                        result.Add("0" + operation.Code + " | " + operation.OperationType.Name);
                    else
                        result.Add(operation.Code + " | " + operation.OperationType.Name);
                }
            }
            return result;
        }

        public ListFill()
        {
            //this.productId = productId;
            _context = new AppDbContext();
            loader = new dbDataLoader();
        }
    }
}
