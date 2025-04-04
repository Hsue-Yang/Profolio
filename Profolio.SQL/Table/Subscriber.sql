CREATE TABLE Subscriber (
    ID INT PRIMARY KEY IDENTITY(1,1),      -- 唯一 ID
    Email NVARCHAR(255) NOT NULL,          -- 用戶 Email
    IsSubscribed BIT DEFAULT 1,            -- 訂閱狀態 (1: 訂閱中, 0: 停止)
    SubscribedAt DATETIME DEFAULT GETDATE(), -- 訂閱時間
    UnsubscribedAt DATETIME NULL           -- 停止訂閱時間
);