namespace Domain.Enums;

public enum OrderStatus
{
    Pending = 1,       // Bekliyor
    Confirmed = 2,     // Onaylandı
    Preparing = 3,     // Hazırlanıyor
    Shipped = 4,       // Kargolandı
    Delivered = 5,     // Teslim Edildi
    Cancelled = 6      // İptal Edildi
}

// Bunun bize faydası ne olacak?

// İleride Service katmanında kod yazarken "Kargolandı" yazıp harf hatası yapmak yerine

// doğrudan OrderStatus.Shipped diyerek güvenle kod yazabileceğiz.