USE SmartPlantDb;
GO

-- Delete existing admin if any
DELETE FROM admins WHERE username='admin';
GO

-- Insert admin user
-- Password: admin123
INSERT INTO admins (username, name, surname, email, password, is_deleted, created_date)
VALUES (
    'admin',
    'Admin',
    'User',
    'admin@smartplant.com',
    '$2a$11$/OQuH1QAfRaz8bl6Atxx2ezd/Rpi2Y0YX3RVue8.JvgFI8P0MIK/2',
    0,
    GETDATE()
);
GO

-- Verify
SELECT id, username, email, LEN(password) as password_length, LEFT(password, 10) as password_start
FROM admins
WHERE username='admin';
GO