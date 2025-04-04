CREATE TABLE HackMDNoteTag (
    [Id] [int] IDENTITY(1,1) NOT NULL,                
    [NoteID] [varchar](50) NOT NULL,       -- 關聯 HackMDNotes 表 NoteID
    [TagID] [int] NOT NULL,                    -- 關聯 Tags 表
    PRIMARY KEY ([NoteID], [TagID])            -- 複合主鍵，避免重複關聯
);