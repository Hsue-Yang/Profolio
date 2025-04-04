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
        n.CreatedAt  -- ?? �T�O ORDER BY �i��
    FROM HackMDNoteTag nt
    JOIN Tag t ON nt.TagID = t.ID
    JOIN HackMDNote n ON nt.NoteID = n.NoteID
    UNION
    -- ����S�� Tag ���峹�A�k���� "Uncategorized" (TagID = 99)
    SELECT 
        99 AS TagID,
        'Uncategorized' AS Name,
        NULL AS ParentID,
        n.NoteID,
        n.Title,
        n.CreatedAt  -- ?? �T�O ORDER BY �i��
    FROM HackMDNote n
    WHERE NOT EXISTS (
        SELECT 1 FROM HackMDNoteTag nt WHERE nt.NoteID = n.NoteID
    )
    ORDER BY  TagID,ParentID, CreatedAt DESC; -- ?? �T�O ORDER BY ���T�ϥ�
END