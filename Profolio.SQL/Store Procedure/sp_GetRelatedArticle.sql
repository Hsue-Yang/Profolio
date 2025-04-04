CREATE PROCEDURE [dbo].[sp_GetRelatedArticle]
    @noteID VARCHAR(50)
AS
BEGIN
SET NOCOUNT ON;
    ;WITH TagMatchCount AS
    (
        SELECT 
            h.NoteID,
            -- Count how many tags in h.NoteID match the tags of @NoteID
            COUNT(nt.TagID) AS TagCount
        FROM HackMDNote h
        -- Use LEFT JOIN so that articles without matching tags still appear
        LEFT JOIN HackMDNoteTag nt
               ON h.NoteID = nt.NoteID
              AND nt.TagID IN (
                  SELECT TagID 
                  FROM HackMDNoteTag
                  WHERE NoteID = @NoteID
              )
        GROUP BY h.NoteID
    )
    SELECT 
        h.NoteID,
        h.Title,
        h.CreatedAt
    FROM HackMDNote h
    INNER JOIN TagMatchCount tmc
        ON h.NoteID = tmc.NoteID
    WHERE h.NoteID <> @NoteID  -- Exclude the current article itself
    ORDER BY 
        tmc.TagCount DESC,      -- Articles with more matching tags first
        h.CreatedAt DESC;
END