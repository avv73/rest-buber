using BuberBreakfast.Data;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts
{
    public class DbBreakfastService(BuberBreakfastContext dbContext) : IBreakfastService
    {
        public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
        {
            dbContext.Add(breakfast);

            dbContext.SaveChanges();

            return Result.Created;
        }

        public ErrorOr<Deleted> DeleteBreakfast(Guid id)
        {
            Breakfast? breakfastToDelete = dbContext.Breakfasts
                                                  .FirstOrDefault(b => b.Id == id);

            if (breakfastToDelete == null)
            {
                return Errors.Breakfast.NotFound;
            }

            dbContext.Remove(breakfastToDelete); 

            dbContext.SaveChanges();

            return Result.Deleted;
        }

        public ErrorOr<Breakfast> GetBreakfast(Guid id)
        {
            Breakfast? breakfastToFind = dbContext.Breakfasts
                                                 .FirstOrDefault(b => b.Id == id);

            if (breakfastToFind == null)
            {
                return Errors.Breakfast.NotFound;
            }

            return breakfastToFind;
        }

        public ErrorOr<UpsertedBreakfastResult> UpsertBreakfast(Breakfast breakfast)
        {
            Breakfast? breakfastToFind = dbContext.Breakfasts
                                                 .FirstOrDefault(b => b.Id == breakfast.Id);

            if (breakfastToFind == null)
            {
                dbContext.Add(breakfast);
            }
            else
            {
                breakfastToFind.CopyFrom(breakfast);
            }

            dbContext.SaveChanges();

            return new UpsertedBreakfastResult(breakfastToFind == null);
        }
    }
}
