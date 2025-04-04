CREATE PROCEDURE [dbo].[sp_GetSearchResults]
    @query NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT NoteID, Title
    FROM HackMDNote
    WHERE Title LIKE '%' + @query + '%';
END