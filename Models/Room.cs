//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomImage { get; set; }
        public decimal RoomPrice { get; set; }
        public int BookingStatusId { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomCapacity { get; set; }
        public string RoomDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
