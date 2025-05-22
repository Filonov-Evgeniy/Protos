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
        public List<MenuItem> ItemOperations(MenuItem item)
        {
            List<MenuItem> result = new List<MenuItem>();
            IQueryable operations = _context.Operations.Include(o => o.OperationType).Where(o => o.ProductId == item.itemId);
            foreach (Operation operation in operations)
            {
                if (operation.OperationType != null)
                {
                    if (operation.Code < 10)
                        result.Add(new MenuItem { Title = "0" + operation.Code + " | " + operation.OperationType.Name, Id = operation.Id });
                    else
                        result.Add(new MenuItem { Title = operation.Code + " | " + operation.OperationType.Name, Id = operation.Id });
                }
            }
            return result;
        }

        public List<MenuItem> OperationEqupment(Operation opId)
        {
            List<MenuItem> result = new List<MenuItem>();
            //IQueryable operation = _context.OperationVariants.Include(ov => ov.Operation).Where(ov => ov.OperationId == opId);
            //IQueryable equipment = _context.OperationVariantComponents.Include(ovc => ovc.OperationVariant).Where(ovc => ovc.OperationVariantId == );
            //IQueryable<OperationVariant> operationVariants = _context.OperationVariants
            //    .Include(ov => ov.Operation)
            //    .Where(ov => ov.OperationId == opId);

            //IQueryable<OperationVariantComponent> equipmentVariant = _context.OperationVariantComponents
            //    .Include(ovc => ovc.OperationVariant)
            //    .Where(ovc => operationVariants.Select(ov => ov.Id).Contains(ovc.OperationVariantId));

            //IQueryable<Equipment> equipment = _context.Equipment
            //    .Select(e => e.Name);


            //пока самое рабочее осталось сделать саму выборку

            //IQueryable equipments = _context.Equipment
            //    .Select(e => e.Name) // подгрузка связанных данных
            //    .Where(e => _context.OperationVariantComponents
            //        .Where(ovc => _context.OperationVariants
            //            .Where(ov => ov.OperationId == opId)
            //            .Select(ov => ov.Id)
            //            .Contains(ovc.OperationVariantId))
            //        .Select(ovc => ovc.EquipmentId)
            //        .Contains(e.Id));

            var equipments = _context.OperationVariants
                .Where(ov => ov.OperationId == opId.Id)
                .Join(
                    _context.OperationVariantComponents,
                    ov => ov.Id,
                    ovc => ovc.OperationVariantId,
                    (ov, ovc) => new { ovc.Equipment.Name }
                )
                .Distinct()
                .ToList();

            foreach (var operation in equipments)
            {
                //if (operation.OperationType != null)
                //{
                //    if (operation.Code < 10)
                //        result.Add("0" + operation.Code + " | " + operation.OperationType.Name);
                //    else
                //        result.Add(operation.Code + " | " + operation.OperationType.Name);
                //}
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
