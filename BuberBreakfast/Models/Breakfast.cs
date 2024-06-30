using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.ServiceErrors;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BuberBreakfast.Models
{
    [Index(nameof(Id), IsUnique = true)]
    public class Breakfast
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 50;

        public const int MinDescriptionLength = 50;
        public const int MaxDescriptionLength = 150;

        [Key]
        public int NumId { get; set; }

        public Guid Id { get; set;  }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime StartDateTime { get; set;}

        public DateTime EndDateTime { get; set; }

        public DateTime LastModifiedDateTime { get; set; }

        public List<string> Savory { get; set; } = null!;

        public List<string> Sweet { get; set; } = null!;

        public Breakfast()
        {
            
        }

        private Breakfast(
            Guid id, 
            string name, 
            string description,
            DateTime startDateTime, 
            DateTime endDateTime, 
            DateTime lastModifiedDateTime, 
            List<string> savory, 
            List<string> sweet)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            LastModifiedDateTime = lastModifiedDateTime;
            Savory = savory;
            Sweet = sweet;
        }

        public static ErrorOr<Breakfast> Create(
            string name,
            string description,
            DateTime startDateTime,
            DateTime endDateTime,
            List<string> savory,
            List<string> sweet,
            Guid? id = null)
        {
            List<Error> errors = new();

            if (name.Length < MinNameLength || name.Length > MaxNameLength)
            {
                errors.Add(Errors.Breakfast.InvalidName);
            }

            if (description.Length < MinDescriptionLength || description.Length > MaxDescriptionLength)
            {
                errors.Add(Errors.Breakfast.InvalidDescription);
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Breakfast(
                id ?? Guid.NewGuid(),
                name,
                description,
                startDateTime,
                endDateTime,
                DateTime.UtcNow,
                savory,
                sweet);
        }

        public void CopyFrom(Breakfast other)
        {
            Id = other.Id;
            Name = other.Name;
            Description = other.Description;
            StartDateTime = other.StartDateTime;
            EndDateTime = other.EndDateTime;
            LastModifiedDateTime = other.LastModifiedDateTime;
            Savory = other.Savory;
            Sweet = other.Sweet;
        }

        public static ErrorOr<Breakfast> From(CreateBreakfastRequest request)
        {
            return Create(
                request.Name,
                request.Description,
                request.StartDateTime,
                request.EndDateTime,
                request.Savory,
                request.Sweet);
        }

        public static ErrorOr<Breakfast> From(Guid id, UpsertBreakfastRequest request)
        {
            return Create(
                request.Name,
                request.Description,
                request.StartDateTime,
                request.EndDateTime,
                request.Savory,
                request.Sweet,
                id);
        }
    }
}
