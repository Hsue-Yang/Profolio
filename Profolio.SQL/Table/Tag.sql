CREATE TABLE Tag (
    ID INT PRIMARY KEY IDENTITY(1,1),      -- 標籤唯一 ID
    Name NVARCHAR(255) NOT NULL,           -- 標籤名稱 (如 C#, .Net Core)
    ParentID INT NULL,                     -- 父標籤 ID (實現多層次分類)
);