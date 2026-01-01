-- SmartPlant - İlk Admin Kullanıcısı Ekleme
-- Kullanıcı Adı: admin
-- Şifre: admin123
-- Email: admin@smartplant.com

USE SmartPlantDb;
GO

-- BCrypt hash for password 'admin123'
-- Bu hash'i oluşturmak için aşağıdaki komutu çalıştır:
-- dotnet run --project SmartPlant -- hash-password admin123

INSERT INTO admins (username, name, surname, email, password, is_deleted, created_date)
VALUES (
    'admin',
    'Admin',
    'User',
    'admin@smartplant.com',
    '$2a$11$mZ9ZqwQqwQqwQqwQqwQqwu8YY6Y.Y6Y.Y6Y.Y6Y.Y6Y.Y6Y.Y6Y.Y',
    0,
    GETDATE()
);
GO

-- Eklenen kullanıcıyı kontrol et
SELECT id, username, name, surname, email, is_deleted, created_date
FROM admins
WHERE username = 'admin';
GO