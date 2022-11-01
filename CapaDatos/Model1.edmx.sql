
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/05/2022 02:17:09
-- Generated from EDMX file: C:\Users\migue\source\repos\MarketPlaceGroup.2.0\CapaDatos\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [db_a83dd0_markeplace];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CARRITO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CARRITO];
GO
IF OBJECT_ID(N'[dbo].[CATEGORIA]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CATEGORIA];
GO
IF OBJECT_ID(N'[dbo].[ESTADO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ESTADO];
GO
IF OBJECT_ID(N'[dbo].[reporte_usuario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[reporte_usuario];
GO
IF OBJECT_ID(N'[dbo].[ROL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ROL];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[db_a83dd0_markeplaceModelStoreContainer].[MostrarCarrito]', 'U') IS NOT NULL
    DROP TABLE [db_a83dd0_markeplaceModelStoreContainer].[MostrarCarrito];
GO
IF OBJECT_ID(N'[db_a83dd0_markeplaceModelStoreContainer].[TABLAREPORTE]', 'U') IS NOT NULL
    DROP TABLE [db_a83dd0_markeplaceModelStoreContainer].[TABLAREPORTE];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CARRITOes'
CREATE TABLE [dbo].[CARRITOes] (
    [ID] int  NOT NULL,
    [ID_PRODUCTO] int  NOT NULL,
    [ID_USUARIO] int  NOT NULL
);
GO

-- Creating table 'CATEGORIAs'
CREATE TABLE [dbo].[CATEGORIAs] (
    [ID] int  NOT NULL,
    [Descripcion] varchar(50)  NOT NULL
);
GO

-- Creating table 'ESTADOes'
CREATE TABLE [dbo].[ESTADOes] (
    [ID] int  NOT NULL,
    [Descripcion] varchar(50)  NOT NULL
);
GO

-- Creating table 'PRODUCTOS'
CREATE TABLE [dbo].[PRODUCTOS] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(50)  NOT NULL,
    [ID_USUARIO] int  NOT NULL,
    [Precio] int  NOT NULL,
    [ID_Categoria] int  NOT NULL,
    [Descripcion] varchar(100)  NOT NULL,
    [Estado] int  NOT NULL,
    [Ubicacion] varchar(400)  NOT NULL,
    [Fecha_Registro] datetime  NOT NULL,
    [imagen] varbinary(max)  NULL
);
GO

-- Creating table 'ROLs'
CREATE TABLE [dbo].[ROLs] (
    [ID] int  NOT NULL,
    [Descripcion] varchar(50)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'USUARIOS'
CREATE TABLE [dbo].[USUARIOS] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(50)  NOT NULL,
    [Correo] varchar(50)  NOT NULL,
    [Id_Rol] int  NOT NULL,
    [Id_Estado] int  NOT NULL,
    [Contrasena] varchar(100)  NOT NULL,
    [TELEFONO] varchar(50)  NOT NULL
);
GO

-- Creating table 'reporte_usuario'
CREATE TABLE [dbo].[reporte_usuario] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ID_usuario_reporte] int  NOT NULL,
    [ID_usuario_reportado] int  NOT NULL,
    [ID_publicacion] int  NOT NULL,
    [descripcion] varchar(200)  NOT NULL
);
GO

-- Creating table 'MostrarCarritoes'
CREATE TABLE [dbo].[MostrarCarritoes] (
    [ID] int  NOT NULL,
    [ID_PRODUCTO] int  NOT NULL,
    [Nombre] varchar(50)  NOT NULL,
    [Precio] int  NOT NULL,
    [Descripcion] varchar(100)  NOT NULL,
    [ID_USUARIO] int  NOT NULL,
    [imagen] varbinary(max)  NULL,
    [Estado] int  NOT NULL
);
GO

