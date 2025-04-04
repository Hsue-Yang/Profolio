CREATE TABLE Timeline (
    ID INT PRIMARY KEY IDENTITY(1,1),      -- 唯一 ID
    TimePoint DATETIME NOT NULL,           -- 時間點
    Title NVARCHAR(255) NOT NULL,          -- 標題
    Description NVARCHAR(MAX) NOT NULL,    -- 詳細描述
    ImageUrl NVARCHAR(500) NULL,           -- 圖片 URL
    CreatedAt DATETIME DEFAULT GETDATE()   -- 創建時間
);