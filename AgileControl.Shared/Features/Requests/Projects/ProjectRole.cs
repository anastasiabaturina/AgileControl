using System.ComponentModel.DataAnnotations;

namespace AgileControl.Shared.Features.Requests.Projects;

public enum ProjectRole
{
    [Display(Name = "Scrum-мастер")]
    ScrumMaster = 1,

    [Display(Name = "Владелец продукта")]
    ProductOwner = 2,

    [Display(Name = "Разработчик")]
    Developer = 3,

    [Display(Name = "Гость")]
    Guest = 4
}