-- Creating table 'TABLAREPORTEs'
CREATE TABLE [dbo].[TABLAREPORTEs] (
    [IDProducto] int  NOT NULL,
    [Nombre] varchar(50)  NOT NULL,
    [Reportes] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'CARRITOes'
ALTER TABLE [dbo].[CARRITOes]
ADD CONSTRAINT [PK_CARRITOes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CATEGORIAs'
ALTER TABLE [dbo].[CATEGORIAs]
ADD CONSTRAINT [PK_CATEGORIAs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ESTADOes'
ALTER TABLE [dbo].[ESTADOes]
ADD CONSTRAINT [PK_ESTADOes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PRODUCTOS'
ALTER TABLE [dbo].[PRODUCTOS]
ADD CONSTRAINT [PK_PRODUCTOS]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ROLs'
ALTER TABLE [dbo].[ROLs]
ADD CONSTRAINT [PK_ROLs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [ID] in table 'USUARIOS'
ALTER TABLE [dbo].[USUARIOS]
ADD CONSTRAINT [PK_USUARIOS]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'reporte_usuario'
ALTER TABLE [dbo].[reporte_usuario]
ADD CONSTRAINT [PK_reporte_usuario]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID], [ID_PRODUCTO], [Nombre], [Precio], [Descripcion], [ID_USUARIO], [Estado] in table 'MostrarCarritoes'
ALTER TABLE [dbo].[MostrarCarritoes]
ADD CONSTRAINT [PK_MostrarCarritoes]
    PRIMARY KEY CLUSTERED ([ID], [ID_PRODUCTO], [Nombre], [Precio], [Descripcion], [ID_USUARIO], [Estado] ASC);
GO

-- Creating primary key on [IDProducto], [Nombre] in table 'TABLAREPORTEs'
ALTER TABLE [dbo].[TABLAREPORTEs]
ADD CONSTRAINT [PK_TABLAREPORTEs]
    PRIMARY KEY CLUSTERED ([IDProducto], [Nombre] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ID_PRODUCTO] in table 'CARRITOes'
ALTER TABLE [dbo].[CARRITOes]
ADD CONSTRAINT [fk_PRODUCTO1]
    FOREIGN KEY ([ID_PRODUCTO])
    REFERENCES [dbo].[PRODUCTOS]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_PRODUCTO1'
CREATE INDEX [IX_fk_PRODUCTO1]
ON [dbo].[CARRITOes]
    ([ID_PRODUCTO]);
GO

-- Creating foreign key on [ID_USUARIO] in table 'CARRITOes'
ALTER TABLE [dbo].[CARRITOes]
ADD CONSTRAINT [fk_USUARIO2]
    FOREIGN KEY ([ID_USUARIO])
    REFERENCES [dbo].[USUARIOS]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_USUARIO2'
CREATE INDEX [IX_fk_USUARIO2]
ON [dbo].[CARRITOes]
    ([ID_USUARIO]);
GO

-- Creating foreign key on [ID_Categoria] in table 'PRODUCTOS'
ALTER TABLE [dbo].[PRODUCTOS]
ADD CONSTRAINT [fk_Categoria1]
    FOREIGN KEY ([ID_Categoria])
    REFERENCES [dbo].[CATEGORIAs]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Categoria1'
CREATE INDEX [IX_fk_Categoria1]
ON [dbo].[PRODUCTOS]
    ([ID_Categoria]);
GO

-- Creating foreign key on [Id_Estado] in table 'USUARIOS'
ALTER TABLE [dbo].[USUARIOS]
ADD CONSTRAINT [fk_Estado1]
    FOREIGN KEY ([Id_Estado])
    REFERENCES [dbo].[ESTADOes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Estado1'
CREATE INDEX [IX_fk_Estado1]
ON [dbo].[USUARIOS]
    ([Id_Estado]);
GO

-- Creating foreign key on [Estado] in table 'PRODUCTOS'
ALTER TABLE [dbo].[PRODUCTOS]
ADD CONSTRAINT [fk_Estado2]
    FOREIGN KEY ([Estado])
    REFERENCES [dbo].[ESTADOes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Estado2'
CREATE INDEX [IX_fk_Estado2]
ON [dbo].[PRODUCTOS]
    ([Estado]);
GO

-- Creating foreign key on [ID_USUARIO] in table 'PRODUCTOS'
ALTER TABLE [dbo].[PRODUCTOS]
ADD CONSTRAINT [fk_Usuario1]
    FOREIGN KEY ([ID_USUARIO])
    REFERENCES [dbo].[USUARIOS]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Usuario1'
CREATE INDEX [IX_fk_Usuario1]
ON [dbo].[PRODUCTOS]
    ([ID_USUARIO]);
GO

-- Creating foreign key on [Id_Rol] in table 'USUARIOS'
ALTER TABLE [dbo].[USUARIOS]
ADD CONSTRAINT [fk_Rol1]
    FOREIGN KEY ([Id_Rol])
    REFERENCES [dbo].[ROLs]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Rol1'
CREATE INDEX [IX_fk_Rol1]
ON [dbo].[USUARIOS]
    ([Id_Rol]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------