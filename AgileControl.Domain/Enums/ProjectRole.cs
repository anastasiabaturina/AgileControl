namespace AgileControl.Domain.Enums;

public enum ProjectRole
{
    Owner,          // Создатель проекта, полный доступ
    ScrumMaster,    // Организатор процессов, следит за методологией
    ProductOwner,   // Представляет интересы заказчика, формирует backlog
    Developer,      // Участник разработки
    Guest
}
