CREATE PROCEDURE [dbo].[sp_GetTagsWithArticles]
AS
SET NOCOUNT ON
BEGIN
    SELECT 
        t.ID AS TagID,
        t.Name,
        t.ParentID,
        n.NoteID,
        n.Title,
        n.CreatedAt  -- ?? 確保 ORDER BY 可用
    FROM HackMDNoteTag nt
    JOIN Tag t ON nt.TagID = t.ID
    JOIN HackMDNote n ON nt.NoteID = n.NoteID
    UNION
    -- 抓取沒有 Tag 的文章，歸類到 "Uncategorized" (TagID = 99)
    SELECT 
        99 AS TagID,
        'Uncategorized' AS Name,
        NULL AS ParentID,
        n.NoteID,
        n.Title,
        n.CreatedAt  -- ?? 確保 ORDER BY 可用
    FROM HackMDNote n
    WHERE NOT EXISTS (
        SELECT 1 FROM HackMDNoteTag nt WHERE nt.NoteID = n.NoteID
    )
    ORDER BY  TagID,ParentID, CreatedAt DESC; -- ?? 確保 ORDER BY 正確使用
END