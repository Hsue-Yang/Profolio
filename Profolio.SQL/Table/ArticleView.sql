CREATE TABLE [dbo].ArticleView (
	ID INT PRIMARY KEY IDENTITY(1,1),      -- 唯一 ID
    NoteID INT NOT NULL,                -- 關聯 HackMDNotes.ID
    UserID INT NULL,                       -- 可選：關聯用戶表（如果需要）
    ViewedAt DATETIME DEFAULT GETDATE()    -- 點擊時間
)