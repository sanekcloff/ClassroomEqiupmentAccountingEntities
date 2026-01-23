using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomEquipmentAccountingEntities.Models
{
    [Flags]
    public enum Permission
    {
        // Default
        None = 0, // нет доступа

        // Classrooms part
        ViewingClassrooms = 1 << 0, // просмотр кабинетов
        AddClassrooms = 1 << 1, // добавление кабинетов
        UpdateClassrooms = 1 << 2, // редактирование кабинетов
        RemoveClassrooms = 1 << 3, // удаление кабинетов
        FullClassroomsAccess = ViewingClassrooms | AddClassrooms | UpdateClassrooms | RemoveClassrooms, // все права для кабинетов

        // Equipments part
        ViewingEquipments = 1 << 4,
        AddEquipments = 1 << 5,
        UpdateEquipments = 1 << 6,
        RemoveEquipments = 1 << 7,
        FullEquipmentsAccess = ViewingEquipments | AddEquipments | UpdateEquipments | RemoveEquipments,

        // Users part
        ViewingUsers = 1 << 8,
        AddUsers = 1 << 9,
        UpdateUsers = 1 << 10,
        RemoveUsers = 1 << 11,
        FullUsersAccess = ViewingUsers | AddUsers | UpdateUsers | RemoveUsers,

        // Categories part
        ViewingCategories = 1 << 12,
        AddCategories = 1 << 13,
        UpdateCategories = 1 << 14,
        RemoveCategories = 1 << 15,
        FullCategoriesAccess = ViewingCategories | AddCategories | UpdateCategories | RemoveCategories,

        // Requests part
        ViewingRequests = 1 << 16,
        AddRequests = 1 << 17,
        UpdateRequests = 1 << 18,
        RemoveRequests = 1 << 19,
        FullRequestsAccess = ViewingRequests | AddRequests | UpdateRequests | RemoveRequests,

        //Inventory part
        CreatingInventory = 1 << 20,

        // Admin part
        Administrator = FullClassroomsAccess | FullEquipmentsAccess | FullUsersAccess |
                    FullCategoriesAccess | FullRequestsAccess | CreatingInventory

    }
}
