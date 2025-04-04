﻿CREATE TABLE TechImage (
    ID INT PRIMARY KEY IDENTITY(1,1),
    TagID NVARCHAR(255) NOT NULL UNIQUE, -- 技術名稱或標籤名稱
    PhotoUrl NVARCHAR(500) NOT NULL        -- 對應圖片的 URL
);