CREATE TABLE UserIntroCard (
    ID INT PRIMARY KEY IDENTITY(1,1),     -- 唯一 ID
    Title NVARCHAR(150) NOT NULL,         -- 卡片標題
    Description NVARCHAR(500) NOT NULL,   -- 卡片描述
    Date DATE NOT NULL,                   -- 日期
    ImageUrl NVARCHAR(150) NULL,          -- 圖片 URL
    Category NVARCHAR(100) NOT NULL,      -- 類別
    IconUrl NVARCHAR(150) NULL            -- 類別圖標 URL
);