USE [BusinessWeb]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetHistoriaAsistenciaPerson]    Script Date: 25/03/2020 9:00:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_GetHistoriaAsistenciaPerson]
(
	 @Dni VARCHAR(50)
)
AS
BEGIN

SET NOCOUNT ON;
                       SELECT Materia , COUNT (*) AS NumeroInasistencia FROM AsistenciaClase Where Dni = @Dni AND Status = 0 GROUP BY Materia
END;

