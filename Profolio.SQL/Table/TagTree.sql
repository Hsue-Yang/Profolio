CREATE TABLE TagTree (
    ID INT PRIMARY KEY IDENTITY(1,1),      -- 節點唯一 ID
    TagID INT NULL,                        -- 選擇性關聯 Tags
    Url NVARCHAR(500) NOT NULL,            -- 對應的 URL
);