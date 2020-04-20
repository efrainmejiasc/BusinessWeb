USE [BusinessWeb]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetHistoriaAsistenciaPerson]    Script Date: 19/04/2020 8:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Sp_GetHistoriaAsistenciaPerson]
(
	 @Dni VARCHAR(50)
)
AS
BEGIN

SET NOCOUNT ON;
                       SELECT Materia , DniAdm , COUNT (*) AS NumeroInasistencia FROM AsistenciaClase Where Dni = @Dni AND Status = 0 GROUP BY Materia,DniAdm 
END;

