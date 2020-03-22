SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Sp_InsertUpdatePerson]
(
     @Nombre VARCHAR(50),
	 @Apellido VARCHAR(50),
	 @Dni VARCHAR(50),
	 @Matricula VARCHAR(50),
	 @Rh VARCHAR(20),
	 @Grado VARCHAR(20),
	 @Grupo VARCHAR(20),
     @Email VARCHAR(50),
	 @IdCompany INT,
	 @Company VARCHAR(200),
	 @Date DATETIME,
	 @Status BIT,
	 @Foto VARCHAR (MAX),
	 @Qr VARCHAR (MAX),
	 @Turno INT
)
AS
BEGIN

SET NOCOUNT ON;

DECLARE @Ide INT; 

SET @Ide = (SELECT Id FROM PERSON WHERE Dni = @Dni);

IF(@Ide = NULL OR @Ide = 0)
   BEGIN 
           INSERT INTO Person    (Nombre,
	                             Apellido,
	                             Dni,
	                             Matricula,
	                             Rh,
	                             Grado,
	                             Grupo,
                                 Email ,
	                             IdCompany,
	                             Company,
	                             Date,
	                             Status,
	                             Foto,
	                             Qr,
	                             Turno)          VALUES   
								                         (@Nombre,
	                                                      @Apellido,
	                                                      @Dni,
	                                                      @Matricula,
	                                                      @Rh,
	                                                      @Grado,
	                                                      @Grupo,
                                                          @Email ,
	                                                      @IdCompany,
	                                                      @Company,
	                                                      @Date,
	                                                      @Status,
	                                                      @Foto,
	                                                      @Qr,
	                                                      @Turno)
  
  END 

  ELSE 
     BEGIN 
	        UPDATE  Person  SET  Nombre = @Nombre,
	                             Apellido = @Apellido,
	                             Dni = @Dni,
	                             Matricula= @Matricula,
	                             Rh = @Rh,
	                             Grado = @Grado,
	                             Grupo = @Grupo,
                                 Email = @Email,
	                             IdCompany = @IdCompany,
	                             Company= @Company,
	                             Date = @Date,
	                             Status = @Status,
	                             Foto = @Foto,
	                             Qr= @Qr,
	                             Turno =@Turno
             WHERE Dni = @Dni
	 END 
END;

