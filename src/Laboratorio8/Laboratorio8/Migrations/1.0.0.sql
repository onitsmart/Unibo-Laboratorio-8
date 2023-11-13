USE [LineeGuida-Laboratorio8-00]
GO
/****** Object:  Table [dbo].[MetaModuli]    Script Date: 22/03/2023 16:39:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MetaModuli](
	[Id] [uniqueidentifier] NOT NULL,
	[Titolo] [nvarchar](50) NULL,
	[Descrizione] [nvarchar](250) NULL,
	[DataCreazione] [datetime2](7) NULL,
	[DataModifica] [datetime2](7) NULL,
	[MetaContenutoJson] [nvarchar](max) NULL,
 CONSTRAINT [PK_MetaModulo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Moduli]    Script Date: 22/03/2023 16:39:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Moduli](
	[Id] [uniqueidentifier] NOT NULL,
	[IdMetaModulo] [uniqueidentifier] NULL,
	[Titolo] [nvarchar](50) NULL,
	[Descrizione] [nvarchar](250) NULL,
	[DataCompilazione] [datetime2](7) NOT NULL,
	[ContenutoJson] [nvarchar](max) NULL,
 CONSTRAINT [PK_Modulo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Moduli]  WITH CHECK ADD  CONSTRAINT [FK_Modulo_MetaModulo] FOREIGN KEY([IdMetaModulo])
REFERENCES [dbo].[MetaModuli] ([Id])
ON UPDATE SET NULL
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Moduli] CHECK CONSTRAINT [FK_Modulo_MetaModulo]
GO
